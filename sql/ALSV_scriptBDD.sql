/*
 ----------------------------------------------------------------------------
             Génération d'une base de données pour
                        SQL Server 2005
                       (24/3/2022 11:36:13)
 ----------------------------------------------------------------------------
     Nom de la base : ALSV_BDDCSHARP
     Projet : 
     Auteur : LEFEBVRE Guillaume
     Date de dernière modification : 24/3/2022 11:35:50
 ----------------------------------------------------------------------------
*/

drop database ALSV_BDDCSHARP
go

/* -----------------------------------------------------------------------------
      OUVERTURE DE LA BASE ALSV_BDDCSHARP
----------------------------------------------------------------------------- */

create database ALSV_BDDCSHARP
go

use ALSV_BDDCSHARP
go



/* -----------------------------------------------------------------------------
      TABLE : MEDICAMENT
----------------------------------------------------------------------------- */

create table MEDICAMENT
  (
     MED_ID smallint identity (1, 1)   ,
     LAB_ID smallint  not null  ,
     CON_ID smallint  not null  ,
     ASP_ID smallint  not null  ,
     UNI_ID smallint  not null  ,
     EFF_ID smallint  not null  ,
     MED_NOM varchar(38)  not null  ,
     MED_PHOTO image  null  ,
     MED_PRIX real  not null  ,
     MED_QUANTITE real  not null  ,
     MED_NB_ENTITES smallint  not null  
     ,
     constraint PK_MEDICAMENT primary key (MED_ID)
  ) 
go



/*      INDEX DE MEDICAMENT      */



/* -----------------------------------------------------------------------------
      TABLE : AVIS
----------------------------------------------------------------------------- */

create table AVIS
  (
     AVI_ID smallint identity (1, 1)   ,
     AVI_TEXTE text  null  
     ,
     constraint PK_AVIS primary key (AVI_ID)
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
      TABLE : EFFET_SECONDAIRE
----------------------------------------------------------------------------- */

create table EFFET_SECONDAIRE
  (
     EFF_ID smallint identity (1, 1)   ,
     EFF_LIBELLE varchar(128)  not null  
     ,
     constraint PK_EFFET_SECONDAIRE primary key (EFF_ID)
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
      TABLE : PRATICIEN
----------------------------------------------------------------------------- */

create table PRATICIEN
  (
     PRA_ID smallint identity (1, 1)   ,
     DIS_ID smallint  not null  ,
     PRA_NOM varchar(38)  not null  ,
     PRA_PRENOM varchar(38)  not null  ,
     PRA_LOGIN varchar(50)  not null  ,
     PRA_MDP varchar(128)  not null  ,
     PRA_CP int  not null  ,
     PRA_ADRESSE varchar(50)  not null  ,
     PRA_VILLE char(50)  not null  
     ,
     constraint PK_PRATICIEN primary key (PRA_ID)
  ) 
go



/*      INDEX DE PRATICIEN      */



/* -----------------------------------------------------------------------------
      TABLE : DISCIPLINE
----------------------------------------------------------------------------- */

create table DISCIPLINE
  (
     DIS_ID smallint identity (1, 1)   ,
     DIS_LIBELLE varchar(38)  not null  
     ,
     constraint PK_DISCIPLINE primary key (DIS_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : CONTRE_INDICATION
----------------------------------------------------------------------------- */

create table CONTRE_INDICATION
  (
     CON_ID smallint identity (1, 1)   ,
     CON_LIBELLE varchar(128)  not null  
     ,
     constraint PK_CONTRE_INDICATION primary key (CON_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : DEPOSER
----------------------------------------------------------------------------- */

create table DEPOSER
  (
     AVI_ID smallint  not null  ,
     MED_ID smallint  not null  ,
     PRA_ID smallint  not null  ,
     DATE datetime  null  
     ,
     constraint PK_DEPOSER primary key (AVI_ID, MED_ID, PRA_ID)
  ) 
go



/*      INDEX DE DEPOSER      */



/* -----------------------------------------------------------------------------
      REFERENCES SUR LES TABLES
----------------------------------------------------------------------------- */



alter table MEDICAMENT 
     add constraint FK_MEDICAMENT_LABORATOIRE foreign key (LAB_ID) 
               references LABORATOIRE (LAB_ID)
go




alter table MEDICAMENT 
     add constraint FK_MEDICAMENT_CONTRE_INDICATION foreign key (CON_ID) 
               references CONTRE_INDICATION (CON_ID)
go




alter table MEDICAMENT 
     add constraint FK_MEDICAMENT_ASPECT foreign key (ASP_ID) 
               references ASPECT (ASP_ID)
go




alter table MEDICAMENT 
     add constraint FK_MEDICAMENT_UNITE foreign key (UNI_ID) 
               references UNITE (UNI_ID)
go




alter table MEDICAMENT 
     add constraint FK_MEDICAMENT_EFFET_SECONDAIRE foreign key (EFF_ID) 
               references EFFET_SECONDAIRE (EFF_ID)
go




alter table PRATICIEN 
     add constraint FK_PRATICIEN_DISCIPLINE foreign key (DIS_ID) 
               references DISCIPLINE (DIS_ID)
go




alter table DEPOSER 
     add constraint FK_DEPOSER_AVIS foreign key (AVI_ID) 
               references AVIS (AVI_ID)
go




alter table DEPOSER 
     add constraint FK_DEPOSER_MEDICAMENT foreign key (MED_ID) 
               references MEDICAMENT (MED_ID)
go




alter table DEPOSER 
     add constraint FK_DEPOSER_PRATICIEN foreign key (PRA_ID) 
               references PRATICIEN (PRA_ID)
go




/*
 -----------------------------------------------------------------------------
               FIN DE GENERATION
 -----------------------------------------------------------------------------
*/

/*
 ----------------------------------------------------------------------------
             Insertion dans une base de données pour
                        SQL Server 2005
                       (8/3/2022 8:50:10)
 ----------------------------------------------------------------------------
     Nom de la base : ALSV_BDDCSHARP
     Projet : 
     Auteur : LEFEBVRE Guillaume
     Date de dernière modification : 8/3/2022 8:51:56
 ----------------------------------------------------------------------------
*/

/* -----------------------------------------------------------------------------
      OUVERTURE DE LA BASE ALSV_BDDCSHARP
----------------------------------------------------------------------------- */

use ALSV_BDDCSHARP
go



/* -----------------------------------------------------------------------------
      TABLE : DISCIPLINE
----------------------------------------------------------------------------- */


insert into DISCIPLINE(DIS_LIBELLE) values('Médecine générale')
insert into DISCIPLINE(DIS_LIBELLE) values('Cardiologie')
insert into DISCIPLINE(DIS_LIBELLE) values('Allergologie')
insert into DISCIPLINE(DIS_LIBELLE) values('Gastro-entérologie')
insert into DISCIPLINE(DIS_LIBELLE) values('Pédiatrie')
insert into DISCIPLINE(DIS_LIBELLE) values('Rhumatologie')

go



/* -----------------------------------------------------------------------------
      TABLE : PRATICIEN
----------------------------------------------------------------------------- */


insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('', 'Administrateur', 'admin', convert(varchar, hashbytes('SHA2_256', 'admin'), 1), 00000, '', '', 1)
insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('Perez', 'Gérard', 'perez.gerard', convert(varchar, hashbytes('SHA2_256', 'HlXBOvpEQ7rsSy8Idtn6'), 1), 23000, '18 rue du Clos', 'Guéret', 1)
insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('Barbe', 'Margaret', 'barbe.margaret', convert(varchar, hashbytes('SHA2_256', 'xx0BV71n8e7EpU54ekyU'), 1), 38045, '4 avenue Jean Jaurès', 'Grenoble', 2)
insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('Garnier', 'Isaac', 'garnier.isaac', convert(varchar, hashbytes('SHA2_256', 'gPbZd0xgtLcQ82hTY97W'), 1), 37000, '2bis rue Alfred Dunet', 'Tours', 3)
insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('Du-Bonneau', 'Matthieu', 'du_bonneau.matthieu', convert(varchar, hashbytes('SHA2_256', 'c3qrMllQvJACnCrrBOyU'), 1), 37000, '63 place de La Libération', 'Strasbourg', 4)
insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('Peron-De-Gaudin', 'Benjamin', 'peron_de_gaudin.benjamin', convert(varchar, hashbytes('SHA2_256', 'oBzmsH6gF23HViYxt0dP'), 1), 59000, '75 rue de la Pocatière', 'Lille', 5)
insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('Aubry', 'Martine', 'aubry.martine', convert(varchar, hashbytes('SHA2_256', 'IDgW5ycvbBfq2ECjbxqG'), 1), 76000, '213 rue du Gros Horloge', 'Rouen', 6)
insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('Cordier', 'Thomas', 'cordier.thomas', convert(varchar, hashbytes('SHA2_256', 'pic0r1wK0Aoy3vjXUliV'), 1), 31000, '21 rue Victor Hugo', 'Toulouse', 1)
insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('Maillet', 'Amélie', 'maillet.amelie', convert(varchar, hashbytes('SHA2_256', 'MnaDSDNT14pgcsgtiD49'), 1), 29200, '63 rue Michel Colucci', 'Brest', 2)
insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('Raynaud-Colin', 'Capucine', 'raynaud_colin.capucine', convert(varchar, hashbytes('SHA2_256', '3iwVEGInSQ8t49XLCAtt'), 1), 75000, '520 avenue des Champs-Élysées', 'Paris', 3)
insert into PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID) values('Robert', 'Constance', 'robert.constance', convert(varchar, hashbytes('SHA2_256', 'uIzw0FQ98J80RuLk0iyH'), 1), 06000, '46 quai Charles de Gaulle', 'Nice', 4)

go



/* -----------------------------------------------------------------------------
      TABLE : ASPECT
----------------------------------------------------------------------------- */


insert into ASPECT(ASP_LIBELLE) values('Gélule')
insert into ASPECT(ASP_LIBELLE) values('Comprimé')
insert into ASPECT(ASP_LIBELLE) values('Poudre')
insert into ASPECT(ASP_LIBELLE) values('Suppositoire')
insert into ASPECT(ASP_LIBELLE) values('Comprimé pelliculé')
insert into ASPECT(ASP_LIBELLE) values('Sirop')
insert into ASPECT(ASP_LIBELLE) values('Comprimé enrobé')

go



/* -----------------------------------------------------------------------------
      TABLE : UNITE
----------------------------------------------------------------------------- */


insert into UNITE(UNI_LIBELLE) values('mg')
insert into UNITE(UNI_LIBELLE) values('mL')

go



/* -----------------------------------------------------------------------------
      TABLE : LABORATOIRE
----------------------------------------------------------------------------- */


insert into LABORATOIRE(LAB_NOM) values('SANOFI')
insert into LABORATOIRE(LAB_NOM) values('UPSA')
insert into LABORATOIRE(LAB_NOM) values('VIDAL')
insert into LABORATOIRE(LAB_NOM) values('Pfizer')
insert into LABORATOIRE(LAB_NOM) values('Zambon')
insert into LABORATOIRE(LAB_NOM) values('Pierre Fabre')
insert into LABORATOIRE(LAB_NOM) values('Biogaran')
insert into LABORATOIRE(LAB_NOM) values('THERABEL')
insert into LABORATOIRE(LAB_NOM) values('GSK')
insert into LABORATOIRE(LAB_NOM) values('Expanscience')
insert into LABORATOIRE(LAB_NOM) values('Abbot')

go



/* -----------------------------------------------------------------------------
      TABLE : EFFET_SECONDAIRE
----------------------------------------------------------------------------- */


insert into EFFET_SECONDAIRE(EFF_LIBELLE) values('Réaction d''hypersensibilité')
insert into EFFET_SECONDAIRE(EFF_LIBELLE) values('Choc anaphylactique')
insert into EFFET_SECONDAIRE(EFF_LIBELLE) values('Oedème de Quincke')
insert into EFFET_SECONDAIRE(EFF_LIBELLE) values('Erythème cutané')
insert into EFFET_SECONDAIRE(EFF_LIBELLE) values('Urticaire')
insert into EFFET_SECONDAIRE(EFF_LIBELLE) values('Rash cutané')
insert into EFFET_SECONDAIRE(EFF_LIBELLE) values('Réaction cutanée')

go


/* -----------------------------------------------------------------------------
      TABLE : CONTRE_INDICATION
----------------------------------------------------------------------------- */


insert into CONTRE_INDICATION(CON_LIBELLE) values('Insuffisance hépatocellulaire sévère')
insert into CONTRE_INDICATION(CON_LIBELLE) values('Enfant avant 6 ans')
insert into CONTRE_INDICATION(CON_LIBELLE) values('Consommation d''alcool')

go



/* -----------------------------------------------------------------------------
      TABLE : MEDICAMENT
----------------------------------------------------------------------------- */

insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Doliprane', null, 1.07, 1000, 8, 1, 1, 1, 1, 1)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Efferalgan', null, 2.10, 1000, 8, 2, 2, 1, 1, 2)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Dafalgan', null, 1.16, 1000, 8, 2, 2, 1, 2, 1)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Levothyrox', null, 10.28, 25, 30, 3, 2, 1, 2, 2)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Imodium', null, 2.25, 2, 20, 3, 1, 1, 3, 3)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Kardegic', null, 1.79, 500, 10, 3, 3, 1, 1, 3)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Spasfon', null, 2.19, 2800, 10, 3, 4, 1, 2, 3)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Ismig', null, 3.69, 2.5, 12, 3, 5, 1, 5, 1)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Tahor', null, 15.76, 80, 90, 4, 5, 1, 2, 3)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Spedifen', null, 4.90, 400, 12, 5, 5, 1, 6, 1)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Voltarene', null, 2.36, 100, 10, 2, 4, 1, 7, 3)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Eludril', null, 1.56, 500, 1, 6, 6, 2, 7, 2)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Ixprim', null, 3.04, 325, 20, 3, 5, 1, 2, 3)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Paracetamol Biogaran', null, 1.07, 1000, 8, 7, 2, 1, 6, 2)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Forlax', null, 3.90, 10000, 20, 3, 3, 1, 2, 2)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Magne B6', null, 5.17, 48, 50, 1, 7, 1, 3, 2)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Helicidine', null, 2.87, 10, 1, 8, 6, 2, 4, 2)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Clamoxyl', null, 1.87, 2000, 10, 9, 3, 1, 4, 1)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Piascledine', null, 5.83, 300, 90, 10, 1, 1, 1, 3)
insert into MEDICAMENT(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID, EFF_ID, CON_ID) values('Lamaline', null, 1.41, 340, 16, 11, 1, 1, 1, 2)

go



/* -----------------------------------------------------------------------------
      TABLE : PROVOQUER
----------------------------------------------------------------------------- */

insert into PROVOQUER(MED_ID, EFF_ID) values (1, 1)
insert into PROVOQUER(MED_ID, EFF_ID) values (1, 2)
insert into PROVOQUER(MED_ID, EFF_ID) values (2, 4)
insert into PROVOQUER(MED_ID, EFF_ID) values (4, 4)
insert into PROVOQUER(MED_ID, EFF_ID) values (4, 5)
insert into PROVOQUER(MED_ID, EFF_ID) values (4, 6)
insert into PROVOQUER(MED_ID, EFF_ID) values (5, 6)
insert into PROVOQUER(MED_ID, EFF_ID) values (5, 7)
insert into PROVOQUER(MED_ID, EFF_ID) values (6, 7)
insert into PROVOQUER(MED_ID, EFF_ID) values (7, 7)
insert into PROVOQUER(MED_ID, EFF_ID) values (7, 3)
insert into PROVOQUER(MED_ID, EFF_ID) values (7, 2)
insert into PROVOQUER(MED_ID, EFF_ID) values (8, 7)
insert into PROVOQUER(MED_ID, EFF_ID) values (8, 1)
insert into PROVOQUER(MED_ID, EFF_ID) values (9, 5)
insert into PROVOQUER(MED_ID, EFF_ID) values (10, 6)
insert into PROVOQUER(MED_ID, EFF_ID) values (10, 7)
insert into PROVOQUER(MED_ID, EFF_ID) values (12, 3)
insert into PROVOQUER(MED_ID, EFF_ID) values (13, 2)
insert into PROVOQUER(MED_ID, EFF_ID) values (13, 1)
insert into PROVOQUER(MED_ID, EFF_ID) values (14, 1)
insert into PROVOQUER(MED_ID, EFF_ID) values (15, 1)
insert into PROVOQUER(MED_ID, EFF_ID) values (17, 3)
insert into PROVOQUER(MED_ID, EFF_ID) values (17, 5)
insert into PROVOQUER(MED_ID, EFF_ID) values (19, 6)
insert into PROVOQUER(MED_ID, EFF_ID) values (19, 7)
insert into PROVOQUER(MED_ID, EFF_ID) values (20, 7)
insert into PROVOQUER(MED_ID, EFF_ID) values (20, 4)


go



/* -----------------------------------------------------------------------------
      TABLE : COMPRENDRE
----------------------------------------------------------------------------- */

insert into COMPRENDRE(MED_ID, CON_ID) values (1, 1)
insert into COMPRENDRE(MED_ID, CON_ID) values (1, 2)
insert into COMPRENDRE(MED_ID, CON_ID) values (2, 3)
insert into COMPRENDRE(MED_ID, CON_ID) values (4, 2)
insert into COMPRENDRE(MED_ID, CON_ID) values (4, 1)
insert into COMPRENDRE(MED_ID, CON_ID) values (4, 3)
insert into COMPRENDRE(MED_ID, CON_ID) values (5, 3)
insert into COMPRENDRE(MED_ID, CON_ID) values (5, 2)
insert into COMPRENDRE(MED_ID, CON_ID) values (6, 3)
insert into COMPRENDRE(MED_ID, CON_ID) values (7, 2)
insert into COMPRENDRE(MED_ID, CON_ID) values (7, 3)
insert into COMPRENDRE(MED_ID, CON_ID) values (7, 1)
insert into COMPRENDRE(MED_ID, CON_ID) values (8, 3)
insert into COMPRENDRE(MED_ID, CON_ID) values (8, 1)
insert into COMPRENDRE(MED_ID, CON_ID) values (9, 2)
insert into COMPRENDRE(MED_ID, CON_ID) values (10, 3)
insert into COMPRENDRE(MED_ID, CON_ID) values (10, 1)
insert into COMPRENDRE(MED_ID, CON_ID) values (12, 1)
insert into COMPRENDRE(MED_ID, CON_ID) values (13, 2)
insert into COMPRENDRE(MED_ID, CON_ID) values (13, 1)
insert into COMPRENDRE(MED_ID, CON_ID) values (14, 2)
insert into COMPRENDRE(MED_ID, CON_ID) values (15, 3)
insert into COMPRENDRE(MED_ID, CON_ID) values (17, 3)
insert into COMPRENDRE(MED_ID, CON_ID) values (17, 2)
insert into COMPRENDRE(MED_ID, CON_ID) values (19, 1)
insert into COMPRENDRE(MED_ID, CON_ID) values (19, 2)
insert into COMPRENDRE(MED_ID, CON_ID) values (20, 1)
insert into COMPRENDRE(MED_ID, CON_ID) values (20, 3)


go



/*
 -----------------------------------------------------------------------------
               FIN DE GENERATION
 -----------------------------------------------------------------------------
*/

Create Proc PS_I_Medicament
/*Procédure permettent l'insertion des données des médicaments dans la base de données Médicaments*/
/*Paramètres du médicament*/
@MED_NOM varchar(38),
@MED_PHOTO image, @MED_PRIX real, @MED_QUANTITE int,
@MED_NB_ENTITES smallint, @LAB_ID smallint, @ASP_ID smallint, @UNI_ID smallint
AS
Begin
	/*Insertion des données du médicament */
	Insert Into MEDICAMENT
	(MED_NOM, MED_PHOTO, MED_PRIX, MED_QUANTITE, MED_NB_ENTITES, LAB_ID, ASP_ID, UNI_ID)
	Values(@MED_NOM,@MED_PHOTO,@MED_PRIX,@MED_QUANTITE,@MED_NB_ENTITES, @LAB_ID, @ASP_ID, @UNI_ID)
END
go

Create Proc PS_D_Medicament
/*Procédure permettent la suppression des données des médicaments dans la base de données*/
/*Paramètres du médicament*/
@MED_ID smallint
AS
Begin
	/*Suppression des données du médicament */
	Delete from COMPRENDRE Where MED_ID = @MED_ID
	Delete from PROVOQUER Where MED_ID = @MED_ID
	Delete from MEDICAMENT Where MED_ID = @MED_ID
END
go

Create Proc PS_I_Praticien
/*Procédure permettent l'insertion des données des praticiens dans la base de données */
/*Paramètres du praticien*/
@PRA_NOM varchar(38), @PRA_PRENOM varchar(38), @PRA_LOGIN varchar(50), @PRA_MDP varchar(128), @PRA_CP int, @PRA_ADRESSE varchar(50), @PRA_VILLE char(50), @DIS_ID smallint 
As
Begin
	/*Insertion des données du médicament */
	Insert Into PRATICIEN
	(PRA_NOM, PRA_PRENOM, PRA_LOGIN, PRA_MDP, PRA_CP, PRA_ADRESSE, PRA_VILLE, DIS_ID)
	Values(@PRA_NOM, @PRA_PRENOM, @PRA_LOGIN, convert(varchar, hashbytes('SHA2_256', @PRA_MDP), 1), @PRA_CP, @PRA_ADRESSE, @PRA_VILLE, @DIS_ID)
End
go

Create Proc PS_D_Praticien
/*Procédure permettent la suppression des données des praticiens dans la base de données*/
/*Paramètres du praticien*/
@PRA_ID smallint
AS
Begin
	/*Suppression des données du médicament */
	Delete from DEPOSER Where PRA_ID = @PRA_ID
	Delete from PRATICIEN Where PRA_ID = @PRA_ID
END
go

create Proc PS_E_Medicament
/*Procédure permettent la modification des données des médicaments dans la base de données Médicaments*/
/*Paramètres du médicament*/

@MED_ID smallint, @MED_NOM varchar(38),
@MED_PHOTO image, @MED_PRIX real, @MED_QUANTITE int,
@MED_NB_ENTITES smallint
AS
Begin
	/*Modification des données du médicament */
	Update MEDICAMENT

	Set MED_NOM = @MED_NOM,
	MED_PHOTO = @MED_PHOTO,
	MED_PRIX = @MED_PRIX,
	MED_QUANTITE = @MED_QUANTITE,
	MED_NB_ENTITES = @MED_NB_ENTITES
	Where MED_ID = @MED_ID
END
go

Create Proc PS_E_Praticien
/*Procédure permettent la modification des données des praticiens dans la base de données*/
/*Paramètres du praticien*/

@PRA_ID smallint, @PRA_NOM varchar(38), @PRA_PRENOM varchar(38), @PRA_LOGIN varchar(50), @PRA_MDP varchar(128), @PRA_CP int, @PRA_ADRESSE varchar(50), @PRA_VILLE char(50), @DIS_ID smallint
AS
Begin
	/*Modification des données du médicament */
	Update PRATICIEN

	Set PRA_NOM = @PRA_NOM,
	PRA_PRENOM = @PRA_PRENOM,
	PRA_LOGIN = @PRA_LOGIN,
	PRA_MDP = @PRA_MDP,
	PRA_CP = @PRA_CP,
    PRA_ADRESSE = @PRA_ADRESSE,
    PRA_VILLE = @PRA_VILLE,
    DIS_ID = @DIS_ID
	Where PRA_ID = @PRA_ID
END
go

create Proc PS_I_Avis
/*Procédure permettent l'insertion des données des avis praticiens dans la base de données */
/*Paramètres des avis*/
@AVI_TEXTE text, @MED_ID smallint, @PRA_ID smallint,  @DATE date 
As
Begin
	declare @MAX_ID_AVIS smallint
	
	/*Insertion des données de l'avis */
	Insert Into AVIS (AVI_TEXTE) Values(@AVI_TEXTE)
	SELECT @MAX_ID_AVIS = MAX(AVI_ID) FROM AVIS    
    Insert into DEPOSER (AVI_ID, MED_ID, PRA_ID, DATE) Values (@MAX_ID_AVIS, @MED_ID, @PRA_ID, @DATE)
End
go

create PROC PS_Verification_Login
/*Procédure permettent de vérifier si le Login et le mot de passe sont les memes qu'en base*/
@PRA_MDP varchar(128), @PRA_LOGIN varchar(50)
AS
Begin
	Declare @Results int
 /*Verif si l'utilisateur est égal à celui dans la BD */
if EXISTS (Select PRA_LOGIN From PRATICIEN WHERE PRA_LOGIN = @PRA_LOGIN AND PRA_MDP = convert(varchar, hashbytes('SHA2_256', @PRA_MDP), 1))
Begin 
	Set @Results = 1
END
else
Begin
	Set @Results = 0
End
select @Results
END
go

CREATE PROCEDURE PS_Affichage_Praticien

AS
BEGIN

SELECT PRA_ID AS 'Id', PRA_NOM AS 'Nom', PRA_PRENOM AS 'Prénom', CONCAT(PRA_ADRESSE, ', ', PRA_CP, ', ', PRA_VILLE) AS 'Adresse'
FROM PRATICIEN
END
go

create PROCEDURE PS_Affichage_Medicament

AS
BEGIN

SELECT DISTINCT med.MED_ID as 'ID', MED_NOM AS 'Nom Médicament', LAB_NOM AS 'Nom Laboratoire', ASP_LIBELLE AS 'Aspect', MED_PRIX AS 'Prix', MED_NB_ENTITES AS 'Nombre d''entités', CONCAT(MED_QUANTITE, ' ', UNI_LIBELLE) AS 'Quantité par entité'
FROM MEDICAMENT AS med INNER JOIN LABORATOIRE AS lab ON med.LAB_ID=lab.LAB_ID INNER JOIN ASPECT AS asp ON med.ASP_ID=asp.ASP_ID INNER JOIN COMPRENDRE AS com ON med.MED_ID=com.MED_ID INNER JOIN PROVOQUER AS pro ON med.MED_ID=pro.MED_ID INNER JOIN  UNITE AS uni ON uni.UNI_ID=med.UNI_ID
END
go