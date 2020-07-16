select * from piping p inner join relationshipinstance r on P.COMP_ID = R.ID2 where P.PROJ_ID = 7 and P.PIECE_MARK = 'PIP30'

select * from piping p  where P.PROJ_ID = 7 and P.PIECE_MARK = 'PIP30'

select *from piping p 
inner join relationshipinstance r on P.COMP_ID = R.ID2
inner join area3d a on R.ID1 = A.ID and R.RELATIONSHIPTYPE = 18 
inner join relationshipinstance r2 on P.COMP_ID = R2.ID2
inner join PROCESS PR on R2.ID1 = PR.KEYTAG and R2.RELATIONSHIPTYPE = 2 and PR.PROJ_ID = 7
--inner join tag_reg tr on TR.KEYTAG = R2.ID1 and TR.TAG_TYPE = 'AT_PROCESS' and TR.PROJ_ID = 7
where P.PROJ_ID = 7 and A.NAME = '03' and P.PIECE_MARK = 'PIP30'

select P.PROJ_ID from project p where P.ITEM= 'ProjectName' and dbms_lob.substr( P.DESCRIPT,100,1) = 'Prueba'

select * from piping
where PROJ_ID = 7