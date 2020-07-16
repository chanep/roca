using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Oracle.DataAccess.Client;

namespace Cno.Roca.Core.Entity
{

    public class Direction
    {
        private Direction()
        {
        }

        public static Direction Asc(object field)
        {
            return new Direction();
        }

        public static Direction Desc(object field)
        {
            return new Direction();
        }
    }

    public class SqlQueryGen<T>
    {
        private Dictionary<string, string> _mapeoDb;
        private string _param;

        public string Query { get; private set; }

        public IList<OracleParameter> QueryParameters { get; private set; }

        public SqlQueryGen(Dictionary<string, string> mapeoDb, Expression<Func<T, bool>> expression, params Expression<Func<T, Direction>>[] orders)
        {
            _mapeoDb = mapeoDb;
            
            Query = "";
            QueryParameters = new List<OracleParameter>();

            var exp = expression as LambdaExpression;
            if (exp == null)
                throw new NotSupportedException("La expresion de condicion no es valida");

            if (exp.Parameters.Count != 1)
                throw new ApplicationException("La expresion debe tener un parametro");

            _param = exp.Parameters[0].Name;

            var where = ConditionExp2String(exp.Body);
            where = FixNullOperand(where);
            var order = OrderExp2String(orders);

            Query = where + order;


        }

        private string OrderExp2String(Expression<Func<T, Direction>>[] orders)
        {
            if (orders == null || orders.Length == 0)
                return "";
            StringBuilder order = new StringBuilder();
            order.Append(" ORDER BY");
            foreach (var orderExp in orders)
            {
                var lambdaExp = orderExp as LambdaExpression;
                if (lambdaExp == null)
                    throw new NotSupportedException("La expresion de ordenamiento no es valida");
                var mcExp = lambdaExp.Body as MethodCallExpression;
                if(mcExp == null)
                    throw new NotSupportedException("La expresion de ordenamiento no es valida");
                
                string direction = mcExp.Method.Name.ToUpper();
                
                var unExp = mcExp.Arguments[0] as UnaryExpression;
                MemberExpression memExp;
                if (unExp != null)
                    memExp = unExp.Operand as MemberExpression;
                else
                    memExp = mcExp.Arguments[0] as MemberExpression;
                if (memExp == null)
                    throw new NotSupportedException("La expresion de ordenamiento no es valida");
                string prop = memExp.Member.Name;
                if(!_mapeoDb.ContainsKey(prop))
                    throw new ArgumentException(prop + " no es una propiedad de la entidad " + typeof(T).FullName);
                
                order.Append(" " + _mapeoDb[prop] + " " + direction + ",");
            }
            order.Remove(order.Length - 1, 1);
            return order.ToString();
        }

        private string FixNullOperand(string sql)
        {
            sql = sql.Replace("= NULL", "is NULL");
            return sql.Replace("!= NULL", "is not NULL");
        }

        private string ConditionExp2String(Expression exp)
        {


            if (exp is BinaryExpression)
            {
                var aux = (BinaryExpression)exp;
                string operador;
                if (IsBitWise(aux))
                {
                    if (aux.NodeType == ExpressionType.And)
                        operador = "BITAND";
                    else
                        operador = "BITOR";
                    return string.Format("{0}({1} , {2})", operador, ConditionExp2String(aux.Left), ConditionExp2String(aux.Right));

                }
                operador = GetOperandFromExpression(aux);
                return string.Format("( {0} {1} {2} )", ConditionExp2String(aux.Left), operador, ConditionExp2String(aux.Right));
            }

            if (exp is UnaryExpression)
            {
                var aux = (UnaryExpression)exp;
                if (aux.NodeType != ExpressionType.Not)
                    throw new ApplicationException("Nodo no reconocido " + aux.NodeType);
                return string.Format("NOT({0})", ConditionExp2String(aux.Operand));
            }

            if (exp is MemberExpression)
            {
                var aux = (MemberExpression)exp;

                if (IsColumn(aux))
                    return GetColumnName(aux);

                return GetConstant(aux);

            }

            if (exp is ConstantExpression || exp is MethodCallExpression)
            {
                return GetConstant(exp);
            }

            throw new NotSupportedException("Expresion no soportada " + exp.Type.Name);
        }



        private string AddQueryParam(object value)
        {
            string paranName = "p" + QueryParameters.Count;
            var p = new OracleParameter(paranName, GetParameterType(value.GetType()));
            p.Value = value;
            QueryParameters.Add(p);
            return ":" + paranName;

        }

        private string GetColumnName(MemberExpression expression)
        {
            string propName = expression.Member.Name;
            if (IsBool(expression.Member))
                return _mapeoDb[propName] + " = 'Y'";
            return _mapeoDb[propName];
        }

        private bool IsColumn(MemberExpression expression)
        {
            if (expression.Expression is ParameterExpression)
                return true;
            if ((expression.Expression is MemberExpression))
                return IsColumn((MemberExpression)expression.Expression);
            return false;
        }

        private bool IsBool(MemberInfo prop)
        {
            return ((PropertyInfo)prop).PropertyType.Equals(typeof(bool));
        }

        private bool IsBitWise(BinaryExpression exp)
        {
            return ((exp.NodeType == ExpressionType.And || exp.NodeType == ExpressionType.Or) &&
                    !exp.Type.Equals(typeof(bool)));
        }

        private string GetConstant(Expression exp)
        {
            var lambda = Expression.Lambda(exp);
            object value = lambda.Compile().DynamicInvoke();
            if (value == null)
                return "NULL";
            return AddQueryParam(value);
        }

        private string GetOperandFromExpression(Expression expression)
        {
            string operand;
            switch (expression.NodeType)
            {
                case ExpressionType.And:
                    operand = "AND";
                    break;
                case ExpressionType.AndAlso:
                    operand = "AND";
                    break;
                case ExpressionType.Equal:
                    operand = "=";
                    break;
                case ExpressionType.ExclusiveOr:
                    operand = "OR";
                    break;
                case ExpressionType.GreaterThan:
                    operand = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    operand = ">=";
                    break;
                case ExpressionType.Not:
                    operand = "NOT";
                    break;
                case ExpressionType.NotEqual:
                    operand = "<>";
                    break;
                case ExpressionType.Or:
                    operand = "OR";
                    break;
                case ExpressionType.OrElse:
                    operand = "OR";
                    break;
                default:
                    throw new NotImplementedException();
            }

            return operand;
        }


        public OracleDbType GetParameterType(Type type)
        {
            if (type.Equals(typeof(double)))
                return OracleDbType.Decimal;
            if (type.Equals(typeof(decimal)))
                return OracleDbType.Decimal;
            if (type.Equals(typeof(int)))
                return OracleDbType.Decimal;
            if (type.Equals(typeof(int?)))
                return OracleDbType.Decimal;
            if (type.Equals(typeof(string)))
                return OracleDbType.Char;
            if (type.Equals(typeof(char)))
                return OracleDbType.Char;
            if (type.Equals(typeof(char?)))
                return OracleDbType.Char;
            if (type.Equals(typeof(bool)))
                return OracleDbType.Char;
            if (type.Equals(typeof(bool?)))
                return OracleDbType.Char;
            if (type.Equals(typeof(DateTime)))
                return OracleDbType.Date;
            if (type.Equals(typeof(DateTime?)))
                return OracleDbType.Date;
            return OracleDbType.Decimal;
        }


    }
}
