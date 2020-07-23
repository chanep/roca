DROP VIEW AUTOPLANT.V_PIPING_FULL;

/* Formatted on 16/10/2014 10:10:26 (QP5 v5.215.12089.38647) */
CREATE OR REPLACE FORCE VIEW AUTOPLANT.V_PIPING_FULL
(
   PROJ_ID,
   COMP_ID,
   MODULE,
   CLASS,
   SORT_CODE,
   SHORT_DESC,
   SHOP_FLD,
   PIECE_MARK,
   TAG,
   LONG_DESCR,
   RUN_SIZE,
   MAIN_SIZE,
   BRAN_SIZE,
   FACING_1,
   SCHEDULE,
   PIPE_OD_M,
   RATING,
   MANU_FACT,
   GTYPE,
   STYPE,
   MATERIAL,
   MAT_GRADE,
   WEIGHT_DRY,
   WEIGHT_TOT,
   PIPE_LEN,
   CUT_LENGTH,
   EXCESS_LEN,
   COMP_LEN,
   COMP_LAYER,
   ELEVATION,
   INSTHICK,
   INSULATION,
   TAGNUMBER,
   MAT_MARK,
   SPEC,
   EXISTING,
   CATALOG,
   CTR_END_M,
   CTR_END_R,
   CTR_END_B,
   QTY_PERSET,
   BOLT_DIA,
   BOLT_LEN,
   PAINT_CODE,
   BOM_DESCR,
   CREATE_TM,
   MODIFY_TM,
   PROPERTY,
   ANN_MARK,
   TRACING,
   COG_LOADEDX,
   COG_LOADEDY,
   COG_LOADEDZ,
   WEIGHT_LOADED,
   COGX,
   COGY,
   COGZ,
   VOLUME,
   LINE,
   SERVICE,
   AREAID,
   AREA,
   SPOOL
)
AS
   SELECT p."PROJ_ID",
          p."COMP_ID",
          p."MODULE",
          p."CLASS",
          p."SORT_CODE",
          p."SHORT_DESC",
          p."SHOP_FLD",
          p."PIECE_MARK",
          p."TAG",
          p."LONG_DESCR",
          p."RUN_SIZE",
          p."MAIN_SIZE",
          p."BRAN_SIZE",
          p."FACING_1",
          p."SCHEDULE",
          p."PIPE_OD_M",
          p."RATING",
          p."MANU_FACT",
          p."GTYPE",
          p."STYPE",
          p."MATERIAL",
          p."MAT_GRADE",
          p."WEIGHT_DRY",
          p."WEIGHT_TOT",
          p."PIPE_LEN",
          p."CUT_LENGTH",
          p."EXCESS_LEN",
          p."COMP_LEN",
          p."COMP_LAYER",
          p."ELEVATION",
          p."INSTHICK",
          p."INSULATION",
          p."TAGNUMBER",
          p."MAT_MARK",
          p."SPEC",
          p."EXISTING",
          p."CATALOG",
          p."CTR_END_M",
          p."CTR_END_R",
          p."CTR_END_B",
          p."QTY_PERSET",
          p."BOLT_DIA",
          p."BOLT_LEN",
          p."PAINT_CODE",
          p."BOM_DESCR",
          p."CREATE_TM",
          p."MODIFY_TM",
          p."PROPERTY",
          p."ANN_MARK",
          p."TRACING",
          p."COG_LOADEDX",
          p."COG_LOADEDY",
          p."COG_LOADEDZ",
          p."WEIGHT_LOADED",
          p."COGX",
          p."COGY",
          p."COGZ",
          p."VOLUME",
          PR.PNUM Line,
          SR.NAME Service,
          A.Id AreaId,
          A.NAME Area,
          SP.NAME Spool
     FROM piping p
          INNER JOIN relationshipinstance r2
             ON     P.COMP_ID = R2.ID2
                AND P.PROJ_ID = R2.PROJ_ID
                AND R2.RELATIONSHIPTYPE = 2
          INNER JOIN process pr
             ON PR.KEYTAG = R2.ID1 AND PR.PROJ_ID = R2.PROJ_ID
          INNER JOIN relationshipinstance r3
             ON     P.COMP_ID = R3.ID2
                AND P.PROJ_ID = R3.PROJ_ID
                AND R3.RELATIONSHIPTYPE = 77
          INNER JOIN service3d sr
             ON sr.id = R3.ID1 AND sr.PROJ_ID = R3.PROJ_ID
          LEFT JOIN relationshipinstance r
             ON     P.COMP_ID = R.ID2
                AND P.PROJ_ID = R.PROJ_ID
                AND R.RELATIONSHIPTYPE = 18
          LEFT JOIN area3d a 
             ON R.ID1 = A.ID AND A.PROJ_ID = R.PROJ_ID
          LEFT JOIN relationshipinstance R4 
             on P.COMP_ID = R4.ID2 and P.PROJ_ID = R4.PROJ_ID and R4.RELATIONSHIPTYPE = 98
          LEFT JOIN spool sp 
             on R4.ID1 = sp.ID and sp.PROJ_ID = sp.PROJ_ID;




DROP VIEW AUTOPLANT.V_PIPING_FOREXPORT;

/* Formatted on 06/06/2014 16:29:44 (QP5 v5.215.12089.38647) */
CREATE OR REPLACE FORCE VIEW AUTOPLANT.V_PIPING_FOREXPORT
(
   PROJ_ID,
   AREAID,
   AREA,
   LINE,
   SERVICE,
   TAG,
   PIECE_MARK,
   QUANTITY,
   TOTALQUANTITY
)
AS
     SELECT P.PROJ_ID,
            P.AreaId,
            P.Area,
            P.Line,
            P.Service,
            P.TAG,
            P.PIECE_MARK,
            ROUND (SUM (roca.GetCantidad (P.CUT_LENGTH, P.QTY_PERSET) * (-1 * (P.EXISTING - 1))), 2) Quantity,
            ROUND (SUM (roca.GetCantidad (P.CUT_LENGTH, P.QTY_PERSET)), 2) TotalQuantity
       FROM V_PIPING_FULL P
   GROUP BY p.PROJ_ID,
            P.AreaId,
            P.Area,
            P.Line,
            P.Service,
            P.TAG,
            P.PIECE_MARK;


