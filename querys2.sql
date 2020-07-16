select * from project;

select rownum PartNo, x.* from
(select PR.PNUM Linea, ROCA.GETCOMPONENTSIZE(P.COMP_ID, P.PROJ_ID) DiamNom, P.SPEC Clase,
        P.INSULATION Aislacion, P.SHORT_DESC Componente, P.LONG_DESCR Descripcion, P.MATERIAL, P.RATING, P.SCHEDULE, P.PAINT_CODE Pintura, P.PIECE_MARK,
        round(SUM(roca.GetCantidad(P.CUT_LENGTH, P.QTY_PERSET)), 2) as cantidad
                                        

 from piping p
 inner join relationshipinstance r on P.COMP_ID = R.ID2 and P.PROJ_ID = R.PROJ_ID
inner join area3d a on R.ID1 = A.ID and R.RELATIONSHIPTYPE = 18 and A.PROJ_ID = R.PROJ_ID
inner join relationshipinstance r2 on P.COMP_ID = R2.ID2 and P.PROJ_ID = R2.PROJ_ID AND R2.RELATIONSHIPTYPE = 2
inner join process pr on PR.KEYTAG = R2.ID1 and PR.PROJ_ID = R2.PROJ_ID
where (P.PROJ_ID = '0002')  and A.NAME = '02'
 
 group by PR.PNUM, ROCA.GETCOMPONENTSIZE(P.COMP_ID, P.PROJ_ID), P.SPEC,
        P.INSULATION, P.SHORT_DESC, P.LONG_DESCR, P.MATERIAL, P.RATING, P.SCHEDULE, P.PAINT_CODE, P.PIECE_MARK,P.SORT_CODE
 order by PR.PNUM, P.SORT_CODE) x;
 
 
 select rownum PartNo, x.* from
(select ROCA.GETCOMPONENTSIZE(P.COMP_ID, P.PROJ_ID) DiamNom, P.SPEC Clase,
        P.INSULATION Aislacion, P.SHORT_DESC Componente, P.LONG_DESCR Descripcion, P.MATERIAL, P.RATING, P.SCHEDULE, P.PAINT_CODE Pintura, P.PIECE_MARK,
        round(SUM(roca.GetCantidad(P.CUT_LENGTH, P.QTY_PERSET)), 2) as cantidad
                                        

 from piping p
 inner join relationshipinstance r on P.COMP_ID = R.ID2 and P.PROJ_ID = R.PROJ_ID
inner join area3d a on R.ID1 = A.ID and R.RELATIONSHIPTYPE = 18 and A.PROJ_ID = R.PROJ_ID
inner join relationshipinstance r2 on P.COMP_ID = R2.ID2 and P.PROJ_ID = R2.PROJ_ID AND R2.RELATIONSHIPTYPE = 2
inner join process pr on PR.KEYTAG = R2.ID1 and PR.PROJ_ID = R2.PROJ_ID
where (P.PROJ_ID = '0002')  and A.NAME = '02'
 
 group by ROCA.GETCOMPONENTSIZE(P.COMP_ID, P.PROJ_ID), P.SPEC,
        P.INSULATION, P.SHORT_DESC, P.LONG_DESCR, P.MATERIAL, P.RATING, P.SCHEDULE, P.PAINT_CODE, P.PIECE_MARK,P.SORT_CODE
 order by P.SORT_CODE) x;
 
 
 select rownum, p.* from piping p where rownum <= 300;
 
        