Insert into ROCA.BAS_CLASSES
   (BAS_CLASS_ID, NAME)
 Values
   (3, 'Cno.Roca.BackEnd.Materials.Data.Materials.BasElementValve');
Insert into ROCA.BAS_CLASSES
   (BAS_CLASS_ID, NAME)
 Values
   (4, 'Cno.Roca.BackEnd.Materials.Data.Materials.BasElementEi');
Insert into ROCA.BAS_CLASSES
   (BAS_CLASS_ID, NAME)
 Values
   (5, 'Cno.Roca.BackEnd.Materials.Data.Materials.BasElementCable');
Insert into ROCA.BAS_CLASSES
   (BAS_CLASS_ID, NAME)
 Values
   (1, 'Cno.Roca.BackEnd.Materials.Data.Materials.BasElement');
Insert into ROCA.BAS_CLASSES
   (BAS_CLASS_ID, NAME)
 Values
   (2, 'Cno.Roca.BackEnd.Materials.Data.Materials.BasElementPiping');
COMMIT;



Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (1, 'p.pipe', 'Pipe', 2);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (2, 'p.fitting', 'Fitting', 2);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (3, 'p.flange', 'Flange', 2);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (4, 'p.gasket', 'Gasket', 2);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (5, 'p.stud-bolt', 'Stud-Bolt', 2);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (6, 'p.miscelaneo', 'Miscelaneo', 2);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (7, 'p.valve', 'Valve', 3);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (10, 'ei.canaliz', 'Canalizacion', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (11, 'ei.bandeja', 'Bandeja', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (12, 'ei.cableselec', 'Cable Electrico', 5);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (13, 'ei.cablesinst', 'Cable de Instrumentos', 5);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (14, 'ei.cablescom', 'Cable de Comunicaciones', 5);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (15, 'ei.terminal', 'Terminal', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (16, 'ei.prensa', 'Prensacable', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (17, 'ei.ilum', 'Iluminación', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (18, 'ei.tomas', 'Toma', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (19, 'ei.pat', 'PAT', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (20, 'ei.buloneria', 'Buloneria', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (21, 'ei.soporteria', 'Soportería', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (22, 'ei.catodica', 'Catódica', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (23, 'ei.tracing', 'Tracing', 4);
Insert into ROCA.BAS_ELEMENT_TYPES
   (BAS_ELEMENT_TYPE_ID, CODE, NAME, BAS_CLASS_ID)
 Values
   (24, 'ei.mecanicos', 'Material mecanico', 5);
COMMIT;




Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (157, 0, 0, 'p.pipe.desc', 'Descripcion', 
    4, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (158, 0, 1, 'p.pipe.estandar', 'Estandar', 
    3, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (159, 0, 2, 'p.pipe.serie1', 'Serie 1', 
    1, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (160, 0, 3, 'p.pipe.serie2', 'Serie 2', 
    1, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (161, 0, 4, 'p.pipe.material', 'Material', 
    3, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (162, 0, 5, 'p.pipe.extremo1', 'Extremo 1', 
    1, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (163, 0, 6, 'p.pipe.extremo2', 'Extremo 2', 
    1, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (164, 1, 7, 'p.pipe.dim1', 'Dim 1', 
    2, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (165, 1, 8, 'p.pipe.dim2', 'Dim 2', 
    3, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (166, 1, 9, 'p.pipe.sched1', 'Sched/Esp 1', 
    4, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (167, 1, 10, 'p.pipe.sched2', 'Sched/Esp 2', 
    4, 1);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (168, 0, 0, 'p.gasket.desc', 'Descripcion', 
    4, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (169, 0, 1, 'p.gasket.estandar', 'Estandar', 
    3, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (170, 0, 2, 'p.gasket.serie1', 'Serie 1', 
    1, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (171, 0, 3, 'p.gasket.serie2', 'Serie 2', 
    1, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (172, 0, 4, 'p.gasket.material', 'Material', 
    3, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (173, 0, 5, 'p.gasket.extremo1', 'Extremo 1', 
    1, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (174, 0, 6, 'p.gasket.extremo2', 'Extremo 2', 
    1, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (175, 1, 7, 'p.gasket.dim1', 'Dim 1', 
    2, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (176, 1, 8, 'p.gasket.dim2', 'Dim 2', 
    3, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (177, 1, 9, 'p.gasket.sched1', 'Sched/Esp 1', 
    4, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (178, 1, 10, 'p.gasket.sched2', 'Sched/Esp 2', 
    4, 4);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (179, 0, 0, 'p.stud-bolt.desc', 'Descripcion', 
    4, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (180, 0, 1, 'p.stud-bolt.estandar', 'Estandar', 
    3, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (181, 0, 2, 'p.stud-bolt.serie1', 'Serie 1', 
    1, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (182, 0, 3, 'p.stud-bolt.serie2', 'Serie 2', 
    1, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (183, 0, 4, 'p.stud-bolt.material', 'Material', 
    3, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (184, 0, 5, 'p.stud-bolt.extremo1', 'Extremo 1', 
    1, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (185, 0, 6, 'p.stud-bolt.extremo2', 'Extremo 2', 
    1, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (186, 1, 7, 'p.stud-bolt.dim1', 'Dim 1', 
    2, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (187, 1, 8, 'p.stud-bolt.dim2', 'Dim 2', 
    3, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (188, 1, 9, 'p.stud-bolt.sched1', 'Sched/Esp 1', 
    4, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (189, 1, 10, 'p.stud-bolt.sched2', 'Sched/Esp 2', 
    4, 5);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (190, 0, 0, 'p.miscelaneo.desc', 'Descripcion', 
    4, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (191, 0, 1, 'p.miscelaneo.estandar', 'Estandar', 
    3, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (192, 0, 2, 'p.miscelaneo.serie1', 'Serie 1', 
    1, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (193, 0, 3, 'p.miscelaneo.serie2', 'Serie 2', 
    1, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (194, 0, 4, 'p.miscelaneo.material', 'Material', 
    3, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (195, 0, 5, 'p.miscelaneo.extremo1', 'Extremo 1', 
    1, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (196, 0, 6, 'p.miscelaneo.extremo2', 'Extremo 2', 
    1, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (197, 1, 7, 'p.miscelaneo.dim1', 'Dim 1', 
    2, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (198, 1, 8, 'p.miscelaneo.dim2', 'Dim 2', 
    3, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (199, 1, 9, 'p.miscelaneo.sched1', 'Sched/Esp 1', 
    4, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (200, 1, 10, 'p.miscelaneo.sched2', 'Sched/Esp 2', 
    4, 6);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (201, 0, 0, 'p.fitting.desc', 'Descripcion', 
    4, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (202, 0, 1, 'p.fitting.estandar', 'Estandar', 
    3, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (203, 0, 2, 'p.fitting.serie1', 'Serie 1', 
    1, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (204, 0, 3, 'p.fitting.serie2', 'Serie 2', 
    1, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (205, 0, 4, 'p.fitting.material', 'Material', 
    3, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (206, 0, 5, 'p.fitting.extremo1', 'Extremo 1', 
    1, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (207, 0, 6, 'p.fitting.extremo2', 'Extremo 2', 
    1, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (208, 1, 7, 'p.fitting.dim1', 'Dim 1', 
    2, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (209, 1, 8, 'p.fitting.dim2', 'Dim 2', 
    3, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (210, 1, 9, 'p.fitting.sched1', 'Sched/Esp 1', 
    4, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (211, 1, 10, 'p.fitting.sched2', 'Sched/Esp 2', 
    4, 2);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (212, 0, 0, 'p.flange.desc', 'Descripcion', 
    4, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (213, 0, 1, 'p.flange.estandar', 'Estandar', 
    3, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (214, 0, 2, 'p.flange.serie1', 'Serie 1', 
    1, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (215, 0, 3, 'p.flange.serie2', 'Serie 2', 
    1, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (216, 0, 4, 'p.flange.material', 'Material', 
    3, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (217, 0, 5, 'p.flange.extremo1', 'Extremo 1', 
    1, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (218, 0, 6, 'p.flange.extremo2', 'Extremo 2', 
    1, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (219, 1, 7, 'p.flange.dim1', 'Dim 1', 
    2, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (220, 1, 8, 'p.flange.dim2', 'Dim 2', 
    3, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (221, 1, 9, 'p.flange.sched1', 'Sched/Esp 1', 
    4, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (222, 1, 10, 'p.flange.sched2', 'Sched/Esp 2', 
    4, 3);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (223, 0, 0, 'p.valve.desc', 'Descripcion', 
    4, 7);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (224, 0, 1, 'p.valve.estandar', 'Estandar', 
    2, 7);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (225, 0, 2, 'p.valve.serie', 'Serie', 
    2, 7);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (226, 0, 3, 'p.valve.extremo', 'Extremo', 
    2, 7);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (227, 0, 4, 'p.valve.operacion', 'Operacion', 
    1, 7);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (228, 10, 5, 'p.valve.material', 'Material', 
    2, 7);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (229, 0, 6, 'p.valve.trim', 'Trim', 
    2, 7);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (230, 0, 7, 'p.valve.sello', 'Sello', 
    2, 7);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (231, 0, 8, 'p.valve.observaciones', 'Observaciones', 
    2, 7);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (232, 1, 9, 'p.valve.diametro', 'Diametro', 
    2, 7);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (241, 0, 0, 'ei.canaliz.flia', 'Familia', 
    5, 10);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (242, 0, 1, 'ei.canaliz.material', 'Material', 
    2, 10);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (243, 0, 2, 'ei.canaliz.clasi', 'Clasificación de Área', 
    2, 10);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (244, 0, 3, 'ei.canaliz.extremos', 'Extremos', 
    1, 10);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (245, 0, 4, 'ei.canaliz.certificacion', 'Certificación', 
    1, 10);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (246, 1, 5, 'ei.canaliz.diametro1.dim1', 'Diámetro', 
    3, 10);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (247, 1, 6, 'ei.canaliz.diametro2.dim2', 'Diámetro reducción', 
    3, 10);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (248, 0, 7, 'ei.canaliz.reserva', 'Reserva', 
    9, 10);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (249, 0, 0, 'ei.bandeja.tipo', 'Tipo de bandeja', 
    3, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (250, 0, 1, 'ei.bandeja.canaliz', 'Elemento', 
    3, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (251, 0, 2, 'ei.bandeja.material', 'Material', 
    1, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (252, 0, 3, 'ei.bandeja.resist', 'Resistencia a la carga', 
    2, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (253, 0, 4, 'ei.bandeja.ala', 'Ala', 
    2, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (254, 0, 5, 'ei.bandeja.peld', 'Peldaños', 
    1, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (255, 0, 6, 'ei.bandeja.radio', 'Radio curvatura', 
    1, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (256, 1, 7, 'ei.bandeja.long.dim1', 'Longitud', 
    2, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (257, 1, 8, 'ei.bandeja.ancho1.dim2', 'Ancho mayor', 
    3, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (258, 1, 9, 'ei.bandeja.ancho2.dim3', 'Ancho menor', 
    3, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (259, 0, 10, 'ei.bandeja.reserva', 'Reserva', 
    5, 11);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (260, 0, 0, 'ei.cableselec.tipo', 'Tipo de cable', 
    3, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (261, 0, 1, 'ei.cableselec.norma', 'Norma constructiva', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (262, 0, 2, 'ei.cableselec.tension', 'Tension', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (263, 0, 3, 'ei.cableselec.aislacion', 'Aislación', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (264, 0, 4, 'ei.cableselec.categoria', 'Categoría', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (265, 0, 5, 'ei.cableselec.temp', 'Temperatura', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (266, 0, 6, 'ei.cableselec.recubrimiento', 'Cubierta exterior', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (267, 0, 7, 'ei.cableselec.conductor', 'Tipo de conductor', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (268, 0, 8, 'ei.cableselec.armadura', 'Armadura', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (269, 0, 9, 'ei.cableselec.pantalla', 'Pantalla', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (270, 0, 10, 'ei.cableselec.llama', 'No propagante de llama', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (271, 0, 11, 'ei.cableselec.propincendio', 'No propagante de Incendio', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (272, 0, 12, 'ei.cableselec.incendio', 'Resistente al Incendio', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (273, 0, 13, 'ei.cableselec.humos', 'Baja emisión de humos', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (274, 0, 14, 'ei.cableselec.halogeno', 'Libre de halógenos', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (275, 0, 15, 'ei.cableselec.uv', 'Resistente a rayos UV', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (276, 0, 16, 'ei.cableselec.hidrocarburos', 'Resistente a hidrocarburos', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (277, 0, 17, 'ei.cableselec.colorexterior', 'Color de cubierta', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (279, 0, 18, 'ei.cableselec.coloraislacion', 'Color de Aislación', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (278, 1, 19, 'ei.cableselec.formación', 'Formación', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (280, 1, 20, 'ei.cableselec.seccppal.dim1', 'Seccion Principal', 
    2, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (281, 1, 21, 'ei.cableselec.secsec.dim2', 'Seccion Secundaria', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (282, 0, 22, 'ei.cableselec.reserva', 'Reserva', 
    1, 12);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (283, 0, 0, 'ei.cablesinst.servicio', 'Servicio', 
    4, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (284, 0, 1, 'ei.cablesinst.tipo', 'Tipo de cable', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (285, 0, 2, 'ei.cablesinst.normas', 'Norma constructiva', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (286, 0, 3, 'ei.cablesinst.Conductor', 'Tipo de conductor', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (287, 0, 4, 'ei.cablesinst.caracteristicas', 'Características', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (288, 0, 5, 'ei.cablesinst.armadura', 'Armadura', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (289, 0, 6, 'ei.cablesinst.blindaje', 'Blindaje', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (290, 0, 7, 'ei.cablesinst.colorexterior', 'Color  de cubierta', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (291, 0, 8, 'ei.cablesinst.Identificacion', 'Identificacion de conductores', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (292, 0, 9, 'ei.cablesinst.llama', 'No propagante de llama', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (293, 0, 10, 'ei.cablesinst.propincendio', 'No propagante de Incendio', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (294, 0, 11, 'ei.cablesinst.incedio', 'Resistente al Incendio', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (295, 0, 12, 'ei.cablesinst.humos', 'Baja emisión de humos', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (296, 0, 13, 'ei.cablesinst.halogenos', 'Libre de halógenos', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (297, 0, 14, 'ei.cablesinst.uv', 'Resistente a rayos UV', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (298, 0, 15, 'ei.cablesinst.hidrocarburos', 'Resistente a hidrocarburos', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (299, 1, 16, 'ei.cablesinst.formacion.dim1', 'Formación', 
    1, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (300, 1, 17, 'ei.cablesinst.seccion.dim2', 'Seccion', 
    2, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (301, 0, 18, 'ei.cablesinst.reserva', 'Reserva', 
    4, 13);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (302, 0, 0, 'ei.cablescom.servicio', 'Servicio', 
    4, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (303, 0, 1, 'ei.cablescom.normas', 'Norma constructiva', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (304, 0, 2, 'ei.cablescom.Conductor', 'Tipo de conductor', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (305, 0, 3, 'ei.cablescom.caracteristicas', 'Características', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (306, 0, 4, 'ei.cablescom.armadura', 'Armadura', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (307, 0, 5, 'ei.cablescom.blindaje', 'Blindaje', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (308, 0, 6, 'ei.cablescom.colorexterior', 'Color  de cubierta', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (309, 0, 7, 'ei.cablescom.Identificacion', 'Identificacion de conductores', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (310, 0, 8, 'ei.cablescom.resist.elec.', 'Resistencia eléctrica', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (311, 0, 9, 'ei.cablescom.vel.propagacion', 'Velocidad de propagación', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (312, 0, 10, 'ei.cablescom.imped.caracteristica', 'Impedancia caract.', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (313, 0, 11, 'ei.cablescom.capacid.mutua', 'Capacidad mutua', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (314, 0, 12, 'ei.cablescom.llama', 'No propagante de llama', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (315, 0, 13, 'ei.cablescom.propincendio', 'No propagante de Incendio', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (316, 0, 14, 'ei.cablescom.incedio', 'Resistente al Incendio', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (317, 0, 15, 'ei.cablescom.humos', 'Baja emisión de humos', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (318, 0, 16, 'ei.cablescom.halogenos', 'Libre de halógenos', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (319, 0, 17, 'ei.cablescom.uv', 'Resistente a rayos UV', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (320, 0, 18, 'ei.cablescom.hidrocarburos', 'Resistente a hidrocarburos', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (321, 1, 19, 'ei.cablescom.formacion.dim1', 'Formación', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (322, 1, 20, 'ei.cablescom.seccion.dim2', 'Seccion', 
    2, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (323, 0, 21, 'ei.cablescom.reserva', 'Reserva', 
    1, 14);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (324, 0, 2, 'ei.terminal.flia', 'Servicio', 
    1, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (325, 0, 1, 'ei.terminal.material', 'Material', 
    1, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (326, 0, 3, 'ei.terminal.tension', 'Tension', 
    1, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (327, 0, 4, 'ei.terminal.ubic', 'Instalación', 
    1, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (328, 0, 5, 'ei.terminal.cables', 'Cantidad de cables', 
    1, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (329, 0, 6, 'ei.terminal.conexión', 'Tipo de conexión', 
    1, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (330, 0, 7, 'ei.terminal.bloq', 'Kit de bloqueo', 
    1, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (331, 0, 8, 'ei.terminal.incl', 'Posición', 
    1, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (332, 0, 0, 'ei.terminal.tipo', 'Tipo de Terminal', 
    6, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (333, 1, 9, 'ei.terminal.dimay.dim1', 'Sección mayor', 
    2, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (334, 1, 10, 'ei.terminal.dimen.dim2', 'Sección menor', 
    2, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (335, 1, 11, 'ei.terminal.diojal.dim3', 'Diámetro Ojal', 
    2, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (336, 0, 12, 'ei.terminal.reserva', 'Reserva', 
    6, 15);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (337, 0, 0, 'ei.prensa.tipo', 'Tipo', 
    3, 16);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (338, 0, 1, 'ei.prensa.material', 'Material', 
    1, 16);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (339, 0, 2, 'ei.prensa.clasificacion', 'Clasificación de Área', 
    1, 16);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (340, 0, 3, 'ei.prensa.cerramiento', 'Grado de protección', 
    1, 16);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (341, 0, 4, 'ei.prensa.pat', 'PAT', 
    1, 16);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (342, 0, 5, 'ei.prensa.rosca', 'Tipo de rosca', 
    1, 16);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (343, 1, 6, 'ei.prensa.acometida.dim1', 'Acometida', 
    1, 16);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (344, 1, 7, 'ei.prensa.diamext.dim2', 'Rango Diám. Bajo Armadura', 
    1, 16);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (345, 1, 8, 'ei.prensa.diambajoarmadura.dim3', 'Rango Diám. Exterior cable', 
    1, 16);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (346, 0, 9, 'ei.prensa.reserva', 'Reserva', 
    15, 16);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (347, 0, 0, 'ei.ilum.tipo', 'Tipo de artefacto', 
    4, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (348, 0, 1, 'ei.ilum.cant', 'Cant. de lámparas', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (349, 0, 2, 'ei.ilum.lampara', 'Tipo de lámpara', 
    2, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (350, 0, 3, 'ei.ilum.base', 'Tipo de base', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (351, 0, 4, 'ei.ilum.emerg', 'Equipo autónomo', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (352, 0, 5, 'ei.ilum.instalacion', 'Instalación', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (353, 0, 6, 'ei.ilum.clasificacion', 'Clasificación de Área', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (354, 0, 7, 'ei.ilum.temp', 'Clase de Temperatura', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (355, 0, 8, 'ei.ilum.cerramiento', 'Grado de protección', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (356, 0, 9, 'ei.ilum.montaje', 'Montaje', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (357, 0, 10, 'ei.ilum.accesorios', 'Accesorios', 
    2, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (358, 1, 11, 'ei.ilum.tension.dim1', 'Tension', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (359, 0, 12, 'ei.ilum.frecuencia', 'Frecuencia', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (360, 1, 13, 'ei.ilum.potencia.dim2', 'Potencia', 
    1, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (361, 0, 14, 'ei.ilum.reserva', 'Reserva', 
    7, 17);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (362, 0, 0, 'ei.tomas.familia', 'Familia', 
    3, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (363, 0, 1, 'ei.tomas.tipo', 'Tipo', 
    2, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (364, 0, 2, 'ei.tomas.instalacion', 'Instalación', 
    1, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (365, 0, 3, 'ei.tomas.material', 'Material', 
    1, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (366, 0, 4, 'ei.tomas.clasificacion', 'Clasificación de Área', 
    1, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (367, 0, 5, 'ei.tomas.temp', 'Clase de Temperatura', 
    1, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (368, 0, 6, 'ei.tomas.cerramiento', 'Grado de protección', 
    1, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (369, 0, 7, 'ei.tomas.polos', 'Polos', 
    1, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (370, 0, 8, 'ei.tomas.clavija', 'Tipo de clavija', 
    2, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (371, 0, 9, 'ei.tomas.montaje', 'Montaje', 
    1, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (372, 1, 10, 'ei.tomas.tensión.dim1', 'Tension', 
    1, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (373, 1, 11, 'ei.tomas.corriente.dim2', 'Corriente', 
    1, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (374, 1, 12, 'ei.tomas.proteccion.dim3', 'Corriente de Protección', 
    1, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (375, 0, 13, 'ei.tomas.reserva', 'Reserva', 
    9, 18);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (376, 0, 0, 'ei.pat.tipo', 'Tipo', 
    6, 19);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (377, 0, 1, 'ei.pat.material', 'Material', 
    2, 19);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (378, 1, 2, 'ei.pat.dim1', 'Rango cables', 
    3, 19);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (379, 0, 3, 'ei.pat.longitud', 'Longitud', 
    4, 19);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (380, 0, 4, 'ei.pat.reserva', 'Reserva', 
    11, 19);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (381, 0, 0, 'ei.buloneria.flia', 'Tipo', 
    5, 20);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (382, 0, 1, 'ei.buloneria.material', 'Material', 
    2, 20);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (383, 0, 2, 'ei.buloneria.rosca', 'Tipo de rosca', 
    3, 20);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (384, 0, 3, 'ei.buloneria.paso', 'Rosca', 
    5, 20);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (385, 1, 4, 'ei.buloneria.largo.dim1', 'Longitud', 
    3, 20);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (386, 1, 5, 'ei.buloneria.diamarandela.dim2', 'Diámetro', 
    3, 20);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (387, 0, 6, 'ei.buloneria.reserva', 'Reserva', 
    5, 20);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (388, 0, 0, 'ei.soporteria.flia', 'Tipo', 
    3, 21);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (389, 0, 2, 'ei.soporteria.material', 'Material', 
    2, 21);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (390, 0, 1, 'ei.soporteria.tipo', 'Perfil', 
    4, 21);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (391, 1, 3, 'ei.soporteria.diam.dim1', 'Diámetro', 
    3, 21);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (392, 1, 4, 'ei.soporteria.ancho.dim2', 'Ancho', 
    3, 21);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (393, 1, 5, 'ei.soporteria.espesor.dim3', 'Espesor', 
    3, 21);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (394, 0, 6, 'ei.soporteria.reserva', 'Reserva', 
    8, 21);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (395, 0, 0, 'ei.catodica.tipo', 'Tipo', 
    4, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (396, 0, 1, 'ei.catodica.materiales', 'Material', 
    2, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (397, 0, 2, 'ei.catodica.proteccion', 'Grado de protección', 
    1, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (398, 0, 3, 'ei.catodica.conexión', 'Tipo de conexión', 
    1, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (399, 0, 4, 'ei.catodica.corriente', 'Corriente de descarga', 
    2, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (400, 0, 5, 'ei.catodica.tension', 'Tensión', 
    2, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (401, 0, 6, 'ei.catodica.instalacion', 'Instalación', 
    2, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (402, 0, 7, 'ei.catodica.resistencia', 'Aislación', 
    1, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (403, 0, 8, 'ei.catodica.clasificacion', 'Clasificación de Área', 
    1, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (404, 0, 9, 'ei.catodica.cargas', 'Cargas', 
    2, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (405, 0, 10, 'ei.catodica.rangos', 'Rangos', 
    2, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (406, 0, 11, 'ei.catodica.reserva', 'Reserva', 
    6, 22);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (407, 0, 0, 'ei.tracing.flia', 'Tipo', 
    5, 23);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (408, 0, 1, 'ei.tracing.recubrimiento', 'Recubrimiento', 
    3, 23);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (409, 0, 2, 'ei.tracing.conductor', 'Conductor', 
    3, 23);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (410, 0, 3, 'ei.tracing.proteccion', 'Proteccion', 
    3, 23);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (411, 0, 4, 'ei.tracing.potencia', 'Potencia', 
    4, 23);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (412, 0, 5, 'ei.tracing.temptrabajo', 'Temperatura de trabajo', 
    1, 23);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (413, 0, 6, 'ei.tracing.tempminima', 'Temperatura mínima', 
    2, 23);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (414, 0, 7, 'ei.tracing.clasificacion', 'Grado de protección', 
    1, 23);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (415, 0, 8, 'ei.tracing.reserva', 'Reserva', 
    4, 23);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (416, 0, 0, 'ei.mecanicos.flia', 'Tipo', 
    4, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (417, 0, 1, 'ei.mecanicos.material', 'Material', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (418, 0, 2, 'ei.mecanicos.internos1', 'Internos 1', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (419, 0, 3, 'ei.mecanicos.internos2', 'Internos 2', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (420, 0, 4, 'ei.mecanicos.internos3', 'Internos 3', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (421, 0, 5, 'ei.mecanicos.clasificacion', 'Clasificación de Área', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (422, 0, 6, 'ei.mecanicos.Conexion1', 'Conexión 1', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (423, 1, 7, 'ei.mecanicos.Dim1', 'Diámetro', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (424, 0, 8, 'ei.mecanicos.Rosca1', 'Tipo de rosca', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (425, 0, 9, 'ei.mecanicos.Conexion2', 'Conexión 2', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (426, 1, 10, 'ei.mecanicos.Dim2', 'Diámetro', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (427, 0, 11, 'ei.mecanicos.Rosca2', 'Tipo de rosca', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (428, 1, 12, 'ei.mecanicos.Dim3', 'Conexión p/venteo-purga', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (429, 0, 13, 'ei.mecanicos.Rosca3', 'Tipo de rosca', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (430, 0, 14, 'ei.mecanicos.Accesorios', 'Accesorios', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (431, 0, 15, 'ei.mecanicos.Serie', 'Serie', 
    1, 24);
Insert into ROCA.BAS_FIELD_DEFINITIONS
   (BAS_FIELD_DEFINITION_ID, TYPE, FIELD_ORDER, CODE, NAME, 
    LENGTH, BAS_ELEMENT_TYPE_ID)
 Values
   (432, 0, 16, 'ei.mecanicos.reserva', 'Reserva', 
    7, 24);
COMMIT;






Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (1, 1);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (2, 1);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (3, 1);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (4, 1);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (5, 1);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (6, 1);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (7, 1);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (10, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (10, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (11, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (11, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (12, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (12, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (13, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (13, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (14, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (14, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (15, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (15, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (16, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (16, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (17, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (17, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (18, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (18, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (19, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (19, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (20, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (20, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (21, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (21, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (22, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (22, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (23, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (23, 3);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (24, 2);
Insert into ROCA.BAS_ELEMENT_TYPE_SPECIALTY
   (BAS_ELEMENT_TYPE_ID, SPECIALTY_ID)
 Values
   (24, 3);
COMMIT;

