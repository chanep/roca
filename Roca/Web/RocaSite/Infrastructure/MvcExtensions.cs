using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json.Converters;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public static class MvcExtensions
    {
        public static string GetPropertyName<TModel>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, object>> propertyNameExpr)
        {
            return ExpressionHelper.GetExpressionText(propertyNameExpr).Replace('.', '_');
        }


        public static MvcHtmlString TextBoxFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> propertyNameExpr, IDictionary<string, object> htmlAttributtes, ElementMode mode)
        {
            AddModeAttributes(htmlAttributtes, mode);
            return htmlHelper.TextBoxFor(propertyNameExpr, htmlAttributtes);
        }

        public static MvcHtmlString TextBoxFor<TModel, TProperty>
        (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> propertyNameExpr, object htmlAttributtes, ElementMode mode)
        {
            return htmlHelper.TextBoxFor(propertyNameExpr, ToDictionary(htmlAttributtes), mode);
        }




        public static MvcHtmlString DropDownListFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> propertyNameExpr, IEnumerable<SelectListItem> selectList,
                    string optionLabel, IDictionary<string, object> htmlAttributtes, ElementMode mode)
        {
            AddModeAttributesToDdl(htmlAttributtes, mode);
            if (mode != ElementMode.Editable)
            {
                var list = selectList as SelectList;
                if(list != null && list.SelectedValue != null)
                    optionLabel = null;
                RemoveOptions(ref selectList);
            }
            return htmlHelper.DropDownListFor(propertyNameExpr, selectList, optionLabel, htmlAttributtes);
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> propertyNameExpr,
                IEnumerable<SelectListItem> selectList,
                IDictionary<string, object> htmlAttributtes, ElementMode mode)
        {
            AddModeAttributesToDdl(htmlAttributtes, mode);

            if (mode != ElementMode.Editable)
            {
                RemoveOptions(ref selectList);
            }
            return htmlHelper.DropDownListFor(propertyNameExpr, selectList, htmlAttributtes);
        }



        public static MvcHtmlString DropDownListFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> propertyNameExpr,
                IEnumerable<SelectListItem> selectList,
                string optionLabel, object htmlAttributtes, ElementMode mode)
        {

            return htmlHelper.DropDownListFor(propertyNameExpr, selectList, optionLabel, ToDictionary(htmlAttributtes), mode);
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> propertyNameExpr,
                IEnumerable<SelectListItem> selectList,
                object htmlAttributtes, ElementMode mode)
        {

            return htmlHelper.DropDownListFor(propertyNameExpr, selectList, ToDictionary(htmlAttributtes), mode);
        }



        private static IDictionary<string, object> ToDictionary(object obj)
        {
            var dict = new Dictionary<string, object>();
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                dict.Add(propertyInfo.Name, propertyInfo.GetValue(obj, null));
            }
            return dict;
        }

        private static void AddModeAttributes(IDictionary<string, object> htmlAttributtes, ElementMode mode)
        {
            if (mode == ElementMode.ReadOnly)
                htmlAttributtes.Add("readonly", "true");
            else if (mode == ElementMode.Disabled)
            {
                htmlAttributtes.Add("readonly", "true");
                htmlAttributtes.Add("style", "color:#b8b8b8;");            
            }
                
        }

        private static void AddModeAttributesToDdl(IDictionary<string, object> htmlAttributtes, ElementMode mode)
        {
            if (mode == ElementMode.ReadOnly)
            {
                htmlAttributtes.Add("onfocus", "this.defaultIndex=this.selectedIndex;");
                htmlAttributtes.Add("onchange", "this.selectedIndex=this.defaultIndex;");
            }               
            else if (mode == ElementMode.Disabled)
            {
                htmlAttributtes.Add("readonly", "true");
                htmlAttributtes.Add("style", "color:#b8b8b8;");
            }
        }

        private static void RemoveOptions(ref IEnumerable<SelectListItem> selectList)
        {
            SelectList newList;
            var newItems = new List<object>();
            var list = selectList as SelectList;
            if (list != null && list.SelectedValue != null)
            {
                foreach (var item in list.Items)
                {
                    object value = null;
                    if (list.DataValueField != null)
                        value = item.GetType().GetProperty(list.DataValueField).GetValue(item, null);
                    else
                        value = item;
                    if (value.Equals(list.SelectedValue))
                        newItems.Add(item);
                }
                newList = new SelectList(newItems, list.DataValueField, list.DataTextField, list.SelectedValue);
                selectList = newList;
            }
        }
    }
}