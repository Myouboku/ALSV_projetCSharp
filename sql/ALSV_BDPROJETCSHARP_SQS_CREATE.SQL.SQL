/*
 ----------------------------------------------------------------------------
             Génération d'une base de données pour
                        SQL Server 2005
                       (24/2/2022 10:36:06)
 ----------------------------------------------------------------------------
     Nom de la base : ALSV_BDProjetCSharp
     Projet : 
     Auteur : LEFEBVRE Guillaume
     Date de dernière modification : 24/2/2022 10:32:04
 ----------------------------------------------------------------------------
*/

drop database ALSV_BDProjetCSharp
go

/* -----------------------------------------------------------------------------
      OUVERTURE DE LA BASE ALSV_BDProjetCSharp
----------------------------------------------------------------------------- */

create database ALSV_BDProjetCSharp
go

use ALSV_BDProjetCSharp
go



/* -----------------------------------------------------------------------------
      TABLE : MEDICAMENT
----------------------------------------------------------------------------- */

create table MEDICAMENT
  (
     MED_ID smallint identity (1, 1)   ,
     LAB_ID smallint  not null  ,
     ASP_ID smallint  not null  ,
     UNI_ID smallint  not null  ,
     MED_NOM varchar(38)  not null  ,
     MED_PHOTO image  not null  ,
     MED_PRIX real  not null  ,
     MED_QUANTITE real  not null  ,
     MED_NB_ENTITES smallint  not null  
     ,
     constraint PK_MEDICAMENT primary key (MED_ID)
  ) 
go



/*      INDEX DE MEDICAMENT      */



/* -----------------------------------------------------------------------------
      TABLE : UTILISATEUR
----------------------------------------------------------------------------- */

create table UTILISATEUR
  (
     UTI_ID smallint identity (1, 1)   ,
     UTI_NOM varchar(38)  not null  ,
     UTI_PRENOM varchar(38)  not null  ,
     UTI_LOGIN varchar(38)  not null  ,
     UTI_MDP varchar(128)  not null  
     ,
     constraint PK_UTILISATEUR primary key (UTI_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : ASPECT
----------------------------------------------------------------------------- */

create table ASPECT
  (
     ASP_ID smallint identity (1, 1)   ,
     ASP_LIBELLE varchar(20)  not null  
     ,
     constraint PK_ASPECT primary key (ASP_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : UNITE
----------------------------------------------------------------------------- */

create table UNITE
  (
     UNI_ID smallint identity (1, 1)   ,
     UNI_LIBELLE varchar(128)  not null  
     ,
     constraint PK_UNITE primary key (UNI_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : LABORATOIRE
----------------------------------------------------------------------------- */

create table LABORATOIRE
  (
     LAB_ID smallint identity (1, 1)   ,
     LAB_NOM varchar(38)  not null  
     ,
     constraint PK_LABORATOIRE primary key (LAB_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      REFERENCES SUR LES TABLES
----------------------------------------------------------------------------- */



alter table MEDICAMENT 
     add constraint FK_MEDICAMENT_LABORATOIRE foreign key (LAB_ID) 
               references LABORATOIRE (LAB_ID)
go




alter table MEDICAMENT 
     add constraint FK_MEDICAMENT_ASPECT foreign key (ASP_ID) 
               references ASPECT (ASP_ID)
go




alter table MEDICAMENT 
     add constraint FK_MEDICAMENT_UNITE foreign key (UNI_ID) 
               references UNITE (UNI_ID)
go




/*
 -----------------------------------------------------------------------------
               FIN DE GENERATION
 -----------------------------------------------------------------------------
*/