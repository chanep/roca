SET DEFINE OFF;
Insert into SPECIALTIES
   (SPECIALTY_ID, ABBREVIATION, NAME)
 Values
   (4, 'C', 'Civil');
Insert into SPECIALTIES
   (SPECIALTY_ID, ABBREVIATION, NAME)
 Values
   (5, 'M', 'Mecanica');
Insert into SPECIALTIES
   (SPECIALTY_ID, ABBREVIATION, NAME)
 Values
   (6, 'P', 'Procesos');
Insert into SPECIALTIES
   (SPECIALTY_ID, ABBREVIATION, NAME)
 Values
   (7, 'G', 'Ductos');
Insert into SPECIALTIES
   (SPECIALTY_ID, ABBREVIATION, NAME)
 Values
   (8, 'COO', 'Coordinacion');
Insert into SPECIALTIES
   (SPECIALTY_ID, ABBREVIATION, NAME)
 Values
   (9, 'PLA', 'Planificacion');
Insert into SPECIALTIES
   (SPECIALTY_ID, ABBREVIATION, NAME)
 Values
   (1, 'T', 'Piping');
Insert into SPECIALTIES
   (SPECIALTY_ID, ABBREVIATION, NAME)
 Values
   (2, 'E', 'Electricidad');
Insert into SPECIALTIES
   (SPECIALTY_ID, ABBREVIATION, NAME)
 Values
   (3, 'I', 'Instrum. y Ctrl.');
Insert into SPECIALTIES
   (SPECIALTY_ID, ABBREVIATION, NAME)
 Values
   (10, 'GEN', 'General');
COMMIT;


Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (1, 'ODEBRECHT\ecanepa', 'Esteban', 'Canepa', 'ecanepa@odebrecht.com', 
    'SuperAdmin,Admin,Read,Write,BasAdmin,BasWrite', 'ESC');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (2, 'WinXpDev\Administrador', 'Test1', 'Canepa', 'ecanepa@odebrecht.com', 
    'SuperAdmin,Admin,Read,Write,BasAdmin,BasWrite', 'ESC');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (3, 'HOME\e', 'Test2', 'Canepa', 'ecanepa@odebrecht.com', 
    'SuperAdmin,Admin,Read,Write,BasAdmin,BasWrite', 'ESC');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (10, 'ODEBRECHT\afelippo', 'Ariel', 'Felippo', 'afelippo@odebrecht.com', 
    'Leader,Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (11, 'ODEBRECHT\cbuccieri', 'Carina', 'Buccieri', 'cbuccieri@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (12, 'ODEBRECHT\amassun', 'Agustin', 'Massun', 'amassun@odebrecht.com', 
    'Leader,Read,Write,Admin', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (13, 'ODEBRECHT\calonso', 'Carlos', 'Alonso', 'calonso@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (14, 'ODEBRECHT\jcongil', 'Javier', 'Congil', 'jcongil@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (15, 'ODEBRECHT\federicogarcia', 'Federico', 'Garcia', 'federicogarcia@odebrecht.com', 
    'Read,Write,BasWrite,BasAdmin', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (16, 'ODEBRECHT\mbenjamin', 'Maximiliano', 'Benjamin', 'mbenjamin@odebrecht.com', 
    'Leader,Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (17, 'ODEBRECHT\gserroels', 'German', 'Serroels', 'gserroels@odebrecht.com', 
    'Leader,Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (18, 'ODEBRECHT\mirandan', 'Nazareno', 'Miranda', 'mirandan@odebrecht.com', 
    'Leader,Read,Write,BasWrite,BasAdmin', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (19, 'ODEBRECHT\emarkow', 'Eduardo', 'Markow', 'emarkow@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (20, 'ODEBRECHT\fdiez', 'Felipe', 'Diez', 'fdiez@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (21, 'ODEBRECHT\aabraham', 'Alejandro', 'Abraham', 'aabraham@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (22, 'ODEBRECHT\raulaiz', 'Raúl', 'Aiz', 'raulaiz@odebrecht.com', 
    'Leader,Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (23, 'ODEBRECHT\jenniferpereira', 'Jennifer', 'Pereira', 'jenniferpereira@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (24, 'ODEBRECHT\escalantem', 'Miguel', 'Escalante', 'escalantem@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (25, 'ODEBRECHT\jmenendez', 'Jorge', 'Menendez', 'jmenendez@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (26, 'ODEBRECHT\asoldatti', 'Adriano', 'Soldatti', 'asoldatti@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (27, 'ODEBRECHT\jinfantino', 'Jorge', 'Infantino', 'jinfantino@odebrecht.com', 
    'Leader,Read,Write,BasWrite,BasAdmin', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (28, 'ODEBRECHT\aavondoglio', 'Alejandro', 'Avondoglio', 'aavondoglio@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (29, 'ODEBRECHT\ipalladino', 'Ignacio', 'Palladino', 'ipalladino@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (30, 'ODEBRECHT\mromano', 'Matias', 'Romano', 'mromano@odebrecht.com', 
    'Read,Write,BasWrite,BasAdmin', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (31, 'ODEBRECHT\bortiz', 'Bernardo', 'Ortiz', 'bortiz@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (32, 'ODEBRECHT\gbalbachan', 'Gaston', 'Balbachan', 'gbalbachan@odebrecht.com', 
    'Leader,Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (33, 'ODEBRECHT\mmazzulli', 'Martín', 'Mazzulli', 'mmazzulli@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (34, 'ODEBRECHT\hegitto', 'Horacio', 'Egitto', 'hegitto@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (35, 'ODEBRECHT\saversa', 'Sebastian', 'Aversa', 'saversa@odebrecht.com', 
    'Leader,Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (36, 'ODEBRECHT\mpalomino', 'Mercedes', 'Palomino', 'mpalomino@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (37, 'ODEBRECHT\gpitisano', 'German', 'Pitisano', 'gpitisano@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (38, 'ODEBRECHT\lichtenbaum', 'Uriel', 'Lichtenbaum', 'lichtenbaum@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (39, 'ODEBRECHT\dmompo', 'Diego', 'Mompo', 'dmompo@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (40, 'ODEBRECHT\ggallo', 'Gonzalo', 'Gallo', 'ggallo@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (41, 'ODEBRECHT\nscalone', 'Nancy', 'Scalone', 'nscalone@odebrecht.com', 
    'Leader,Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (42, 'ODEBRECHT\dandino', 'Daniela', 'Andino', 'dandino@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (43, 'ODEBRECHT\gcalaili', 'Guillermina', 'Calaili', 'gcalaili@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (44, 'ODEBRECHT\rodolfoayres', 'Rodolfo', 'Ayres', 'rodolfoayres@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (45, 'ODEBRECHT\cfiumara', 'Claudio', 'Fiumara', 'cfiumara@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (46, 'ODEBRECHT\fernandezg', 'Guadalupe', 'Fernandez', 'fernandezg@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (47, 'ODEBRECHT\jcataldi', 'Jorge', 'Cataldi', 'jcataldi@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (48, 'ODEBRECHT\prossi', 'Pablo', 'Rossi', 'prossi@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (49, 'ODEBRECHT\bellora', 'Marcelo', 'Bellora', 'bellora@odebrecht.com', 
    'Leader,Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (50, 'ODEBRECHT\jpalvarez', 'Juan Pablo', 'Alvarez', 'jpalvarez@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (51, 'ODEBRECHT\cgbowler', 'Gaston', 'Bowler', 'cgbowler@odebrecht.com', 
    'Admin,Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (52, 'ODEBRECHT\pabloisla', 'Pablo', 'Isla', 'pabloisla@odebrecht.com', 
    'Read,Write,Leader', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (53, 'ODEBRECHT\janzola', 'Jairo', 'Anzola', 'janzola@odebrecht.com', 
    'Read,Write,Leader', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (54, 'ODEBRECHT\ugarofalo', 'Uberto', 'Garofalo', 'ugarofalo@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (55, 'ODEBRECHT\fbruzone', 'Francisco', 'Bruzone', 'fbruzone@odebrecht.com', 
    'Read,Write,BasWrite', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (56, 'ODEBRECHT\frebak', 'Felipe', 'Rebak', 'frebak@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (57, 'ODEBRECHT\alzugaray', 'Eugenio', 'Alzugaray', 'alzugaray@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (58, 'ODEBRECHT\rbonanno', 'Roberto', 'Bonanno', 'rbonanno@odebrecht.com', 
    'Read,Write,BasWrite,BasAdmin', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (59, 'ODEBRECHT\jmrincon', 'Juan Manuel', 'Rincon', 'jmrincon@odebrecht.com', 
    'Read,Write,BasWrite,BasAdmin', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (60, 'ODEBRECHT\mtisminetzky', 'Mauro', 'Tisminetzky', 'mtisminetzky@odebrecht.com', 
    'Read,Write', 'XX');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (61, 'ODEBRECHT\dtoth', 'Daniel', 'Toth', 'dtoth@odebrecht.com', 
    'Read,Write', 'DT');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (62, 'ODEBRECHT\emevangelista', 'Elisa Mabel', 'Evangelista', 'emevangelista@odebrecht.com', 
    'Read,Write', 'EME');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (63, 'ODEBRECHT\nheredia', 'Nicolas', 'Heredia', 'nheredia@odebrecht.com', 
    'Read,Write', 'NH');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (64, 'ODEBRECHT\pamestoy', 'Pablo', 'Amestoy', 'pamestoy@odebrecht.com', 
    'Read,Write', 'PA');
Insert into ROCA.USERS
   (ID, USER_NAME, NAME, LAST_NAME, MAIL, 
    ROLES, INITIALS)
 Values
   (65, 'ODEBRECHT\martinniz', 'Martin', 'Niz', 'martinniz@odebrecht.com', 
    'Read,Write', 'MN');
COMMIT;










SET DEFINE OFF;
Insert into ROCA.UNITS
   (UNIT_ID, ABBREVIATION, NAME)
 Values
   (1, 'u', 'Unidades');
Insert into ROCA.UNITS
   (UNIT_ID, ABBREVIATION, NAME)
 Values
   (2, 'mm', 'Milimitetros');
Insert into ROCA.UNITS
   (UNIT_ID, ABBREVIATION, NAME)
 Values
   (3, 'm', 'Metros');
COMMIT;

Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (53, 'PC Manantiales Behr', 'PCMB', 'PC Manantiales', 52);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME)
 Values
   (54, 'PC Pico Truncado', 'PCPT', 'PC Pico Truncado');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (49, 'Miraflores - Lumbreras', 'N38', 'N38', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (50, 'Aldao - Santa Fe', 'N34c', 'N34c', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (51, 'Mercedes - Cardales', 'S66', 'S66', 21);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME)
 Values
   (52, 'PC Manantiales Behr', 'PCMB', 'PC Manantiales');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (55, 'PC Pico Truncado', 'PCPT', 'PC Pico Truncado', 54);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME)
 Values
   (56, 'PC Garayalde', 'PCGA', 'PC Garayalde');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (57, 'PC Garayalde', 'PCGA', 'PC Garayalde', 56);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME)
 Values
   (58, 'PC Buchanan', 'PCBU', 'PC Buchanan');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (59, 'PC Buchanan', 'PCBU', 'PC Buchanan', 58);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, SUBPROJECT_TYPE)
 Values
   (1, 'Poliducto Pascuales Cuenca', 'PEQ001', 'Poliducto', 'Planta');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (2, 'Pascuales', '01', 'Pascuales', 1);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (3, 'Chorrillo', '02', 'Chorrillo', 1);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (4, 'General', '00', 'General', 1);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (5, 'Poliducto', '08', 'Poliducto', 1);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME)
 Values
   (6, 'PC Pichanal', 'PIC', 'Pichanal');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (7, 'PC Pichanal', 'PIC', 'Pichanal', 6);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME)
 Values
   (8, 'PC Beazley', 'BEA', 'Beazley');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (9, 'PC Beazley', 'BEA', 'Beazley', 8);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME)
 Values
   (1000, 'No Imputable', 'NO_IMP', 'No Imputab.');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (1001, 'No Imputable', 'NO_IMP', 'No Imputab.', 1000);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (48, 'La Mora-Beazley', 'N36', 'N36', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, SUBPROJECT_TYPE)
 Values
   (20, 'Gasoducto Norte', 'GASO_N', 'Gaso Norte', 'Tramo');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, SUBPROJECT_TYPE)
 Values
   (21, 'Gasoducto Sur', 'GASO_S', 'Gaso Sur', 'Tramo');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (22, 'Tramos Generales ', 'N00', 'N00', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (23, 'Campo Duran -Pichanal ', 'N02', 'N02', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (24, 'Miraflores-Lumbreras', 'N05a', 'N05a', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (25, 'Tucuman-Lavalle', 'N09a', 'N09a', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (26, 'Lavalle - Recreo', 'N10a', 'N10a', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (27, 'Recreo - Deán Funes', 'N11', 'N11', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (28, 'Recreo-Dean Funes', 'N11a', 'N11a', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (29, 'Dean Funes - Ferreyra', 'N13a', 'N13a', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (30, 'Ferreyra - Tio Pujio', 'N13b', 'N13b', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (31, 'Dean Funes - Ferreyra', 'N13c', 'N13c', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (32, 'Ferreyra-Tio Pujio', 'N13d', 'N13d', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (33, 'Tio Pujio -Leones', 'N14a', 'N14a', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (34, 'San Jerónimo - VN73', 'N33', 'N33', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (35, 'San Jerónimo - Aldao', 'N34a', 'N34a', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (36, 'Aldao - Santa Fe', 'N34b', 'N34b', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (37, 'Leones - San Jerónimo', 'N35', 'N35', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (38, 'Leones - San Jeronimo ', 'N35a', 'N35a', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (39, 'Beazley-La Paz', 'N37', 'N37', 20);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (40, 'Desc. PC Pico Truncado II', 'S04a', 'S04a', 21);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (41, 'Desc. PC Manantiales Behr II', 'S06a', 'S06a', 21);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (42, 'Desc. PC Dolavon', 'S09a', 'S09a', 21);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (43, 'Desc. PC San Antonio Oeste II', 'S16a', 'S16a', 21);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (44, 'Desc. PC Saturno', 'S31a', 'S31a', 21);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (45, 'Desc. PC Magallanes', 'S39', 'S39', 21);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (46, 'Desc. PC Magallanes', 'S40', 'S40', 21);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (47, 'Desc. PC Garayalde', 'S42', 'S42', 21);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (102, 'Apoyo a Gaso.', 'APG', 'Apoyo Gaso.', 100);
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, SUBPROJECT_TYPE)
 Values
   (100, 'Gasoducto Sur Peruano', 'GSP', 'GSP', 'Planta');
Insert into ROCA.PROJECTS
   (PROJECT_ID, NAME, CODE, SHORT_NAME, PARENT_ID)
 Values
   (101, 'PC Ticumpinia', 'PCT', 'PC Ticumpinia', 100);
COMMIT;






Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (61, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (1, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (2, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (2, 2);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (2, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (3, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (3, 2);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (3, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (10, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (11, 5);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (12, 8);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (13, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (14, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (15, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (16, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (17, 8);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (18, 2);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (19, 2);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (20, 2);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (21, 2);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (22, 4);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (23, 4);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (24, 4);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (25, 4);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (26, 4);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (27, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (28, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (29, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (30, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (31, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (32, 5);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (33, 5);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (34, 5);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (35, 6);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (36, 6);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (37, 6);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (38, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (39, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (40, 3);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (41, 7);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (42, 7);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (43, 7);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (44, 7);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (45, 6);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (46, 7);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (47, 7);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (48, 8);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (49, 8);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (50, 8);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (51, 9);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (52, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (53, 8);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (54, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (55, 7);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (56, 1);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (57, 4);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (58, 2);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (59, 2);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (60, 6);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (62, 6);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (63, 2);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (64, 4);
Insert into ROCA.USER_SPECIALTY
   (USER_ID, SPECIALTY_ID)
 Values
   (65, 6);
COMMIT;




Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (1, 'mlPurpose', 'PA', 'PARA APROBACION');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (2, 'mlPurpose', 'PC', 'PARA CUCAMONIZACION');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (3, 'caTarea', 'Coordinación', 'Coordinación (Gestión / seguimiento / reportes)');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (4, 'caTarea', 'Reuniones', 'Reuniones (Reuniones de coordinación / Revisión interdisciplinaria)');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (5, 'caTarea', 'NoEmisibles', 'Elaborados no emisibles');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (6, 'caTarea', 'DP', 'DP (Calificación documentos de Proveedor / gestión con proveedores / KOM)');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (7, 'caTareaObsoleto', 'Viajes', 'Viajes');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (8, 'caTarea', 'Soporte', 'Soporte (Apoyo a otras disciplinas)');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (9, 'caTarea', 'Capacitaciones', 'Capacitaciones, Presentaciones, Congresos');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (10, 'caTarea', 'Vacaciones', 'Vacaciones');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (11, 'caTarea', 'Licencia', 'Licencia, Permisos, Feriados');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (12, 'caTarea', 'CAOs', 'CAOs');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (13, 'caTarea', 'OtrosProyectos', 'Apoyo a otros Proyectos');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (14, 'caTareaObsoleto', 'Permisos', 'Gestion de Permisos de Paso/Cruces');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (15, 'caTarea', 'Soporte2', 'Soporte (Apoyo a otras areas)');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (16, 'caTarea', 'Maqueta', 'Maqueta');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (17, 'caTarea', 'Informes', 'Informes y Reportes');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (20, 'tipoDoc', 'BD', 'BD');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (21, 'tipoDoc', 'DB', 'DB');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (22, 'tipoDoc', 'DE', 'DE');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (23, 'tipoDoc', 'ES', 'ES');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (24, 'tipoDoc', 'ET', 'ET');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (25, 'tipoDoc', 'FD', 'FD');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (26, 'tipoDoc', 'IP', 'IP');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (27, 'tipoDoc', 'IS', 'IS');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (28, 'tipoDoc', 'LI', 'LI');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (29, 'tipoDoc', 'MA', 'MA');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (30, 'tipoDoc', 'MC', 'MC');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (31, 'tipoDoc', 'MD', 'MD');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (32, 'tipoDoc', 'PR', 'PR');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (33, 'tipoDoc', 'PT', 'PT');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (34, 'tipoDoc', 'RL', 'RL');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (35, 'tipoDoc', 'RM', 'RM');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (100, 'tipoDoc', 'AP', 'AP');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (101, 'tipoDoc', 'DC', 'DC');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (102, 'tipoDoc', 'DF', 'DF');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (103, 'tipoDoc', 'DL', 'DL');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (104, 'tipoDoc', 'DM', 'DM');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (105, 'tipoDoc', 'FU', 'FU');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (106, 'tipoDoc', 'HD', 'HD');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (107, 'tipoDoc', 'IE', 'IE');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (108, 'tipoDoc', 'IF', 'IF');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (109, 'tipoDoc', 'KP', 'KP');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (110, 'tipoDoc', 'LC', 'LC');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (111, 'tipoDoc', 'LD', 'LD');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (112, 'tipoDoc', 'LE', 'LE');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (113, 'tipoDoc', 'LG', 'LG');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (114, 'tipoDoc', 'LH', 'LH');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (115, 'tipoDoc', 'LM', 'LM');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (116, 'tipoDoc', 'LS', 'LS');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (117, 'tipoDoc', 'LT', 'LT');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (118, 'tipoDoc', 'LV', 'LV');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (119, 'tipoDoc', 'LY', 'LY');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (120, 'tipoDoc', 'LL', 'LL');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (121, 'tipoDoc', 'PI', 'PI');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (122, 'tipoDoc', 'PL', 'PL');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (123, 'tipoDoc', 'PN', 'PN');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (124, 'tipoDoc', 'SP', 'SP');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (125, 'tipoDoc', 'TI', 'TI');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (126, 'tipoDoc', 'UN', 'UN');
Insert into ROCA.LOOKUPS
   (LOOKUP_ID, TYPE, CODE, VALUE)
 Values
   (10001, 'caTarea', 'Evaluaciones', 'Actividades Organizacionales (Evaluaciones)');
COMMIT;









