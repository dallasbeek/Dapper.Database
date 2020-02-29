/****** Object:  Table [dbo].[Contact]    Script Date: 2/28/2020 9:53:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [varchar](64) NOT NULL,
	[LastName] [varchar](64) NOT NULL,
	[FullName]  AS (([FirstName]+' ')+[LastName]),
	[Company] [varchar](64) NULL,
	[Title] [varchar](64) NULL,
	[Email] [varchar](256) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[Timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phone]    Script Date: 2/28/2020 9:53:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phone](
	[Id] [uniqueidentifier] NOT NULL,
	[ContactId] [uniqueidentifier] NOT NULL,
	[Number] [varchar](32) NOT NULL,
	[Label] [varchar](8) NULL,
	[Ordinal] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Phone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'198eaea2-9578-ce16-4cf6-000bbbd22af3', N'Curtis', N'Frye', N'Supquestin  Corp.', N'Prepaid Customer', N'lpryppb.hrpixhsjqw@pbu-ls.org', CAST(N'1967-01-01T09:21:09.940' AS DateTime), CAST(N'2016-09-01T21:04:24.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'12502cb6-2ce6-f192-8114-001496e04735', N'Janelle', N'Alvarez', N'Emkilonazz International ', N'Accounting', N'ttfpbql25@appkrh.org', CAST(N'1964-06-11T10:59:39.510' AS DateTime), CAST(N'1992-08-29T12:28:52.630' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6672ab2e-6fd6-ea33-71cc-00255a13d888', N'Melvin', N'Fernandez', NULL, N'Technical', N'ardxxyqx01@jkezujl.cdncec.org', CAST(N'2004-07-24T13:58:15.630' AS DateTime), CAST(N'1989-12-03T08:53:19.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f73cec99-ce9d-24ca-6686-002f290b093c', N'Gerard', N'Powers', N'Lomnipedgan International ', N'Customer', N'zuhy.krwfdtnkkb@ktic.rjqhtz.com', CAST(N'1978-05-15T23:04:10.940' AS DateTime), CAST(N'1993-09-09T05:26:30.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd94165af-22f9-3e81-1b01-0096ef9a9fd2', N'Lisa', N'Nixon', NULL, N'Accounting', N'dnsn@qzqvoy.wfddpd.net', CAST(N'1979-09-01T22:09:28.790' AS DateTime), CAST(N'1978-09-03T10:11:03.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'87778770-0095-c249-ae71-009fedcec639', N'Wallace', N'Travis', NULL, N'Accounting', N'fswibyqi.dyjdr@wkfzud.net', CAST(N'1996-02-14T21:41:08.680' AS DateTime), CAST(N'1984-03-02T18:57:46.550' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'59a0cc61-6cd7-3428-a324-00f38b979a19', N'Hannah', N'Gamble', N'Trutumover Holdings ', N'Service', N'vvzdpk.vnjlnpjq@qwdcxxd.tpxjne.com', CAST(N'1992-03-02T09:42:54.510' AS DateTime), CAST(N'2014-08-25T19:32:05.970' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'065317fd-3715-d48e-040e-014261e6112e', N'Tisha', N'Hanson', NULL, N'Web', N'sdxjzr755@-ydfno.com', CAST(N'1958-11-17T18:41:34.140' AS DateTime), CAST(N'1964-09-19T16:56:40.680' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2850d571-e360-e950-642d-0217d64527e3', N'Christine', N'Olsen', N'Sursipantor International ', N'Marketing', N'kitblyv.hyqnev@itudb-.org', CAST(N'1970-10-03T18:16:37.120' AS DateTime), CAST(N'1986-06-21T17:31:04.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'492a3ac6-9b1c-efa7-0691-023a4c29b65a', N'Gene', N'Yoder', N'Thruericator  ', N'Prepaid Customer', N'uotxfeon.eltsoi@srgqkd.org', CAST(N'2009-12-02T18:11:06.320' AS DateTime), CAST(N'2010-05-30T19:20:16.890' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c46a3de9-2e41-d1d1-ce12-026985d2e3cd', N'Patrick', N'Mcpherson', N'Endwerpor  Group', N'Marketing', N'fsqs@aro-zm.net', CAST(N'2009-10-29T05:05:13.250' AS DateTime), CAST(N'1958-04-17T15:44:41.920' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6b635de2-9f63-9f1b-7914-032d6db72636', N'Zachary', N'Powers', NULL, N'Service', N'quki.igckcsxds@xtzyyt.net', CAST(N'2012-03-31T23:49:25.800' AS DateTime), CAST(N'1968-02-26T13:35:36.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'01c14faf-9fe5-fff5-6aca-03a7151cc895', N'Constance', N'Pham', NULL, N'Technical', N'duyf.fwsvqjtt@bneadf.com', CAST(N'1998-02-21T06:49:26.700' AS DateTime), CAST(N'1968-04-13T10:28:45.770' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0c47010b-e6f4-0a99-21ad-03f86c3986fe', N'Harvey', N'Frazier', NULL, N'Technical', N'gzqd25@edzyfq.org', CAST(N'1959-08-12T05:41:37.890' AS DateTime), CAST(N'1965-12-27T05:24:42.630' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f3415d1a-bf76-00ce-59d8-048b20c6caf6', N'Lakisha', N'Spears', N'Suptinopor  Inc', N'Service', N'fjugzih1@hduft.bebmpd.org', CAST(N'1986-09-04T18:54:28.250' AS DateTime), CAST(N'1983-02-07T16:22:36.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'372858e9-b431-7179-06d0-04aa0f74e8ed', N'Demetrius', N'Barton', N'Happickower  ', N'Accounting', N'hheduju3@tpytu.gqj--p.org', CAST(N'1960-08-19T00:36:28.310' AS DateTime), CAST(N'2003-12-25T09:54:20.170' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bbf080ed-8ff7-de34-2166-04adf0722f3f', N'Kathryn', N'Moran', NULL, N'Accounting', N'exbdljri91@blajwpaxi.fqvaof.com', CAST(N'1953-11-25T15:19:40.930' AS DateTime), CAST(N'1971-02-24T23:58:51.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bc662b26-343d-bf13-ef5d-04bdb7d81624', N'Bradford', N'Mc Gee', NULL, N'Accounting', N'cdmrgno@oxwwlc.org', CAST(N'2015-06-08T01:29:39.800' AS DateTime), CAST(N'2000-02-13T03:21:56.760' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'57127af3-4c5c-4f57-7c5e-051702522d2d', N'Sherman', N'Andrews', NULL, N'Service', N'qsjfjol.plucswyy@weegyj.net', CAST(N'1993-03-09T09:38:48.530' AS DateTime), CAST(N'2013-05-24T06:35:42.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'19d865a4-1ccd-0f2d-a757-0518e0b5e4a6', N'Tasha', N'Hughes', NULL, N'Technical', N'kosrr256@udfawecps.nfafmv.net', CAST(N'2002-04-20T11:24:35.050' AS DateTime), CAST(N'1980-03-16T19:47:33.520' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e5e07bdc-7e09-dccb-a27c-05c213e10647', N'Ray', N'Cobb', NULL, N'Accessory Sales', N'qitum.qhhatd@tqsjku.net', CAST(N'2006-12-14T23:38:05.570' AS DateTime), CAST(N'1953-03-16T08:15:52.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f642946b-40e0-f388-2c2a-05d97e5a3233', N'Trina', N'Lara', NULL, N'Technical Customer', N'mfeyp.iolsoozmqa@mnljcl.org', CAST(N'1954-11-07T05:50:17.500' AS DateTime), CAST(N'1976-03-31T23:14:59.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1e2bb3cc-9383-59b8-18fd-062042106392', N'Fernando', N'Myers', NULL, N'Prepaid Customer', N'zhlz.uyganllxh@aqcvxl.vhdcyy.net', CAST(N'1973-04-28T15:58:45.180' AS DateTime), CAST(N'1959-06-06T06:54:37.960' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'04ff69c3-8395-594f-2f5f-0658458f969a', N'Christina', N'Beasley', N'Winquestadicator International ', N'International Sales', N'pghati.lfaivq@grtpru.org', CAST(N'1967-05-06T07:39:41.160' AS DateTime), CAST(N'2000-09-17T21:58:31.220' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bdcb1dc2-683d-b469-02c1-0728aff94e57', N'Jeannie', N'Allison', NULL, N'Accounting', N'wmduraj92@dwudum.com', CAST(N'1961-12-03T10:04:29.600' AS DateTime), CAST(N'2005-10-22T16:44:49.070' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9612874b-a4ba-9c64-5e4b-07313af07936', N'Nicholas', N'Shields', NULL, N'Service', N'ckemoifr21@qjllox.eehrwv.net', CAST(N'1999-12-16T22:32:26.210' AS DateTime), CAST(N'1994-02-08T18:30:02.980' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c72bfe07-685f-edd7-d75a-07631bbe27c3', N'Lamont', N'Mejia', N'Thrutinommar International ', N'Prepaid Customer', N'icmr.gcbatr@fmwnioso.nzqsml.com', CAST(N'1963-04-15T01:15:32.610' AS DateTime), CAST(N'1978-04-26T00:57:08.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'19472ae5-8f4d-2e60-68b1-081d23a86bd9', N'Bridgette', N'Mccall', NULL, N'Prepaid Customer', N'wqjp.fncg@xbohb.xsoi-w.com', CAST(N'1998-08-25T15:48:14.100' AS DateTime), CAST(N'2009-07-22T04:24:24.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd9158c1d-dc50-40e1-ccbc-082f28a03870', N'Katherine', N'Fisher', NULL, N'Accounting', N'qwfbwqst4@waeizya.pryqyq.net', CAST(N'2011-11-01T20:06:52.390' AS DateTime), CAST(N'2001-12-02T18:18:28.620' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'220ea8f3-4067-fec0-02c9-083f13d0601c', N'Kristie', N'Klein', NULL, N'Web', N'npujg39@z-vwnf.org', CAST(N'1966-12-22T07:46:21.200' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6d0eb4f5-0bda-493d-f6ce-089bbfed88a5', N'Nelson', N'Bird', NULL, N'Accounting', N'kjmoxmt.djaelly@qsqndk.net', CAST(N'1994-09-30T04:36:20.670' AS DateTime), CAST(N'1954-01-25T10:49:22.590' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'70bbcd78-ae11-220e-23e7-0936fca70187', N'Lashonda', N'Hardy', N'Barcadedgor Holdings ', N'National Sales', N'wkgp@tspdsygi.doipwh.net', CAST(N'1969-05-10T04:17:12.450' AS DateTime), CAST(N'2008-01-23T12:01:22.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0c9993ad-02d2-7b5d-d695-093e768602aa', N'Jesus', N'Welch', NULL, N'Service', N'wocvfpmm247@tdfzho.net', CAST(N'2002-11-04T10:55:07.090' AS DateTime), CAST(N'1967-08-10T01:13:09.540' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e5e73965-2288-2404-c49f-098d84a3e797', N'Angelina', N'Delacruz', NULL, N'Accounting', N'maov.atozux@uvfnte.com', CAST(N'1980-06-17T14:36:00.090' AS DateTime), CAST(N'1979-07-12T10:18:59.230' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2ad28961-418b-91be-d710-0a7d2748bb70', N'Erick', N'Porter', N'Monvenplor  Inc', N'Web', N'qfimlai554@fezbem.eqednf.org', CAST(N'1987-01-07T19:43:55.640' AS DateTime), CAST(N'2002-07-11T12:45:27.820' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b4e88620-d85f-7a9a-22c1-0a8698ddc2d8', N'Earnest', N'Wheeler', NULL, N'Web', N'usuh5@hnbzcc.vpxzjk.org', CAST(N'2011-08-21T20:15:31.240' AS DateTime), CAST(N'1962-06-09T19:08:44.630' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a6160a25-386f-f4f4-1595-0ac758b3b576', N'Sonja', N'Mac Donald', NULL, N'Accounting', N'murv.uwmwov@tbzddx.yoefkh.net', CAST(N'1988-10-06T15:00:01.990' AS DateTime), CAST(N'2007-12-23T08:17:47.840' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c3c015f6-6e90-55f7-ea65-0b12c9128d30', N'Thomas', N'Carrillo', NULL, N'Web', N'rnnbcvj18@arqchkuw.wkqgej.org', CAST(N'1987-03-05T03:17:15.740' AS DateTime), CAST(N'2015-08-17T10:59:06.990' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8008c560-b24e-ab59-91b6-0bf1b2ba065d', N'Lauren', N'Sandoval', NULL, N'Service', N'wnfepmk.cooik@nbdzif.mzinez.com', CAST(N'1993-03-31T05:55:24.760' AS DateTime), CAST(N'1999-08-10T12:32:12.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a122a590-863a-be23-e6ac-0bf3ce9a8b0e', N'Tanya', N'O''Connell', N'Pardudentor  ', N'Accessory Customer', N'stvja06@rgepqtvja.yupimp.net', CAST(N'1996-10-02T18:28:19.890' AS DateTime), CAST(N'2000-12-13T18:20:59.560' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4ee35e35-b543-f8ac-52cc-0c55fac33999', N'Edward', N'Marquez', NULL, N'Prepaid Customer', N'xgdrha.fbjarquykz@gkeacgb.xyjfpc.com', CAST(N'1978-12-30T14:40:59.130' AS DateTime), CAST(N'1996-05-05T03:09:29.820' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'595e88e6-4be2-de11-8ff8-0cfcfe76b5d9', N'Tammi', N'Weiss', NULL, NULL, N'icdygioo.skfnstr@xzpbsr.com', CAST(N'1968-02-19T19:19:03.610' AS DateTime), CAST(N'1965-05-10T19:08:44.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8d2e5dac-c1ea-a137-196e-0d201f2af573', N'Holly', N'Mc Mahon', NULL, N'Service', N'jcrz.ltuqrxzuwm@mudwwv.com', CAST(N'1978-06-12T19:52:33.520' AS DateTime), CAST(N'1967-08-04T12:24:52.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4f4ac784-8f8c-1b16-1c92-0d32faedc36f', N'Kerri', N'Park', N'Tupzapimex WorldWide ', N'Customer', N'oeak@dnkk.liyerw.com', CAST(N'1960-06-17T05:42:57.550' AS DateTime), CAST(N'2018-02-27T19:16:43.890' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e290f8b5-c685-b432-45e1-0d905ce0ca90', N'Scotty', N'King', NULL, N'Consumer Marketing', N'idvxdx.gpubbtzr@xeddtf.com', CAST(N'2013-01-16T17:36:01.850' AS DateTime), CAST(N'2000-07-01T05:22:19.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ec2fe97f-fd8a-40cf-d36a-0ddde9880768', N'Reginald', N'Cowan', NULL, N'Service', N'wiucnuhb.pinbjnt@xmqyvcioz.howagd.org', CAST(N'1986-06-08T18:02:48.260' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1ab2be01-e9e9-6f7e-6bf2-0df76d694181', N'Ted', N'Cunningham', NULL, N'Web', N'xyput229@ymkdwwn.uuoqbk.net', CAST(N'2000-12-07T20:35:50.120' AS DateTime), CAST(N'1989-05-09T01:15:00.210' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2e855192-56b3-16cf-df3d-0ee8784a1531', N'Neal', N'Monroe', NULL, N'Web', N'lpupr.mmikmezst@ausihc.org', CAST(N'1984-12-11T17:35:30.080' AS DateTime), CAST(N'1979-05-18T03:22:15.060' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'aff410dd-7d47-aaa0-0d93-0eecad577aab', N'Jamison', N'Bonilla', N'Thrupebex  ', N'Technical Customer', N'wfbnx.huyle@rthl.iaojzt.org', CAST(N'1977-12-22T21:24:12.590' AS DateTime), CAST(N'1961-05-16T08:19:22.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'290f0c2f-5511-8f22-31d8-0effb9fcbe5e', N'Claire', N'Mcfarland', N'Varkilover WorldWide ', N'Technical', N'odof151@snyzkz.qjkp-g.org', CAST(N'1970-04-10T06:37:24.750' AS DateTime), CAST(N'1991-05-14T13:05:03.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9f9cf971-7492-aa1e-3a1d-0f59f7dc7557', N'Keith', N'Mahoney', NULL, N'Accounting', N'mlbktu0@omefqi.net', CAST(N'1955-04-13T01:49:24.890' AS DateTime), CAST(N'2003-07-07T12:44:11.510' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f6961a1f-cddb-5fcd-3321-0f6e57fb764b', N'Glenda', N'Benton', N'Gropickegover  ', N'Technical', N'cjdyi163@whtume.org', CAST(N'1993-03-13T23:26:35.050' AS DateTime), CAST(N'2009-04-07T20:25:40.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'78a3154a-e10d-0f48-536d-0f8f264b8f8c', N'Jack', N'Dawson', N'Cipfropefan  ', N'Technical', N'yrbig36@phmbh.-rfixp.org', CAST(N'1956-01-30T09:51:27.950' AS DateTime), CAST(N'1971-11-27T14:19:14.060' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'15fb16de-ee9d-7a54-2082-0fd969190411', N'Audrey', N'Lowe', N'Supbanicator International ', N'Technical', N'rpqlotk.myhi@vexcyzq.nwzfgs.net', CAST(N'2014-01-28T23:08:53.120' AS DateTime), CAST(N'1994-03-04T22:53:29.750' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bc519677-9f85-eeb4-51eb-103cf4e8fb1d', N'Latisha', N'Bruce', N'Rapkilefin Direct ', N'Accounting', N'axnioi.ucdc@dgdrght.nhijxg.org', CAST(N'2013-09-07T07:48:11.470' AS DateTime), CAST(N'1972-07-02T11:30:58.800' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'04ea5e3c-e72c-f996-ecd8-1093ceb27775', N'Kristy', N'Kidd', N'Lomsapor International Corp.', N'Technical Customer', N'cypdfk1@ngmjzn.org', CAST(N'1984-12-20T23:12:05.130' AS DateTime), CAST(N'1995-10-25T14:48:18.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b430903d-007a-b3e0-6d38-10f39253a78d', N'Allen', N'Houston', N'Thrurobax International ', N'Accounting', N'fhfq568@fdvcgd.net', CAST(N'2009-02-11T06:08:55.070' AS DateTime), CAST(N'1993-09-11T13:27:37.960' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'39df430d-6876-983c-df27-11133cf41465', N'Serena', N'Gallegos', NULL, N'Accounting', N'fgrltc.sgdpcwii@fmuj.exxcan.com', CAST(N'1987-07-02T17:08:42.370' AS DateTime), CAST(N'1962-12-31T10:56:20.980' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'898bb756-0d79-0c18-e1b5-11174f4db55f', N'Sophia', N'Harrington', N'Bartanantor  ', N'Technical', N'htewogk5@zdvxjl.com', CAST(N'1986-04-22T00:22:14.450' AS DateTime), CAST(N'1999-07-24T05:51:08.500' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0d35428a-db74-880c-40cf-11236cf85f3e', N'Hannah', N'Goodwin', NULL, N'Prepaid Customer', N'sqidrhag.xkycart@vwrdf.sdebpv.com', CAST(N'1965-12-19T09:38:03.370' AS DateTime), CAST(N'1995-12-19T11:35:26.790' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5b6ee76e-a1d9-eb89-838b-11a0da17cd8e', N'Jenna', N'Weber', N'Emsapexax International ', N'Prepaid Customer', N'didybj.wkqxuqdiix@afvdbo.org', CAST(N'1960-03-30T02:03:54.600' AS DateTime), CAST(N'1970-02-07T13:55:39.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'33ce60fc-7bc7-1fb5-1caf-11e929c0794b', N'Brian', N'Montgomery', N'Thrusapan  ', N'Technical', N'jynwoh7@giodcl.znbogj.net', CAST(N'2009-07-07T19:08:27.120' AS DateTime), CAST(N'1980-03-06T17:39:34.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ed5448d8-052d-e536-1dc4-120b96d43859', N'Rickey', N'Estrada', NULL, N'Web', N'bprvu.sczjbcf@kwspl.ruphbn.org', CAST(N'1987-02-04T07:35:13.040' AS DateTime), CAST(N'1982-11-25T19:39:16.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5bd3905f-5785-cdff-b041-1250dc8cd135', N'Andy', N'Moses', N'Hapsipin  ', N'Service', N'xzaedmi63@xfrkai.org', CAST(N'1982-01-06T05:51:30.250' AS DateTime), CAST(N'1964-10-24T10:31:05.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'40faa097-3a8c-e7a0-896c-1255eac6a6d2', N'Zachary', N'Pratt', NULL, N'Accounting', N'mrleuic@bbdhionev.xuphpm.com', CAST(N'1957-05-01T06:36:58.100' AS DateTime), CAST(N'1998-12-11T05:53:24.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'17ea96aa-4758-4fe9-672e-125d1175a1c5', N'Sam', N'Mccoy', N'Empickadicator International ', N'Prepaid Customer', N'sxbnygf4@ovsamb.com', CAST(N'1982-06-27T01:52:24.470' AS DateTime), CAST(N'1969-07-22T05:41:13.850' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7590aef9-d785-b833-73e2-127f67881ede', N'Lonnie', N'Moody', NULL, N'Accounting', N'ergkfetc.xnboqbaluo@bshrso.net', CAST(N'2003-03-10T20:06:29.590' AS DateTime), CAST(N'2011-11-11T00:33:34.900' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd59bd736-8e42-8101-1c40-128517855cb7', N'Jack', N'Newman', NULL, N'Prepaid Customer', N'rovepqu.zwzmtfhax@yhsnze.com', CAST(N'1989-07-11T09:59:21.650' AS DateTime), CAST(N'1962-05-05T07:21:26.560' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5249d820-918e-e425-6ef8-12ba293b40c7', N'Bryce', N'Farmer', N'Parrobentor  ', N'Technical', N'iyxn.cyqbvx@lsfbbu.org', CAST(N'2000-09-04T12:19:22.960' AS DateTime), CAST(N'1974-05-01T03:28:28.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'598459e9-d698-d810-8c1f-12e00a3a0c6b', N'Kurt', N'Mills', NULL, N'Web', N'avlywxo00@wqqayt.ls--ht.net', CAST(N'1953-08-04T19:59:32.980' AS DateTime), CAST(N'1983-12-17T17:46:56.680' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3181779d-7098-d55a-2fb8-12f0f58e898f', N'Laura', N'Kennedy', NULL, N'Business Sales', N'somsnjp.rirqoor@awkxim.org', CAST(N'2005-09-23T12:27:24.640' AS DateTime), CAST(N'1961-02-13T17:10:39.460' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0ee518e1-d3f6-6362-87b3-12f7c75bcdc5', N'Nakia', N'Joseph', N'Barglibentor Holdings ', N'Technical', N'ewbn.iyhr@mbqi-q.com', CAST(N'1977-11-03T19:50:42.560' AS DateTime), CAST(N'2013-01-05T06:17:16.160' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'dd91cac4-746c-b5f7-f61a-13281e8adb89', N'Juanita', N'Booth', N'Upzapackor Holdings ', N'Technical Marketing', N'pfckcvup6@wcwrdo.qfjxhy.net', CAST(N'1958-05-04T00:03:00.930' AS DateTime), CAST(N'1983-05-22T15:53:25.570' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bab3936a-bbb2-b371-6192-1328e1600590', N'Irma', N'Wagner', NULL, N'Web', N'pnbr@apgqep.org', CAST(N'1993-03-30T12:44:52.940' AS DateTime), CAST(N'1974-11-01T08:54:05.480' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'92bcf2e3-6cf7-1b13-a731-132ea3097502', N'Amelia', N'Esparza', NULL, N'Service', N'asomrg.dhlj@beksnh.net', CAST(N'1979-12-02T08:54:26.810' AS DateTime), CAST(N'2001-05-24T05:22:58.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f66e2829-d221-bec6-d3da-1341c3a58ca4', N'Rogelio', N'Hanna', N'Surwerin WorldWide ', NULL, N'hdzs.lvsxbp@mubxpe.com', CAST(N'1957-12-17T00:30:59.350' AS DateTime), CAST(N'1968-02-24T06:22:47.430' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ae3b0719-2461-fc30-820b-13425696571b', N'Sherman', N'Mueller', N'Qwitumover WorldWide Inc', N'Service', N'poqd29@ogiqls.org', CAST(N'1964-06-10T01:17:13.730' AS DateTime), CAST(N'1983-02-05T14:12:18.910' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'08313059-2e76-1cec-ea5b-136ce42943b4', N'Jimmy', N'Patel', N'Inhupicover WorldWide ', N'Technical', N'rzbdkgk.wizmwpicfx@aqryfjmr.pnkxbr.net', CAST(N'1998-07-04T06:02:33.210' AS DateTime), CAST(N'1956-09-25T13:52:39.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'07794f1f-adc7-adbd-3786-144d575e10ad', N'Glenda', N'Benton', N'Kliweredor WorldWide ', N'Corporate Marketing', N'fjpbldb.fqls@fdmggf.puyilu.net', CAST(N'1993-03-25T00:52:46.180' AS DateTime), CAST(N'1996-02-13T23:16:44.900' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7eda0893-a537-e7af-9117-152ee7f37f9b', N'Tanisha', N'Yoder', N'Rapcadax Direct ', N'Technical', N'hkoqhlb.emzhvyvat@pqoubw.net', CAST(N'1980-10-30T06:47:47.820' AS DateTime), CAST(N'1990-01-27T22:51:52.800' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0469f040-aea1-d804-8293-159d2335dc2e', N'Stephen', N'Shannon', N'Zeerobower  ', N'Accounting', N'xukvxt890@ecxugb.com', CAST(N'2012-08-13T22:44:28.590' AS DateTime), CAST(N'1959-09-17T00:37:53.040' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e20efbd6-4941-4da6-e509-15fa21330ab0', N'Lance', N'Livingston', NULL, N'Web', N'qmvnyp.htgywe@veouzy.com', CAST(N'1990-03-24T20:20:01.890' AS DateTime), CAST(N'2003-10-11T22:52:13.870' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c257fa74-f21f-8f3c-1f3d-161d644ffffe', N'Sally', N'Bishop', NULL, N'National Sales', N'pmfrs88@fjprab.org', CAST(N'1977-12-09T11:00:10.170' AS DateTime), CAST(N'1979-07-20T14:27:00.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'26658887-0fae-7142-b860-1633f7a8bb42', N'Jeannie', N'Kelly', N'Gronipicower  Group', N'Service', N'sjktn.jkksxwdsot@rdyshq.net', CAST(N'1953-05-18T17:56:19.390' AS DateTime), CAST(N'1969-01-06T14:44:55.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'90f293f9-c517-3b25-c098-1662aecae8a6', N'Dewayne', N'Curtis', N'Qwidudimover WorldWide ', N'Prepaid Customer', N'usptmjb.cyvxxp@cteuke.uygaqw.org', CAST(N'2009-10-30T23:51:47.040' AS DateTime), CAST(N'1998-03-13T19:55:10.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'55a9f545-02d5-5e74-4736-1666e6bf05c7', N'Ramiro', N'Howard', N'Suppickax  ', N'Technical', N'wgpjup.owytwfh@vmvilji.igfgxb.org', CAST(N'1967-08-13T02:46:42.700' AS DateTime), CAST(N'1964-01-22T22:20:26.210' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e4adb2b2-8328-d573-9c91-16944aba2bec', N'Tia', N'Reyes', NULL, N'Service', N'inmeze.sdag@fjqylw.net', CAST(N'1985-09-27T19:39:55.350' AS DateTime), CAST(N'1975-10-10T14:47:59.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fadabfe1-dcf9-d30d-a0f9-169a9eac0017', N'Tom', N'Steele', NULL, N'Prepaid Customer', N'xwpps7@jkcdmu.com', CAST(N'1993-06-30T15:46:42.990' AS DateTime), CAST(N'1998-11-30T06:52:56.530' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bf9973b4-bca4-8b20-c171-16ddb732d1f3', N'Demond', N'Casey', NULL, N'Corporate Customer', N'vmlitdji97@isupaz.org', CAST(N'1976-11-15T17:41:04.520' AS DateTime), CAST(N'1958-11-24T12:02:17.600' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b572665d-3ca3-5699-4d32-16debd353a46', N'Miriam', N'Gordon', N'Zeediminower International ', N'Prepaid Customer', N'biksrmz.ycyjavkiq@xjpshq.net', CAST(N'2006-09-30T07:22:24.490' AS DateTime), CAST(N'1967-04-22T13:57:18.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b852f4eb-0a26-57a1-0f03-170fcc676839', N'Elton', N'Ali', N'Thrupebex WorldWide ', N'Accounting', N'thjpnnqt.bzuabbz@fgbwll.tmwkuf.net', CAST(N'1954-02-27T23:50:21.530' AS DateTime), CAST(N'1962-08-25T10:45:27.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a4cc6e23-3545-8fe6-f998-172b6985a81b', N'Bryan', N'Watts', N'Parsapicator  Inc', N'Prepaid Customer', N'hjtiqc.acutdi@rumize.org', CAST(N'2016-09-07T09:08:57.220' AS DateTime), CAST(N'1954-05-26T04:39:50.210' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b0e0df28-1954-16c9-a7ca-1782d738dad5', N'Lance', N'Solis', N'Resapin Holdings ', N'Technical', N'hnxu.zazuaukcg@iajvip.knrcxr.org', CAST(N'2015-10-18T18:13:22.390' AS DateTime), CAST(N'2006-10-05T02:39:03.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd61ffb4b-e3c4-f7d8-cd24-179a6ed497a0', N'Ricky', N'Sexton', NULL, N'Service', N'dbouwq.khfwlmk@zvwcujlq.poqpjg.com', CAST(N'1972-01-24T03:36:02.140' AS DateTime), CAST(N'1962-10-08T17:36:02.350' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'34237ceb-f58c-f4b1-1306-17b1ab9cccaa', N'Chester', N'Cortez', NULL, N'Prepaid Customer', N'dhncyg36@wsed.bfhhua.net', CAST(N'1980-06-03T10:07:02.400' AS DateTime), CAST(N'1999-11-27T16:46:01.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9e8e589b-1a22-d3ca-0a6f-17ba0e4e9906', N'Chester', N'Mercer', NULL, N'Technical', N'zywlawe.dqpwygyuy@adnlav.net', CAST(N'2012-06-13T05:05:29.930' AS DateTime), CAST(N'1973-05-01T05:18:22.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2137544c-b035-0156-91f0-17e76c8e4306', N'Darren', N'Mc Donald', N'Parquestex Holdings ', N'Service', N'zhwpv.fspvfhzbsd@flgxjo.com', CAST(N'1970-03-30T23:05:19.110' AS DateTime), CAST(N'1999-02-16T11:52:14.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'af352f9d-b0b5-2f93-0191-1841c7b83c9b', N'Johanna', N'Serrano', NULL, N'International Customer', N'huyhpo4@tne-tr.com', CAST(N'1995-11-01T08:05:26.560' AS DateTime), CAST(N'1990-04-27T09:27:22.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'06da91ea-bdac-7365-63fc-1850c6ed04d4', N'Alfred', N'Hickman', NULL, N'Prepaid Customer', N'kwxoz.fcpmu@rvutva.net', CAST(N'2015-02-22T17:07:20.550' AS DateTime), CAST(N'1966-04-06T21:13:20.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e7771c48-f76c-d603-0d6f-189cb9dfe163', N'Candice', N'Arias', NULL, N'Accounting', N'epasxsw.kwwjgy@axmieog.jsqgjf.com', CAST(N'1956-09-04T03:59:20.020' AS DateTime), CAST(N'1977-12-19T15:47:35.290' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6b21b319-c60d-caa6-746f-18a29139a2e5', N'Erica', N'Gay', N'Innipicator Holdings Corp.', N'Technical', N'svhzer7@aieikt.org', CAST(N'2001-12-05T01:29:26.500' AS DateTime), CAST(N'1979-05-01T02:56:48.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd9a7b48b-83d2-9e4f-d1ef-18a8451297b6', N'Bryant', N'Mcintosh', N'Grotumplan WorldWide Corp.', N'Web', N'wlkxq6@ciad-v.org', CAST(N'1973-12-25T07:28:11.450' AS DateTime), CAST(N'1955-08-14T15:24:56.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cffa50c0-4169-fe95-c225-18ac8288aefc', N'Nora', N'Nash', N'Supquestover  Group', N'Accounting', N'oesw.jwwdajmv@soocmt.com', CAST(N'1975-03-24T10:12:46.300' AS DateTime), CAST(N'2004-02-26T04:15:24.510' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'25676b82-392c-dd79-c23a-18e282591447', N'Amelia', N'Neal', N'Tupdudonazz  ', N'Service', N'quko3@rvmpgaho.-nedac.com', CAST(N'1992-03-16T23:56:17.980' AS DateTime), CAST(N'2003-01-13T18:16:59.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'654841c8-e445-7806-4615-18e9b16d22aa', N'Lauren', N'Savage', NULL, N'Sales', N'bypvy.dhof@hoaala.net', CAST(N'2000-09-24T04:05:35.510' AS DateTime), CAST(N'2000-07-10T03:00:08.590' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2b57d3ff-d91a-97b5-d810-1947115a878f', N'Michele', N'Ritter', NULL, N'Prepaid Customer', N'apiswoqt.oqbrii@pdsppvx.ywtual.com', CAST(N'2007-12-27T09:43:36.720' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a570a175-30b6-9008-b001-198e61f51a37', N'Owen', N'Norton', NULL, N'Prepaid Customer', N'lwgolijg.xaqeq@vrki.ntxd-x.org', CAST(N'2002-01-30T06:57:47.960' AS DateTime), CAST(N'2018-02-02T21:55:24.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1a3f3055-4baf-c489-8544-19a64eb833f7', N'Guillermo', N'Mac Donald', N'Emwericator  ', N'Technical', N'ierivfx.ncoxt@dpumls.com', CAST(N'1995-07-16T08:43:11.080' AS DateTime), CAST(N'1973-09-12T10:15:55.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3d29dede-a96a-f262-88d3-19e73b0cb4e9', N'Yolanda', N'Gardner', NULL, N'Service', N'pbfr.yygprimvqs@ucsobd.bcmyha.org', CAST(N'2017-06-09T06:35:49.550' AS DateTime), CAST(N'2008-04-01T04:41:15.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5af6e61d-c3a4-3aae-972c-1a48a74a222e', N'Andrew', N'Guzman', N'Reeredgan Direct Group', N'Technical', N'tzxwhh200@yk-cea.com', CAST(N'1956-11-06T03:30:44.900' AS DateTime), CAST(N'2010-08-29T01:40:16.390' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'23518b66-a6a6-056a-459b-1a7a86b49394', N'Yvonne', N'Flowers', N'Truwerpedgex  Company', N'Web', N'ztybq.xnmbs@qwobsr.org', CAST(N'1994-07-12T03:58:32.040' AS DateTime), CAST(N'1959-01-17T01:39:12.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1cb0dead-5cb2-44e1-1295-1b641b4d4014', N'Kimberley', N'Washington', N'Zeecadar WorldWide ', N'National Sales', N'uqxp@thynqc.org', CAST(N'1956-01-31T20:20:00.630' AS DateTime), CAST(N'2012-11-06T01:18:00.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cd0cde8e-00bb-9ef7-02a8-1bd00fa822fc', N'Grace', N'Weiss', N'Rapglibantor International ', N'Service', N'ytojgb94@iwgusk.nzvsld.net', CAST(N'1994-06-27T08:55:51.710' AS DateTime), CAST(N'2011-04-04T08:40:36.140' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f5bbf3c5-3589-6d27-6bc8-1c430b593701', N'Franklin', N'Dean', NULL, N'Marketing', N'ufyh.vlpxr@wiowzw.zyghov.com', CAST(N'1959-10-23T11:13:01.140' AS DateTime), CAST(N'1972-11-18T13:33:42.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9796b441-ee6e-6d1c-412d-1caf037a0f97', N'Isaac', N'Sims', NULL, N'Prepaid Customer', N'bswgbx443@jgnj.d-zpyi.com', CAST(N'2014-06-10T09:00:54.390' AS DateTime), CAST(N'1959-03-14T10:46:24.850' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2ea8e8a5-14b0-ad67-3cdd-1ccf245d73d1', N'Angelica', N'Bauer', NULL, N'Prepaid Customer', N'aaisxcao.lkzdabhjr@kkiejmv.foyole.com', CAST(N'1975-03-26T13:02:57.100' AS DateTime), CAST(N'1995-06-03T11:19:47.710' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7dbe3f53-7d08-9a11-90f0-1d1d51f8a768', N'Marcie', N'Dickerson', NULL, N'Technical', N'hmji@wuhivj.org', CAST(N'1981-05-15T07:00:03.660' AS DateTime), CAST(N'1980-05-08T13:45:06.020' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7b485aef-562f-5347-dc98-1dbb64c2ae37', N'Charlene', N'Franklin', NULL, N'Technical', N'rrdqb.dsut@erzepa.org', CAST(N'1960-03-05T21:07:04.180' AS DateTime), CAST(N'1976-07-06T06:58:39.350' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'75bfedad-4a62-c65f-be93-1dc820736d77', N'Erik', N'Weaver', NULL, N'Customer', N'phuctune@jjvm.kqckty.com', CAST(N'2015-02-25T22:27:01.480' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'844db8de-7660-557f-18e6-1dd52e973730', N'Misti', N'Franco', NULL, N'Web', N'vsmtx.vaalmssi@csrkv.labclp.com', CAST(N'1965-04-27T13:17:25.210' AS DateTime), CAST(N'2005-12-22T13:16:28.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e4f28c7c-a018-1ac3-ad93-1e224f10afb7', N'Jolene', N'Whitehead', NULL, N'Sales', N'lsoxsr91@wbbtxs.com', CAST(N'2004-07-06T02:25:01.030' AS DateTime), CAST(N'2010-02-02T19:38:54.600' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1a35464a-15f6-d2e2-7bb0-1e2dfedcb66e', N'Simon', N'Petty', N'Recadefazz  ', N'Corporate Customer', N'dmmpioih.akjauiiix@hxjvcdh.dlcfrh.org', CAST(N'1999-06-30T11:22:51.460' AS DateTime), CAST(N'2004-01-12T06:06:57.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f64c1fba-db41-cfce-a859-1e8be5e0a0d7', N'Vickie', N'Vance', N'Parpickupantor  Group', N'Service', N'wmpzrcip56@khkhz.wwtann.org', CAST(N'1996-10-21T09:18:47.260' AS DateTime), CAST(N'1995-06-09T18:46:14.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'10c921a2-393f-d73d-bd8d-1eef0ad852e9', N'Monique', N'Meza', NULL, N'Service', N'hlrp@y-na-k.org', CAST(N'1987-01-23T21:50:48.180' AS DateTime), CAST(N'1990-12-27T17:40:08.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'48030c69-1297-5fcc-1b22-1ef2adb69527', N'Fernando', N'Juarez', N'Endmunplan  ', N'Service', N'kpvdhuaz6@kjjvbk.com', CAST(N'1977-05-31T12:07:34.950' AS DateTime), CAST(N'1983-03-21T10:23:01.260' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3af1562a-bc01-c94c-b87e-1f79bccd34e0', N'Felicia', N'Becker', N'Addimicin Holdings Group', N'Prepaid Customer', N'yjgpbfd5@wjwjbrm.gskhck.net', CAST(N'1979-02-05T07:08:37.050' AS DateTime), CAST(N'1964-06-22T12:24:46.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f949d8e1-f3b0-bd12-2b2f-1fa0d58605ba', N'Sidney', N'Smith', NULL, N'Technical', N'xgdca04@slinsd.net', CAST(N'2000-02-28T03:10:02.170' AS DateTime), CAST(N'2005-05-25T23:21:16.150' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4d472212-b448-62ef-9429-1fbfab5ef447', N'Jocelyn', N'Kidd', N'Rapcadedar  Inc', N'Service', N'twnapim.vxffttvg@hhayevken.prjc-o.net', CAST(N'1981-05-14T02:49:51.550' AS DateTime), CAST(N'1982-09-24T04:03:10.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'356049dc-a4a1-c93a-282b-1fce038692a6', N'Sean', N'Duncan', NULL, N'Technical', N'yzklk.pkbsmnnoqu@qxurny.org', CAST(N'1975-11-13T17:56:17.840' AS DateTime), CAST(N'1997-07-02T19:08:59.810' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'31912448-aede-f726-aeba-2025c64cddfb', N'Miguel', N'Grant', N'Trurobax International ', N'Service', N'kywcb.drsagtsnt@kfiiupjgp.tgyuzs.com', CAST(N'1956-02-13T06:13:06.550' AS DateTime), CAST(N'1976-10-27T10:48:57.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bc37f7f3-ca42-9efc-8a5e-209ac495e1f9', N'Demond', N'Dodson', N'Adpebexantor International Corp.', N'Technical', N'gaky892@pfmvzk.org', CAST(N'1994-07-26T02:12:58.040' AS DateTime), CAST(N'1980-08-27T05:01:21.770' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cdbb1c1a-6fcf-d2be-ae7f-20d058b7b173', N'Tina', N'Rosales', NULL, N'Accounting', N'shpun46@ergpgq.rdjdez.org', CAST(N'1998-01-08T15:35:17.630' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e01dd7e2-3434-9b75-98de-20fc39f1d391', N'Heather', N'Ross', NULL, N'Web', N'asuw.pqkucvbbjo@pmcyrp.net', CAST(N'1996-12-21T16:08:52.500' AS DateTime), CAST(N'2007-03-02T17:54:03.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'08667db7-1a92-259b-d714-213e0cfc60cb', N'Monica', N'Sheppard', NULL, N'Accessory Sales', N'ygir.kqfxvsei@vqmowhaaz.-xvdcj.com', CAST(N'1964-08-14T01:41:58.580' AS DateTime), CAST(N'1958-09-21T01:32:29.760' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2fdf7e9a-4032-3a1e-8a39-2163057c641e', N'Joe', N'Harrison', NULL, N'International Customer', N'kxjz9@cbmxuf.net', CAST(N'1999-04-24T04:45:31.730' AS DateTime), CAST(N'1954-09-05T12:44:18.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0c25c3f8-af60-12bb-84ad-222755ede74f', N'Kerrie', N'Koch', NULL, N'Prepaid Customer', N'bmhs6@eedqttt.hwrbhf.org', CAST(N'1965-11-12T08:53:22.520' AS DateTime), CAST(N'1958-07-27T20:59:05.950' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'08a664d7-2f1b-303a-605e-2258684fc892', N'Douglas', N'Marshall', N'Endglibar  Corp.', N'Prepaid Customer', N'qswhrdl201@t-sci-.net', CAST(N'1962-01-01T13:33:55.610' AS DateTime), CAST(N'1988-01-13T05:57:18.170' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd27f62f5-6e86-130c-f066-2283abf1fd3e', N'Eli', N'Brown', NULL, N'Accounting', N'zatzo57@eqsziq.com', CAST(N'1966-11-05T15:40:53.110' AS DateTime), CAST(N'2018-05-16T16:56:42.990' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b61b8fba-2674-9f78-76e7-22d6bb733da5', N'Tim', N'Pruitt', N'Rapkilan Holdings ', N'Technical', N'vovebnhw.owrairfpjo@tlaxt.pvlrwu.net', CAST(N'1994-06-20T04:10:00.500' AS DateTime), CAST(N'1977-08-10T14:46:24.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9ec9ea50-6f0c-cd89-0b6e-22e14eb3a58c', N'Damian', N'Copeland', NULL, N'Web', N'mvcvgate4@gdyylx.net', CAST(N'1976-09-19T12:51:34.240' AS DateTime), CAST(N'1982-10-30T01:19:34.510' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ff0af445-7afe-3eb0-bff0-22fb5c671be1', N'Roy', N'Dawson', NULL, N'Web', N'seqfu.egme@itsbzq.wcrfxy.net', CAST(N'1960-12-17T18:09:32.400' AS DateTime), CAST(N'1966-09-24T23:42:06.900' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'66c78fc1-6ca5-f206-8563-231300c1e0cd', N'Marcie', N'Gilbert', NULL, N'Prepaid Customer', N'gstvwjad@qpduqg.com', CAST(N'1971-06-19T13:36:06.840' AS DateTime), CAST(N'1982-05-24T09:25:46.510' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'50caa968-57fb-9646-0073-236b8d194091', N'Jennie', N'Hickman', N'Barsapentor  Group', N'Service', N'jzppoq551@hyesg.zursht.org', CAST(N'1963-07-21T15:46:01.240' AS DateTime), CAST(N'1965-11-23T09:25:40.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'af1f6667-9afc-6cc6-05d2-2387775784a1', N'Devin', N'Faulkner', NULL, N'Web', N'mznlrhyi@efbooxgd.fems-f.com', CAST(N'2013-04-29T07:55:46.900' AS DateTime), CAST(N'1974-05-28T23:39:40.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a1c66f30-4e03-f513-9899-239f0268601f', N'Yvette', N'Livingston', N'Zeejubazz Direct ', N'Technical', N'flrw.godvhxc@miryrh.com', CAST(N'1968-10-19T03:38:18.670' AS DateTime), CAST(N'1968-09-17T02:55:52.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fe92ddf6-49d3-6897-9257-23ccba517567', N'Bobbi', N'Kemp', N'Cipsapegantor Direct Group', N'Technical', N'mgbucg@vdhnf.icaeta.com', CAST(N'1957-12-01T21:17:02.320' AS DateTime), CAST(N'1971-07-18T04:32:07.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fdf80359-b33c-d439-5226-23d262060dde', N'Timothy', N'Fritz', NULL, N'Accounting', N'drnuzk256@jvwqqq.net', CAST(N'2014-05-24T14:36:24.240' AS DateTime), CAST(N'2013-10-03T00:23:51.350' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c0737605-cd48-9788-60d1-23f50ccba99e', N'Christy', N'Archer', NULL, N'Prepaid Customer', N'bzjlb861@mcsvrl.ehwhxc.org', CAST(N'2015-05-30T20:04:55.910' AS DateTime), CAST(N'1957-08-11T10:23:54.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7333e8db-26e7-b4ec-923f-2419a88cb6bc', N'Jenny', N'Leach', NULL, NULL, N'peved770@keccrdsk.jwpbxf.net', CAST(N'1993-06-09T22:16:12.890' AS DateTime), CAST(N'2001-07-10T11:49:02.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c88c06c0-9cd0-2bda-7bbf-244415596cae', N'Alison', N'George', N'Adpebefin  ', N'Service', N'utagn.tmqlbek@-ybzbc.com', CAST(N'1997-04-24T01:24:55.440' AS DateTime), CAST(N'2018-06-17T22:51:05.220' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ffb5a984-b061-45db-9a6b-2484cd0af2a7', N'Dwayne', N'Alvarado', NULL, N'Prepaid Customer', N'arautpcx.ubmptde@datfub.com', CAST(N'1976-10-12T01:54:38.230' AS DateTime), CAST(N'2013-01-12T19:40:12.870' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bb9767c5-1598-bcd4-7c65-24dde030f7a3', N'Cathy', N'Frye', NULL, N'Business Marketing', N'lefjid.ixixldwhx@nhiehc.com', CAST(N'1976-05-30T16:16:06.220' AS DateTime), CAST(N'1985-10-12T04:29:39.150' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'855df372-64d6-9dec-ab64-24e21228704c', N'Guadalupe', N'Chandler', NULL, N'Business Sales', N'zmquaxwz.ksnbpm@-gkeyc.org', CAST(N'1980-09-24T02:02:26.620' AS DateTime), CAST(N'1986-02-15T23:18:25.550' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'038a719b-45c6-2263-0478-25026d04da72', N'John', N'Hendrix', N'Tupsapicator  ', N'Accounting', N'ushj.yfsqg@dhavjpcj.iwybxb.org', CAST(N'1954-03-12T21:53:01.240' AS DateTime), CAST(N'1991-04-17T02:55:57.440' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f6191ae8-77d0-16b5-d6b1-2513f879f90c', N'Anitra', N'Walter', NULL, N'Prepaid Customer', N'tkgij1@hcwppzun.ulrxue.net', CAST(N'1966-09-13T18:34:36.020' AS DateTime), CAST(N'1996-10-29T03:15:37.560' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'55a57d26-432c-fda9-f2d0-257339f793fa', N'Glenda', N'Mullins', N'Supsipanazz WorldWide ', N'Service', N'nkfhzyas.yflha@bkbupe.org', CAST(N'1953-06-16T00:17:21.700' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b20eaed0-9b62-1c20-f37d-25ad91399e01', N'Jenifer', N'Livingston', N'Klipickplazz  ', N'Prepaid Customer', N'wfvu.xqynwlfrbk@uekrme.fxldfd.org', CAST(N'1982-08-20T15:02:20.540' AS DateTime), CAST(N'1965-04-05T12:23:02.620' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'79005d3c-a544-9cc0-1ad7-2616b8c5bfdc', N'Ramon', N'Wood', N'Qwiwerpinower WorldWide Inc', N'Service', N'dxkrrk@zabgvs.com', CAST(N'2004-10-03T22:10:23.470' AS DateTime), CAST(N'1983-07-08T02:29:42.310' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8f6bda07-0ddd-523b-1101-2640818dcfa2', N'Krista', N'Conner', NULL, N'Business Sales', N'omav23@qfossb.-mwmhj.net', CAST(N'1967-01-18T13:16:45.770' AS DateTime), CAST(N'1981-06-19T07:01:50.080' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'024ff35a-7e84-345d-ed2e-26c4e844cacf', N'Felipe', N'Fisher', NULL, N'Service', N'sgfbt742@hyyefo.jkacaj.net', CAST(N'1988-08-04T19:22:17.800' AS DateTime), CAST(N'1984-02-10T18:32:05.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'82d9172a-b51e-4587-7208-270708049dd7', N'Abraham', N'Beard', NULL, N'Corporate Sales', N'mtnt1@zng-yh.com', CAST(N'1974-11-03T13:13:47.140' AS DateTime), CAST(N'2015-10-17T17:26:33.520' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'298728c9-4438-1b8b-f404-273f09155a73', N'Autumn', N'Cabrera', NULL, N'Web', N'ufloqsq.xkeblm@qfnpry.jfvbps.net', CAST(N'2002-05-19T02:25:07.380' AS DateTime), CAST(N'1992-05-22T23:17:24.220' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'053f9ad6-93c7-b16d-7a52-2790e53a2c26', N'Simon', N'Nixon', N'Klitanicistor Holdings ', N'Web', N'vyeqqhet@cjpdnq.net', CAST(N'1977-04-02T21:59:32.270' AS DateTime), CAST(N'1973-12-31T06:11:51.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ecf7646f-3bee-4984-af36-279c10526d43', N'Emily', N'Le', N'Zeehupepex  ', N'Web', N'rkugztbb.sfadvrrgcv@xueel.rxjmoz.com', CAST(N'1973-08-20T16:29:56.620' AS DateTime), CAST(N'2016-08-20T15:16:00.850' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'96155c86-dd07-2b58-fceb-27c25da8661c', N'Andrea', N'Watts', NULL, N'Accounting', N'ahjjua9@nmwxnwpnl.mmrgnf.org', CAST(N'1974-07-26T09:13:12.550' AS DateTime), CAST(N'1968-11-25T18:15:43.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3640ce70-c477-6895-fb5d-28dfa34a8c60', N'Marlon', N'Landry', NULL, N'Accounting', N'fsvujj28@uaatql.org', CAST(N'2018-06-06T16:37:43.290' AS DateTime), CAST(N'1985-09-17T05:56:31.980' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'399a00d2-c5e8-b252-a2bc-28ef26cdeff7', N'Omar', N'O''Neill', NULL, N'Technical', N'kktfhai@wzryazrry.-rh-wg.org', CAST(N'2005-07-18T05:33:38.590' AS DateTime), CAST(N'1972-06-09T13:26:18.080' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'be4dfe18-2c08-fba3-b97b-2905727713f5', N'Tara', N'Pope', N'Kliericator  Inc', N'Technical', N'slwknwe15@cgivuhxn.kjxlnn.net', CAST(N'1962-09-02T09:22:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'96ba137f-0fae-6526-5435-295bf94dd64b', N'Diana', N'Dalton', NULL, N'Accounting', N'smwqcudx@nusljo.com', CAST(N'1977-01-09T05:17:52.480' AS DateTime), CAST(N'2015-04-07T02:44:31.790' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0e0f1889-fbc2-8211-2455-297ee9dfa4b7', N'Marie', N'Diaz', NULL, N'Sales', N'ttbdgk@hjlusq.com', CAST(N'1999-09-02T10:34:25.670' AS DateTime), CAST(N'1975-05-29T20:17:28.560' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0e19acfa-779a-205a-0ff0-29e7f5c5234f', N'Ernest', N'Luna', N'Upglibar Direct ', N'Technical', N'alwbelft@amwqaq.com', CAST(N'1998-10-08T02:48:24.390' AS DateTime), CAST(N'1971-12-22T06:40:58.650' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ca8d6850-c123-ba14-5894-2a2385f5c81d', N'Jesse', N'Fuentes', NULL, N'Prepaid Customer', N'zpgfzb.ylzwp@hwxoym.gdziwj.net', CAST(N'2014-02-05T16:06:06.730' AS DateTime), CAST(N'1971-08-01T10:40:34.550' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd631dae9-5790-a902-a223-2a5e3db1530d', N'Jocelyn', N'Buckley', N'Qwipickistor  Corp.', N'Technical Customer', N'yydan8@lkrmlzy.-oaxot.net', CAST(N'1957-07-18T16:13:10.880' AS DateTime), CAST(N'1968-11-23T15:21:43.020' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ec19197b-6083-f793-f059-2aa3e53ab4cd', N'Carlos', N'Kaufman', NULL, N'Technical', N'mmzdkyir.lsjhg@kpfijs.com', CAST(N'1991-01-27T13:08:31.440' AS DateTime), CAST(N'2001-10-17T19:13:21.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'911bbb58-c32f-0617-d3d5-2ac178cfad1e', N'Wendi', N'Contreras', NULL, N'Accounting', N'gfwl.thxztbbox@opcdjzi.bxhlv-.org', CAST(N'1962-05-30T01:10:58.240' AS DateTime), CAST(N'1954-09-15T18:36:23.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'88f1fc78-12be-8de9-1178-2ac4004a0165', N'Angelina', N'Roth', N'Rapbanedistor Holdings ', N'Prepaid Customer', N'gdbnfny.somcwklyx@khjyvq.net', CAST(N'2006-04-19T03:13:18.050' AS DateTime), CAST(N'2018-05-20T04:45:03.240' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd9ff15b9-f0b2-60f8-b5cc-2b3ef7cfd436', N'Vivian', N'Miranda', NULL, N'Web', N'ginlkxtr7@mfbipo.net', CAST(N'1958-01-24T17:53:59.450' AS DateTime), CAST(N'1955-12-12T16:08:33.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'068d40a1-2a6c-1872-f55f-2b5a86dde9c4', N'Margarita', N'Aguirre', NULL, N'Corporate Customer', N'xmcxij.qlgny@whabtw.blbfeg.org', CAST(N'2008-05-02T02:24:08.570' AS DateTime), CAST(N'1996-10-17T21:19:29.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0148d35c-71d7-2c8d-52d9-2b5c080200cb', N'Nakia', N'Ferguson', N'Winbanegazz WorldWide ', N'Web', N'qfzaoqv.wcgtzdtnvl@jnom.lktfqv.org', CAST(N'1971-05-08T15:20:11.830' AS DateTime), CAST(N'1990-11-07T20:27:42.890' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fab58a6d-2cae-7d97-e571-2bff2d87808f', N'Jane', N'Gamble', N'Qwipebicazz Holdings ', N'Technical', N'lbjaka95@dedoyghj.zesxpx.com', CAST(N'2003-04-17T01:40:26.340' AS DateTime), CAST(N'1960-07-15T05:55:51.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'151a0626-1d8b-54a0-af91-2c2f9b40ff91', N'Evelyn', N'Stokes', N'Frotinewantor Holdings Corp.', N'Customer', N'btuqmxl.gqtd@gyeu.kwfivw.net', CAST(N'1960-05-06T20:33:37.840' AS DateTime), CAST(N'1961-09-24T07:25:09.650' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5dfefbb2-39fe-b205-da15-2c4599ba3c9c', N'Jeannie', N'Wilkins', NULL, N'Web', N'lhzk03@inrtkcolf.llrxpw.net', CAST(N'2005-04-14T10:55:30.230' AS DateTime), CAST(N'1960-12-30T02:01:46.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e3ba98cb-5aed-6323-642a-2cb5be9e2664', N'Alex', N'Ellison', N'Sursipin International Corp.', N'Web', N'wynh.bnuapquc@gqygxlmc.utqlbm.org', CAST(N'1988-11-13T07:40:26.350' AS DateTime), CAST(N'1969-06-15T21:09:36.140' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e93dc24d-5fba-aee1-c0f9-2ce82b2d7c04', N'Marla', N'Richards', NULL, N'Technical', N'ikonprvr@pvepxeoc.mejhto.com', CAST(N'2014-06-15T01:27:45.870' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'88f87fd4-3791-3f34-ac56-2d00425d9615', N'Tammi', N'Kelley', NULL, N'Prepaid Customer', N'huihwcqc0@tuyven.org', CAST(N'1962-05-22T20:06:05.550' AS DateTime), CAST(N'1953-01-06T13:56:45.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b2b0b384-5260-2019-ecba-2d97a95c0592', N'Kimberly', N'Peters', NULL, N'National Marketing', N'cgbcjgkg.kwhmzkk@eumzqe.net', CAST(N'2015-12-08T01:14:34.820' AS DateTime), CAST(N'1957-11-05T21:48:11.920' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7b5c85ec-6d6c-2c44-7bfe-2dac625681fd', N'Olga', N'Mc Gee', N'Insapollex Holdings Corp.', N'Customer', N'neftakd@noiimd.net', CAST(N'1990-06-17T19:41:16.860' AS DateTime), CAST(N'1981-08-21T21:05:25.460' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'75d2447e-0f66-f906-f633-2dbc70a3c287', N'Evan', N'Reid', NULL, N'Prepaid Customer', N'pcmcr9@nq-y-u.org', CAST(N'1963-05-06T20:31:02.930' AS DateTime), CAST(N'1986-07-22T18:51:06.300' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'afe99c8f-790f-c153-21e5-2de28bc8835a', N'Shawna', N'Browning', NULL, N'Web', N'moatu27@ntds.jltwvs.org', CAST(N'2010-03-14T04:09:28.140' AS DateTime), CAST(N'2016-01-19T12:16:44.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0581c576-fdd1-5816-a037-2e806d8ef329', N'Ronald', N'Ho', NULL, N'Technical', N'txiglk859@mbhdqdmd.ckilby.net', CAST(N'2013-03-30T05:20:09.150' AS DateTime), CAST(N'2013-11-19T12:53:26.790' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f709b1f7-d07a-4953-6196-2ea6f369f204', N'Gwendolyn', N'Montoya', N'Tupdimentor Direct ', N'Accounting', N'cmap.xudlzp@greoc.r-ylvu.org', CAST(N'1988-05-25T12:38:14.510' AS DateTime), CAST(N'1992-08-14T00:14:42.290' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c6f84273-8257-25a5-b012-2ea835fd3452', N'Lawrence', N'Vaughan', N'Surfropupex Direct Group', N'Service', N'uncizge41@jfacpskmi.xosher.com', CAST(N'1993-04-18T23:20:15.850' AS DateTime), CAST(N'2009-05-20T23:23:48.000' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3365bf58-58d5-d45d-391a-2ee84811d5fe', N'Maggie', N'Mahoney', N'Winerefin  ', N'Web', N'ftwdvf@evobuo.org', CAST(N'2005-07-22T18:42:27.840' AS DateTime), CAST(N'2015-09-24T04:11:18.980' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd22dbc37-5494-25fb-f6ee-2f1e4c7c1827', N'Ashley', N'Odom', NULL, N'Service', N'wkak.gnonygu@jfggny.com', CAST(N'2010-06-02T01:27:23.010' AS DateTime), CAST(N'1955-11-30T18:39:05.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'04bf2c66-c894-e12e-b91b-2f6e15541103', N'Marisol', N'Rangel', NULL, N'Prepaid Customer', N'jjzhuh170@wotzri.net', CAST(N'1985-11-27T19:16:55.250' AS DateTime), CAST(N'1959-05-01T09:35:53.850' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8b4c5a50-4d9d-1e84-6b0e-2feda9c6ee84', N'Jason', N'Schneider', NULL, N'Web', N'agiz.pvdnzi@xwnis.-nzkyd.org', CAST(N'1980-04-13T10:01:48.980' AS DateTime), CAST(N'1988-03-03T02:02:46.950' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'101191fd-b0eb-0a81-1d11-30e63a42dc76', N'Virgil', N'Haley', NULL, N'Web', N'vrth012@nvuj.ewcayd.com', CAST(N'1978-06-30T10:51:48.200' AS DateTime), CAST(N'1990-03-06T20:53:13.890' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cba54225-2059-d4e5-6c39-3107c82b34c3', N'Tracey', N'Shah', NULL, N'Accounting', N'tfmx.rtummxhl@ukul.glgecw.org', CAST(N'1960-10-17T19:15:15.740' AS DateTime), CAST(N'1967-05-28T10:53:36.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'324ab1fb-9b94-9b72-30b3-31653aabfdd2', N'Kristina', N'Cox', N'Kliglibin  ', N'Prepaid Customer', N'hgxwr69@morbci.com', CAST(N'2010-04-20T01:19:15.240' AS DateTime), CAST(N'1993-01-05T15:27:48.390' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0370fe12-b835-98f2-e206-31cd889c163e', N'Julia', N'Miranda', N'Montinan Holdings ', N'Web', N'gcsfhpx.tunhukfskx@dvmxij.net', CAST(N'1963-03-26T20:33:18.320' AS DateTime), CAST(N'1974-07-29T23:51:48.880' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e4d62b47-6199-5876-04b0-31d22e513af9', N'Jose', N'Woods', NULL, N'Corporate Marketing', N'ydjizig.ggxvnpwvza@e-dhqh.com', CAST(N'1954-04-26T13:26:51.820' AS DateTime), CAST(N'1974-08-04T06:57:23.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd4c183ce-fd71-9b6a-be2d-31efb4a1ee13', N'Clint', N'Rangel', N'Fronipan International Inc', N'Accounting', N'rgxpkmrf27@xtfuqi.com', CAST(N'1968-10-23T04:21:24.320' AS DateTime), CAST(N'1955-08-23T19:52:48.680' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0ce89a40-b3cd-711d-808b-323a4940e5c2', N'Jacob', N'Figueroa', NULL, N'Technical', N'ddybag.jgqk@g-kurp.net', CAST(N'2013-11-18T19:08:14.700' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'72d9b5ac-323f-b206-ee90-333fd1561773', N'Monique', N'Castillo', NULL, N'Accounting', N'kpqp.pcyrqyhvar@rngi-t.net', CAST(N'1974-06-16T16:25:07.960' AS DateTime), CAST(N'1990-12-10T11:30:23.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'28ab506b-95a4-91b9-5391-34460458da5b', N'Tamiko', N'Boone', NULL, N'Service', N'fwzlehwa.qvksxmyajv@kxok.-bkuyx.org', CAST(N'1975-08-30T11:41:08.480' AS DateTime), CAST(N'1968-08-28T03:18:28.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'649efdd7-8197-a5cd-d0fa-34f91fd1ee06', N'Ramiro', N'Barnett', NULL, N'Service', N'vhhkqinj.rpqj@tw-jlz.com', CAST(N'1993-05-14T17:12:52.070' AS DateTime), CAST(N'1965-06-05T13:58:41.970' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'08d81c4e-fdc7-f4e3-464e-36022b129919', N'Stacy', N'Lambert', N'Tippickamor International ', N'Service', N'txkahda4@aquhan.wbl-cq.com', CAST(N'1964-02-06T18:13:59.530' AS DateTime), CAST(N'1985-11-24T19:12:04.460' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'61538b7f-340e-a7e2-88aa-3623cfbb6682', N'David', N'Coffey', NULL, N'Accessory Sales', N'fvimsyva828@gqgsac.org', CAST(N'1983-12-03T10:59:32.480' AS DateTime), CAST(N'1990-11-27T03:04:48.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4a6ab7ba-6428-b29b-b2da-367bd564f464', N'Casey', N'Baker', NULL, N'Technical Marketing', N'fktpiv09@ydclx.rvdtj-.org', CAST(N'2015-10-25T12:53:21.840' AS DateTime), CAST(N'1979-11-10T11:18:37.500' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd0ad0f6d-eb58-e178-d5c1-36a5e8607c78', N'Marjorie', N'Terry', NULL, N'Technical', N'zjbcax6@rgkqkg.net', CAST(N'1966-03-03T11:04:29.540' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b7bb975e-d517-1ab4-ba67-36b40cb178d4', N'Cassie', N'Harvey', NULL, N'Accounting', N'vgnu.iyzwazy@xbmnxuczh.ufaibb.net', CAST(N'1978-06-23T21:19:48.660' AS DateTime), CAST(N'1979-11-30T12:42:44.980' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'765b5582-cb33-09cb-cbee-36b915c320ed', N'Jennie', N'Blanchard', NULL, N'Prepaid Customer', N'vpko@zdkhxz.org', CAST(N'1994-02-06T04:06:13.330' AS DateTime), CAST(N'1977-04-04T08:10:38.520' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cb05d5d0-c5e9-78fa-70d2-36caf9b230ea', N'Monique', N'Martin', N'Empebonover Holdings ', N'Accounting', N'wogn9@aslj.liazor.net', CAST(N'1993-02-24T12:11:11.400' AS DateTime), CAST(N'2000-01-24T17:38:58.000' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd6aa13da-c2db-b803-dfa9-36d5b5eeb5ff', N'Jackie', N'Friedman', N'Grodudistor International ', N'Accounting', N'iwljrq.mubrehtcr@gwdkrf.net', CAST(N'1972-01-01T12:39:22.250' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'034d481d-92bd-d36c-d0ae-36db84c26069', N'Alejandro', N'Daniels', N'Emdimaquin Direct ', N'Service', N'rglyp.krhqecasd@asnboo.net', CAST(N'1995-10-16T15:12:19.070' AS DateTime), CAST(N'1992-12-30T03:21:41.500' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'30335da4-be50-b09f-65da-36e87f281622', N'Johnathan', N'Poole', NULL, N'Technical', N'xskvi.tjqb@bycfzbg.utwhty.org', CAST(N'2005-05-13T19:16:03.660' AS DateTime), CAST(N'1977-05-18T01:42:42.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3b97d90e-aa9c-89d8-66a3-37140194bfb5', N'Vickie', N'Hobbs', N'Grojubamar International Corp.', N'Web', N'dnrfxuo.dedvszmqd@prgxjd.net', CAST(N'1985-11-26T04:42:11.240' AS DateTime), CAST(N'1973-11-08T14:34:36.210' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6c67e0bb-ab21-07f5-3bb5-3729bd98d762', N'Nora', N'Ruiz', NULL, N'Business Sales', N'bzwtlc0@codwua.net', CAST(N'1986-02-23T10:42:26.320' AS DateTime), CAST(N'1972-09-19T19:57:32.900' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'701e9dc9-7670-2d31-acfd-372bab0805c6', N'Lonnie', N'Golden', N'Dopvenefin  ', N'Accounting', N'uoahtgs3@wpksxx.dhstup.com', CAST(N'2004-03-28T16:20:29.280' AS DateTime), CAST(N'1959-04-24T00:45:57.390' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c0f4d178-4f58-2b95-90cf-37547eb057fb', N'Nichole', N'Hawkins', N'Tipsipar Direct Group', N'Web', N'fned.ljvahvwnrs@ypdqfvdzh.mjmcqr.org', CAST(N'2010-08-17T08:55:38.160' AS DateTime), CAST(N'1989-11-21T11:51:24.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3dc7b32e-26f0-ac8e-9e59-3762867588da', N'Katrina', N'Ayers', NULL, N'Accounting', N'saxbjtqq.ptvrq@b-heda.com', CAST(N'1968-03-06T11:23:59.100' AS DateTime), CAST(N'2011-01-09T21:46:55.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c8613851-c0f0-4ba9-2b94-37654e682f86', N'Melanie', N'Baldwin', NULL, N'Service', N'qaqg08@slhjml.org', CAST(N'2003-10-03T09:21:14.890' AS DateTime), CAST(N'2007-05-15T00:32:16.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'be477848-de79-fdf7-fb14-37bcf149a24c', N'Kurt', N'Clements', NULL, N'Accounting', N'lrqafw910@xodtf.clvyxw.net', CAST(N'1957-07-22T17:33:37.790' AS DateTime), CAST(N'1999-04-15T10:00:02.960' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'32502023-4b75-d650-7525-3868b79e7c31', N'Daniel', N'Kim', N'Upjubantor Direct Inc', N'Accounting', N'kqts.fcneeunui@ma-bpk.net', CAST(N'1977-03-08T21:00:41.210' AS DateTime), CAST(N'2006-12-24T22:32:29.820' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c8314f5b-e787-f1b6-32f4-38c9397e0bb8', N'Lillian', N'Fletcher', NULL, N'Service', N'olcckuv.xmbpwcncsn@mluxgtlg.vs-dht.org', CAST(N'1978-07-21T08:45:37.630' AS DateTime), CAST(N'1958-01-03T12:31:34.620' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2e017358-1fe7-316b-a1ed-38ed11e6ad18', N'Herman', N'Anderson', N'Rehupistor  ', N'Sales', N'kukjjbmx95@zwfku.dypcde.org', CAST(N'2009-06-04T20:41:52.510' AS DateTime), CAST(N'1967-10-07T14:34:58.530' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3c8c3929-7afd-ea31-862e-39e3d0e03b6b', N'Timothy', N'Arias', N'Winjubistor  ', N'Service', N'eaamsruz.tggbywsx@fbito.tapqcw.org', CAST(N'2009-07-19T03:07:47.180' AS DateTime), CAST(N'1990-04-21T09:38:46.980' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6c2563ec-5826-e1e7-8cd7-39eca3f9c2b1', N'Randal', N'Edwards', NULL, NULL, N'llav.zgplrkd@wgerzm.org', CAST(N'1999-09-29T04:10:03.850' AS DateTime), CAST(N'2013-08-07T18:31:10.300' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'86c6f30e-45c0-99b3-e93d-3a43f809eab0', N'Tyrone', N'Blanchard', N'Tiprobplar WorldWide ', N'Accounting', N'faphedkh52@zndqoo.net', CAST(N'1968-01-11T00:22:35.740' AS DateTime), CAST(N'1976-02-12T07:00:43.200' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b4180adf-8327-4f24-0fb7-3a55f3178470', N'Kendall', N'Vaughan', N'Zeesipanin WorldWide Group', N'Technical Customer', NULL, CAST(N'1966-12-24T05:44:07.960' AS DateTime), CAST(N'1961-12-11T22:33:58.420' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5f5eb3c3-c74c-b167-4ae4-3a895a10bccf', N'Micah', N'Rose', N'Unbanower Holdings Company', N'Service', N'wxlc.csmvg@wnbzro.com', CAST(N'2012-08-23T08:11:55.560' AS DateTime), CAST(N'2017-01-06T07:39:05.620' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1e308786-0581-fb4f-f7a9-3aaa52e0ca27', N'Dianna', N'Harrell', N'Trucadicator WorldWide ', N'Accounting', N'elocse56@dzmqhk.net', CAST(N'2014-09-06T11:08:10.220' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f337e1a3-45d5-33b7-9a6f-3b57de03c788', N'Joanna', N'Griffin', NULL, N'Technical', N'vkay.exhyonrcej@nzibh.eajgrm.net', CAST(N'1953-07-19T15:53:17.340' AS DateTime), CAST(N'1961-10-07T05:32:41.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7f559f2c-a7b7-dad4-e008-3ba89c122837', N'Shawna', N'Chase', NULL, N'Marketing', N'zxcupc56@yjqaqkjmq.iljilc.net', CAST(N'1960-11-02T23:31:20.050' AS DateTime), CAST(N'1982-12-25T13:37:57.390' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'090dbff9-27fd-e144-5eb0-3bc4fddbaa16', N'Demond', N'Lloyd', NULL, N'Web', N'nczepf.rlbumywqq@p-obtk.net', CAST(N'1988-12-13T17:03:57.500' AS DateTime), CAST(N'1981-03-17T16:26:07.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'359b92f3-7ccb-a42f-3dce-3bdf005e4d90', N'Melvin', N'Diaz', NULL, N'Accounting', N'vmwyg.xdtyvuiky@fuskoa.rqhfpo.org', CAST(N'1962-11-01T06:35:13.860' AS DateTime), CAST(N'2006-01-04T19:56:51.080' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8a1eac81-9901-4aa4-9eea-3c1e994ea0ed', N'Clarence', N'Wood', NULL, N'Technical', N'mhzskyg444@ljvzqz.net', CAST(N'1966-11-06T14:19:12.880' AS DateTime), CAST(N'2010-05-28T04:10:15.500' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c2830531-1a2a-eebd-01c2-3c325c736aa7', N'Marvin', N'Bradshaw', N'Emkilover  ', N'Consumer Sales', N'leibdgst@kxv-dw.org', CAST(N'1963-04-05T01:56:44.900' AS DateTime), CAST(N'2010-10-23T21:01:02.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6bb5fa3e-ac63-48ae-322e-3cb7c3794d9a', N'Lewis', N'Hoover', NULL, N'Prepaid Customer', N'alaq95@debpyx.net', CAST(N'2006-10-30T15:08:54.790' AS DateTime), CAST(N'2015-03-31T20:12:55.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'020f207c-0f38-8458-2970-3cba7beba0da', N'Hugh', N'Weiss', NULL, N'Accounting', N'gepfx.vjfxj@bfpctxiee.ymucmb.org', CAST(N'1997-07-14T10:41:11.310' AS DateTime), CAST(N'1961-12-01T06:17:23.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c95676fd-c864-95ad-4556-3ccb009388d8', N'Tiffany', N'Mayer', NULL, N'Service', N'bzjlj.ndkeqnlgb@uuqngk.nib-fc.com', CAST(N'1981-02-03T03:05:16.970' AS DateTime), CAST(N'1973-08-14T03:42:47.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'520ae4eb-91e7-cea0-29d6-3d5684e510aa', N'Richard', N'Peck', N'Thrurobilentor International ', N'Prepaid Customer', N'frieq2@pferilb.tvm-wp.org', CAST(N'1965-11-19T22:19:14.770' AS DateTime), CAST(N'1962-06-29T01:16:07.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7be815b1-5d39-39aa-65e4-3d5855955207', N'Cheri', N'Higgins', N'Raptanilar  Inc', N'Service', N'meufxs.lomwx@ygcm.oblewd.net', CAST(N'2014-01-26T17:25:07.420' AS DateTime), CAST(N'1957-08-05T06:35:06.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7c6c030a-8dd0-e319-a752-3d62bd6faace', N'Clifford', N'Avery', NULL, N'Consumer Marketing', N'omuug@qcfsbstlr.wmtevq.com', CAST(N'2001-04-10T09:37:39.500' AS DateTime), CAST(N'1967-06-07T23:38:35.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a671ff0a-4665-f5be-a52b-3e2ed1993283', N'Mike', N'Hoover', NULL, N'Service', N'kqdq624@kxicqk.net', CAST(N'1984-05-18T20:04:08.040' AS DateTime), CAST(N'2005-07-29T08:09:00.560' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7fbf87d4-7c3d-2c1a-dd5a-3ebeb1ab9121', N'Forrest', N'Terrell', N'Tuperar  Corp.', N'Web', N'nttjra.dgboq@lvhkya.-txln-.com', CAST(N'1954-02-27T12:48:06.080' AS DateTime), CAST(N'1969-11-18T05:56:51.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'35828ee9-1163-b31b-91d3-3f21320f0ba1', N'Terry', N'Riley', N'Tipcadex Holdings Group', N'Web', N'rejtvrbb@wzvf-e.org', CAST(N'1965-11-20T22:57:39.330' AS DateTime), CAST(N'2017-04-26T04:13:12.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9dc0889b-b5e7-a83f-4cba-3f864551b8a8', N'Myra', N'Thornton', NULL, N'Technical', N'sxfwu.gbzhquv@mhwsiuxqb.nz-sue.org', CAST(N'1974-01-05T07:30:02.060' AS DateTime), CAST(N'1993-09-04T23:10:54.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'699594fc-8567-ff40-bfaa-3f93ea74cfe9', N'Teri', N'Spence', N'Emtanplin WorldWide Company', N'Corporate Marketing', N'uear2@g--ltz.net', CAST(N'1978-11-05T20:47:45.030' AS DateTime), CAST(N'1968-10-17T23:08:07.860' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fe2069ea-a9b4-2259-8d50-3fcc75eb5631', N'Vicky', N'Wiley', N'Endzapilentor Holdings Company', N'Accounting', N'ftgd972@ifmt.gzkmq-.org', CAST(N'2015-11-08T23:03:45.040' AS DateTime), CAST(N'1997-05-02T11:47:02.080' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7c7d6d41-1f5c-31dc-9369-3fd5d70bf5b2', N'Ernest', N'Church', N'Cipsipan  Inc', N'Service', N'ksqrc01@phhh.ilkheo.net', CAST(N'2002-07-25T02:33:09.010' AS DateTime), CAST(N'1997-10-11T00:47:44.120' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6fe9639c-422f-d401-eda8-404b0f24e51a', N'Levi', N'Burke', N'Hapglibedan  ', N'Prepaid Customer', N'zbqddv05@lpgtlc.com', CAST(N'1970-05-28T22:05:27.280' AS DateTime), CAST(N'2016-01-10T07:35:38.480' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3cf28156-3972-f0f0-180a-4054b8bdc193', N'Tammi', N'Mercer', NULL, N'Service', N'nlekphay.uovnkm@nvhblh.org', CAST(N'2001-02-12T18:51:47.990' AS DateTime), CAST(N'1983-04-28T13:28:43.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'03467c84-eaad-024d-b28f-40b5e90ca9af', N'Tabitha', N'Horn', NULL, N'Service', N'qpnxrns.hlrgccmo@awkvzjqei.xspot-.com', CAST(N'1981-09-02T00:20:54.510' AS DateTime), CAST(N'1975-07-04T01:42:32.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fb6eff33-c231-b08a-e06e-4118f840158e', N'Gerard', N'Rich', NULL, N'Web', N'dxdvyhqh.xmiprrr@iavyp.tkakkn.net', CAST(N'1985-01-09T03:27:09.150' AS DateTime), CAST(N'1979-02-28T17:54:05.240' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'eb49798b-ed31-3834-62cc-41835d98d275', N'Sheryl', N'Hanna', NULL, N'Technical', N'djys3@gsoitt.com', CAST(N'2012-05-05T20:37:43.470' AS DateTime), CAST(N'1965-02-05T23:53:37.790' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'37b96da1-dffe-006c-af19-41a9b7111bb7', N'Brandon', N'Chen', NULL, N'Service', N'cupfmx524@ujygxoc.lkvfso.org', CAST(N'1987-02-24T11:56:23.930' AS DateTime), CAST(N'2012-02-08T08:49:01.550' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'657e3be8-8380-8d1d-91bb-41ba2bf65db8', N'Henry', N'Sherman', N'Supvenonicator  Inc', N'Web', N'hhfoq27@jhdtnuzp.mffpls.org', CAST(N'1973-05-19T06:55:50.710' AS DateTime), CAST(N'2005-01-22T09:43:42.500' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd1266889-4c47-9ce1-4485-42f37c9349ec', N'Ralph', N'English', NULL, N'Prepaid Customer', N'sluqdzf4@yvgbyb.com', CAST(N'2009-03-24T14:24:36.910' AS DateTime), CAST(N'1953-12-01T16:26:24.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'eb5f4c86-57b4-9818-8e81-433218f50160', N'Wendy', N'Compton', NULL, N'Accounting', N'tpdpuw7@vwrfhp.net', CAST(N'1993-11-17T22:58:53.930' AS DateTime), CAST(N'1974-02-26T12:05:25.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6651bd73-21a4-3ce0-02d6-43703f18ab3b', N'Efrain', N'Hutchinson', N'Hapzapommantor International ', N'Service', N'xcrqh.rwmcmr@zrawveb.bpobez.com', CAST(N'2009-04-08T14:29:11.530' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'97fdf0a6-d20b-b2d8-dd38-43809d6037d6', N'Julian', N'Cordova', N'Frodudentor International ', N'Service', N'cxyrqwix37@dmncyk.com', CAST(N'1972-11-24T05:52:31.050' AS DateTime), CAST(N'1955-10-05T10:04:02.950' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0f5bbd18-2d78-cb1a-9930-4380d6c34681', N'Arnold', N'Carlson', N'Qwimunover Holdings ', N'Technical', N'tujavg.bbzdjwtcd@hgfifw.net', CAST(N'1996-09-23T13:22:10.040' AS DateTime), CAST(N'1996-01-30T17:54:07.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b88e31ea-991e-c3de-281b-43b0d06a37dc', N'Nick', N'Marshall', N'Refropupover WorldWide ', N'Service', N'xkfy@ljcodh.com', CAST(N'1965-08-09T17:57:33.050' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e4d1afbe-a489-c507-3a73-43fa6d38b456', N'Lora', N'Strong', NULL, N'Service', N'dvphipac.iphnrp@pnzfmo.net', CAST(N'1985-11-02T10:12:36.620' AS DateTime), CAST(N'1984-12-06T23:43:42.200' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'534b2b03-c0bd-159b-7a90-44755d08c155', N'Charles', N'Sanford', N'Cipzapan  Group', N'Web', N'flla.yjhyy@ctmis.c-rdpq.com', CAST(N'1978-10-18T05:49:48.630' AS DateTime), CAST(N'1954-11-15T09:08:29.650' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0dc4ca11-e671-d581-31b5-44eb11e85c28', N'Felix', N'Howe', NULL, N'Technical', N'nxgau.wcdyob@dtujmtw.obftqw.org', CAST(N'1961-09-02T19:03:58.720' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f2f17e24-8914-8af9-56e3-44f534a52fd8', N'John', N'Warner', N'Frowerefar  Inc', N'Technical', N'azgw25@uksdmlp.gtfmvu.org', CAST(N'1982-03-13T13:05:52.540' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3df98292-b8a4-d52b-4f9c-45939a4da247', N'Albert', N'Flowers', N'Tuptanegazz WorldWide ', N'Technical', N'bmsump.dyriaeqqz@dsnkdfn.tquia-.com', CAST(N'2000-10-31T18:46:39.680' AS DateTime), CAST(N'1968-01-01T05:04:07.310' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'410c0a01-8045-be85-51d4-46035f17c82c', N'Charity', N'Bailey', NULL, N'Corporate Sales', N'hupj.vcpx@epss.uorvnp.net', CAST(N'2014-01-06T09:44:03.740' AS DateTime), CAST(N'1990-02-11T17:36:09.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e94bf5f0-1485-3ba3-198b-4605a0d65d89', N'Marla', N'Brennan', NULL, N'Service', N'lbdkm066@atojkml.mc-ufu.net', CAST(N'1985-09-09T09:50:50.270' AS DateTime), CAST(N'1978-07-27T15:29:36.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5d39ff51-c531-f968-3e00-46063942c2dd', N'Randy', N'O''Neill', N'Tippickantor Direct ', N'Web', N'mcfjx9@tpyvag.arwkbb.net', CAST(N'1960-06-22T01:24:07.230' AS DateTime), CAST(N'1970-10-25T22:58:19.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'22bcea00-07b2-52f2-64e0-4651a3a0aaf1', N'Shirley', N'Delgado', N'Tupjubimover Holdings ', N'Accounting', N'gwabtbt@kx-qvz.com', CAST(N'1998-03-21T06:13:08.280' AS DateTime), CAST(N'1960-02-08T05:42:33.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd3c39c88-587e-b452-e0fb-469c30c74261', N'Shaun', N'Andrade', NULL, N'Prepaid Customer', N'kwfbdvt.ugene@dkosnclz.nucouu.org', CAST(N'1954-10-30T07:16:20.610' AS DateTime), CAST(N'1955-11-28T03:54:49.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3ae2672c-d516-ea36-a237-46ab730048cd', N'Dora', N'Moyer', NULL, N'Service', N'lnwys199@vcsq.heyayk.com', CAST(N'1970-11-10T19:49:22.490' AS DateTime), CAST(N'2007-07-16T23:38:05.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7ea8f395-4f26-a8cb-87f8-46ca7ce0f1c9', N'Kerrie', N'Benjamin', NULL, N'Prepaid Customer', N'evuiv70@tilvae.net', CAST(N'1982-09-28T01:51:54.250' AS DateTime), CAST(N'2014-09-08T09:42:15.200' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5c47f009-cad0-e6b4-59ea-46f2cfaf9eb9', N'Jeremy', N'Mc Donald', NULL, N'Prepaid Customer', N'qcyfsw29@fzpnxjjk.j-ilrz.com', CAST(N'2014-01-06T01:48:54.920' AS DateTime), CAST(N'2005-06-30T05:33:18.230' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2b073ff8-8bde-eb24-a2fc-47091cc0282e', N'Pete', N'Lyons', NULL, N'Web', N'deeienwt5@yjxgvjvzm.nsxugw.net', CAST(N'2014-03-12T19:10:05.920' AS DateTime), CAST(N'1970-08-14T20:18:48.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fdeb567d-520c-392b-6480-471fb1dda1e6', N'Nathaniel', N'Murray', N'Wintanexazz  Company', N'Service', N'hozipits7@ndvskwy.kpfjka.org', CAST(N'1973-10-14T08:26:06.310' AS DateTime), CAST(N'2006-01-26T21:35:29.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e9898fe9-8582-c00c-3d2e-47786e0451c6', N'Theodore', N'Mc Dowell', N'Rappickefax International ', N'Accounting', N'npag@ryqzs.hogrni.net', CAST(N'2000-10-08T02:28:57.820' AS DateTime), CAST(N'2004-08-04T06:53:12.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'26ca9df8-b57b-2b21-877f-48a3a578438d', N'Jack', N'Sullivan', NULL, N'Service', N'tpxvfznw.gudfkewf@kuhehf.-gnapo.com', CAST(N'1983-08-24T18:28:41.770' AS DateTime), CAST(N'1972-12-12T19:25:29.110' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'acd651a1-3d00-345a-a525-48e5d028cbe7', N'Ray', N'Mc Gee', NULL, N'Accounting', N'kwlh977@unvtc.ivxxjm.org', CAST(N'1980-02-03T18:02:20.310' AS DateTime), CAST(N'1994-08-12T22:28:03.000' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e359811a-9ae5-e0e9-a488-493cc527d768', N'Misty', N'Leonard', NULL, N'Customer', N'iiwyqdi9@eibexj.net', CAST(N'1977-09-23T15:36:32.730' AS DateTime), CAST(N'1997-10-13T12:16:36.440' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6fc81ef3-2e1f-9771-79f1-493f38b0b3f4', N'Mindy', N'Orozco', N'Suprobaquar  ', N'Accounting', N'mrlmnsgy92@jeuhx.dxlvkv.org', CAST(N'1998-11-26T21:17:19.470' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ae33c505-9764-2394-ab8c-4989a0ee7641', N'Bradford', N'Stafford', NULL, N'Corporate Marketing', N'zplwxvti46@rjcgcq.net', CAST(N'1996-07-02T19:27:06.570' AS DateTime), CAST(N'1981-07-06T05:24:35.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bd2b21ad-6667-8fb2-b2ce-49ca7219ef76', N'Staci', N'Giles', N'Froglibentor  Company', N'Technical', N'yepcqkjf99@frtfapph.jy-jrh.com', CAST(N'2003-11-22T04:05:06.080' AS DateTime), CAST(N'1981-03-15T04:56:43.020' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0981e036-4a27-665c-9182-49d0bbe5f214', N'Marcella', N'Cameron', NULL, N'Accessory Customer', N'kzzrbtyk6@pqmfmkol.xrfds-.com', CAST(N'1958-03-07T22:05:08.190' AS DateTime), CAST(N'2013-04-16T12:54:22.750' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9e8ec18d-8013-334b-4a86-4a083c765c8b', N'Chastity', N'Mills', N'Thrunipopin  Inc', N'Accounting', N'yvqwe.scgomvbnv@cshydt.org', CAST(N'1978-06-11T13:24:00.720' AS DateTime), CAST(N'1963-11-26T15:13:16.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a327a627-4199-e783-e291-4a549e8fcfa9', N'Heidi', N'Harrell', N'Frozapex International Inc', N'Technical', N'ktwtxly@prsgef.com', CAST(N'1987-02-14T01:01:30.930' AS DateTime), CAST(N'2006-06-15T02:25:28.200' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'09b32be4-eaea-10af-218f-4a60703ecc2b', N'Brandon', N'Allison', NULL, N'Web', N'eixgla332@esyik.mapwor.org', CAST(N'1985-07-18T17:00:24.460' AS DateTime), CAST(N'2005-01-18T23:58:18.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1aa57a5b-930a-ab2c-edc8-4a66de3a1e90', N'Jean', N'Haynes', N'Qwipickover  ', N'Web', N'gaaoe6@x-dtjt.org', CAST(N'2007-10-28T13:34:29.940' AS DateTime), CAST(N'1991-06-13T00:41:44.870' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5f23d050-d077-7ee2-df4d-4b0eefc27b04', N'Phillip', N'Sexton', N'Zeesipazz Holdings ', N'Accounting', N'egkw9@kosygm.org', CAST(N'2010-07-18T17:35:10.750' AS DateTime), CAST(N'1994-09-15T06:36:55.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4a713e9b-c4de-3b99-d032-4b562727bb9c', N'Angelo', N'Mccann', NULL, N'Marketing', N'cfqig8@vpohog.hvyxhz.com', CAST(N'1981-08-21T18:58:34.310' AS DateTime), CAST(N'1985-11-04T16:07:52.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a111a04d-3ff4-733b-bbd9-4b5c66759375', N'Donnell', N'Mayer', NULL, N'Accounting', N'cxblp@vmfncj.org', CAST(N'1959-07-07T03:40:07.750' AS DateTime), CAST(N'1955-10-21T21:56:34.970' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd2c8d433-aae9-0831-e030-4b891893abac', N'Wanda', N'O''Neal', NULL, N'Web', N'fjkvino50@dsqdzh.com', CAST(N'2012-11-28T12:02:53.270' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a4bc23a3-c170-c394-0372-4bd6d287f333', N'Kara', N'Barry', NULL, N'Technical', N'ldpueqg32@pp-hsw.com', CAST(N'2000-06-05T12:08:28.050' AS DateTime), CAST(N'2002-12-05T21:36:16.550' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'666236cc-068b-acd6-3208-4bf45b7105cf', N'Drew', N'Mc Dowell', N'Tipsapanex  ', N'Marketing', N'owwyhd2@qllptkt.co-gon.com', CAST(N'1994-02-02T22:41:52.120' AS DateTime), CAST(N'2005-11-14T23:27:28.630' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3d11e4a5-f418-9252-9af3-4cafba5e409d', N'Katrina', N'Fry', N'Uptumackentor  ', N'Customer', N'xlnvua@lrcgvf.org', CAST(N'1978-04-01T22:51:14.870' AS DateTime), CAST(N'1956-04-12T10:34:53.160' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f313a0fc-445c-cf83-068c-4cd959c5865e', N'Willie', N'Woods', NULL, N'Web', N'dvbd8@htzygi.com', CAST(N'2007-12-19T11:48:17.690' AS DateTime), CAST(N'1965-10-11T17:31:34.070' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0aa082b4-3e2e-120c-ae62-4cfccaf907d3', N'Austin', N'Lynn', NULL, N'Prepaid Customer', N'cwbbcotc7@etypui.net', CAST(N'1963-08-04T11:50:33.770' AS DateTime), CAST(N'1995-01-31T23:11:54.890' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bc62dbcb-88dc-b11e-2e0d-4d06e7de9722', N'Krista', N'Robinson', NULL, N'Accounting', N'crnki@qnffjg.org', CAST(N'1985-05-23T02:43:23.110' AS DateTime), CAST(N'2014-07-26T16:19:28.300' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0fc77d6e-134c-0d88-acfe-4d10777b3712', N'Rickey', N'Santos', N'Adbanefax Holdings Group', N'Prepaid Customer', N'viyx47@ovpeeb.com', CAST(N'1979-07-14T12:42:50.190' AS DateTime), CAST(N'1991-05-29T07:55:58.910' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd7ba1a6c-ea19-28ee-fda1-4d2b43e77c02', N'Angela', N'Decker', NULL, N'Web', N'rrwmo0@oxfmlj.org', CAST(N'1998-05-06T22:55:01.370' AS DateTime), CAST(N'2015-05-19T07:59:08.040' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'042c9909-edf2-b816-0de5-4d5f4db0b25c', N'Kerrie', N'Mccall', N'Rapdudefar  ', N'Web', N'onbrspn.tsjrzmxju@ilom.vywelk.net', CAST(N'2006-08-22T01:53:35.610' AS DateTime), CAST(N'1976-01-23T16:23:07.540' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'627018cb-6bce-8578-6554-4d6453f0d5af', N'Andrew', N'Bowen', NULL, N'Technical', N'pvyd.uvfg@xeoptd.net', CAST(N'2012-11-20T13:25:51.140' AS DateTime), CAST(N'2009-07-06T07:54:05.000' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8f1351b7-4ee4-e2f6-47b8-4d71aa86a85a', N'Margarita', N'Tate', NULL, N'Service', N'fprsvca8@mwpjaa.org', CAST(N'1980-12-10T08:23:04.320' AS DateTime), CAST(N'2003-04-18T01:58:01.770' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b2a4f0b0-a7c7-cafd-f374-4d76224b954d', N'Lisa', N'Elliott', N'Undudicator Holdings ', N'Technical', N'fzrprv.cjfheed@esfxcjh.qjgxta.net', CAST(N'1975-04-01T15:50:51.270' AS DateTime), CAST(N'2012-08-30T00:56:28.570' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'38946839-bc36-667c-36fe-4df1b3aa287d', N'Kristy', N'O''Neal', NULL, N'Prepaid Customer', N'ucehqpu@ceadam.org', CAST(N'1991-09-06T04:09:03.410' AS DateTime), CAST(N'1980-04-21T09:22:58.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'90fb640d-83ae-a9f7-e070-4e8018966a7c', N'Devin', N'Benson', N'Enddimower International ', N'Web', N'gqtqoovv.ejkttrzht@p-blrq.com', CAST(N'1988-06-04T23:47:37.440' AS DateTime), CAST(N'1994-05-21T12:22:37.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'95d03b0a-78b5-dc9f-6db3-4ea8eaed1193', N'Derick', N'Cooke', NULL, N'Web', N'ldmoqzl@ykdfos.com', CAST(N'2006-05-07T18:16:07.330' AS DateTime), CAST(N'1983-02-07T03:27:31.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f2ca0e6b-f535-01a6-11d7-4ebdef9f666e', N'Felix', N'Simmons', NULL, N'Service', N'zgemz.ivpbbl@pkprso.zmwqjv.net', CAST(N'1959-12-21T03:09:43.910' AS DateTime), CAST(N'1994-08-08T10:33:58.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd159c73d-f349-b4b0-57f8-4ee62f70d498', N'Brady', N'Ali', NULL, N'Prepaid Customer', N'irzinua.djmublq@ismatd.com', CAST(N'1955-07-08T09:34:16.800' AS DateTime), CAST(N'1988-06-08T08:48:36.170' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8e9e5531-196f-d417-0cc1-4f23a75548de', N'Brandi', N'Melendez', NULL, N'Technical', N'flca24@rliumrfn.wlvvgd.net', CAST(N'2013-06-07T15:15:55.900' AS DateTime), CAST(N'2014-06-19T17:22:41.960' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5cf7b805-8f5f-4cfc-449b-4f4c42849d21', N'Aisha', N'Arroyo', NULL, N'Prepaid Customer', N'usxhpxz54@zblyay.org', CAST(N'1968-05-07T12:40:25.600' AS DateTime), CAST(N'2012-11-28T17:45:07.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1eb1327e-43da-86c7-2a0f-4f5dda429e02', N'Jim', N'Wells', NULL, N'Prepaid Customer', N'ojvn@-acngr.com', CAST(N'1964-12-04T17:33:04.830' AS DateTime), CAST(N'1959-03-02T06:16:15.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f1d06352-63d2-8b8e-a9c3-503d4c745143', N'Don', N'Shannon', NULL, N'Prepaid Customer', N'ouochs.rcztervanu@zcemvy.yiyfos.org', CAST(N'1958-04-10T16:55:57.910' AS DateTime), CAST(N'1980-10-24T07:07:36.170' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fbe92c7e-8e20-6282-2cf9-511cdb5daa62', N'Roy', N'Brady', N'Emtanor Direct ', N'Web', N'iidw@wqpmrg.net', CAST(N'1982-08-11T05:03:52.480' AS DateTime), CAST(N'1959-01-09T13:51:14.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fa5ef110-4ff1-6a7f-e2f1-5122693c0fdb', N'Zachary', N'Hardin', N'Thruwerax  Group', N'Accounting', N'xxedpwsl.wwynpqmjvq@dmnuse.com', CAST(N'1991-07-15T07:28:57.430' AS DateTime), CAST(N'1954-06-05T16:12:27.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'90c758bf-3cff-30fc-1010-5152ff47b359', N'Ray', N'Maynard', NULL, N'Service', N'rrqov.qsyvn@ohjn.gvgymb.com', CAST(N'1971-10-13T14:44:27.460' AS DateTime), CAST(N'2009-05-17T12:40:08.000' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'561436da-99bc-f1a1-334d-5160c04a03ed', N'Victor', N'Lyons', N'Tipglibin  ', N'Technical', N'bhypxfs.wabgnafvhh@ogptaa.m-xant.org', CAST(N'1981-03-01T11:47:28.180' AS DateTime), CAST(N'2015-02-15T06:05:00.300' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'86b4f611-c070-0994-46a4-518a56f24ac1', N'Heather', N'Chambers', N'Raptinex  ', N'National Marketing', N'ggoxqn54@ebrzge.com', CAST(N'2017-01-01T13:49:03.450' AS DateTime), CAST(N'2009-06-09T16:47:55.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ef444b51-e87a-883f-23e2-5190ec9a529a', N'Leo', N'Lopez', N'Surrobilantor WorldWide ', N'Consumer Marketing', N'wuvmcu78@augtxy.com', CAST(N'2014-09-16T11:04:04.640' AS DateTime), CAST(N'1957-11-12T10:32:40.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8a816db1-2dc2-8b57-eae0-51b514f04695', N'Arthur', N'Drake', NULL, N'Marketing', N'jbhwby.gmfsgwjrpd@kwl-lg.org', CAST(N'1972-12-01T17:36:43.140' AS DateTime), CAST(N'2007-02-10T20:50:30.590' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bcf33b19-2739-2e46-9b2f-51e1755dafea', N'Jeanine', N'Petersen', NULL, N'Accounting', N'hplsbf.typgrjcj@nuriva.org', CAST(N'2013-12-14T01:10:41.890' AS DateTime), CAST(N'1967-03-25T13:39:25.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b45181b5-2490-4d74-d1cd-51e26446ce3b', N'Molly', N'Schmidt', NULL, N'Web', N'rybtxofx6@vbjvakmwu.ywbcez.com', CAST(N'1970-05-25T03:04:03.620' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'78496b1c-b6e0-c6f7-0356-5253b1011e64', N'April', N'Huff', N'Zeebanin International ', N'Prepaid Customer', N'qtzcueyb772@jbnexakno.qjzwbt.com', CAST(N'2015-12-06T01:04:58.200' AS DateTime), CAST(N'2006-05-27T16:43:50.200' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5e4cd648-8222-65b9-d788-5274cb220b75', N'Carlos', N'Jensen', NULL, N'Technical', N'adzef.zaxvvyiae@kgubyffp.mcnith.net', CAST(N'1991-05-16T13:05:42.000' AS DateTime), CAST(N'1974-01-04T18:07:37.300' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cf476a7e-ca28-5c68-82ca-5282dc44f3aa', N'Quentin', N'Green', N'Rapmunover  Group', N'Web', N'yzhnuedv8@ircqrxhgq.fkpyod.org', CAST(N'2015-10-26T10:30:55.090' AS DateTime), CAST(N'1954-03-25T19:09:03.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ee978c7b-83ef-0001-b027-52d56b8db100', N'Chandra', N'Choi', NULL, N'Technical', N'kxug@uidoeo.ieaona.org', CAST(N'1984-11-29T22:01:06.810' AS DateTime), CAST(N'1964-04-11T13:10:35.100' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a96aa8ae-42f4-aa8c-574b-5306d8d3b7a2', N'Sarah', N'Buchanan', N'Qwitanantor Direct ', N'International Sales', N'hmrk.snanzryxw@ofwloz.org', CAST(N'2010-06-14T11:13:43.540' AS DateTime), CAST(N'1990-12-27T02:31:38.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3bc73d83-5d9a-a9f6-2bb9-530d31d455b2', N'Dylan', N'Russo', NULL, N'Web', N'aafeggt5@vflrwn.net', CAST(N'1992-03-01T16:27:45.310' AS DateTime), CAST(N'1984-12-10T13:15:48.690' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ee63ef52-5cb0-731d-457f-5330429d96e8', N'Lindsay', N'Henderson', N'Zeekilaquex  ', N'Web', N'ehzz.eijfvd@pnfodh.com', CAST(N'1960-05-24T17:10:01.990' AS DateTime), CAST(N'1989-12-30T19:45:39.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'aa36e581-60cd-98da-fd80-54d707b3a5b4', N'Monica', N'Conway', N'Dopfropin  Company', N'Service', N'iubllacg.pwirlydg@hicsyu.org', CAST(N'1964-01-04T13:55:16.430' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5e8e68d3-9bd6-060a-a55c-54f06ae5730b', N'Moses', N'Harrington', NULL, N'Service', N'dcbxdje.xirtzj@psuygl.net', CAST(N'1968-07-19T01:41:23.420' AS DateTime), CAST(N'2004-09-02T01:59:03.620' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a255a9ab-5d8b-4ff8-ac60-554851ed1ff6', N'Arlene', N'Fischer', NULL, N'Accounting', N'tdxirs.glwb@zlvzabx.asdu-t.com', CAST(N'1983-09-22T18:21:16.680' AS DateTime), CAST(N'1956-11-25T20:44:21.260' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'76ed3075-0429-12ec-d493-55bf5dd03585', N'Shari', N'Dennis', NULL, N'Service', N'gkbai@gdknyq.com', CAST(N'1990-01-17T13:39:42.030' AS DateTime), CAST(N'2016-08-24T03:48:07.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e002b7fd-e7dd-a8cb-1a42-55f912567e03', N'Stephanie', N'Rush', N'Haptumilistor  ', N'Customer', N'oqeltx25@jsimmq.net', CAST(N'1964-12-11T19:33:49.330' AS DateTime), CAST(N'1979-09-21T22:55:18.770' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8af6665e-5cf9-9e45-daa2-561239280386', N'Trisha', N'Leach', NULL, N'Web', N'hckyk90@wogfg.xgjica.com', CAST(N'1974-08-08T01:26:15.050' AS DateTime), CAST(N'1978-11-08T05:01:47.140' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'eeab3c7a-bf3c-aca8-64cf-57493bc611ec', N'Rita', N'Jenkins', N'Barcadex Direct Group', N'Accessory Sales', N'tqfh.zgvglh@zaziiv.chagnf.net', CAST(N'1967-12-26T17:03:12.970' AS DateTime), CAST(N'1981-02-25T09:56:37.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5bd6bdf3-5676-3cc8-3379-5764427be66e', N'Clifton', N'Watson', N'Ciptanantor Holdings ', N'Accounting', N'cvqz.earxuhamx@hqhogwt.uoarzv.com', CAST(N'1982-02-18T12:38:21.530' AS DateTime), CAST(N'1978-12-30T02:27:32.590' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0f17af71-a953-ecec-b275-5769657e3d34', N'Caleb', N'Gardner', N'Emwerazz  Inc', N'Prepaid Customer', N'ompdxwet9@lfaker.net', CAST(N'1967-06-05T12:22:27.510' AS DateTime), CAST(N'1970-02-01T21:42:38.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'193abdc4-f255-379d-8505-57e09123adcb', N'Alissa', N'Donaldson', N'Tipveneficator WorldWide ', N'National Marketing', N'bpnqiwkj.tctkur@-jbzep.com', CAST(N'1976-07-15T13:45:27.290' AS DateTime), CAST(N'1996-02-15T22:04:28.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'49849363-2487-4351-0761-57f581b71e46', N'Regina', N'Forbes', N'Barquestegover International Group', N'Service', N'uximngtb056@zbztf.dxvqxc.net', CAST(N'1956-06-13T18:26:30.780' AS DateTime), CAST(N'1970-07-15T19:26:45.920' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0e7284ab-d45b-8d33-8b8c-581e8ecc5cc3', N'Oliver', N'Sheppard', NULL, N'Technical', N'sqmolr295@kodpfq.net', CAST(N'1986-03-23T07:50:14.620' AS DateTime), CAST(N'1963-12-19T23:59:02.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'88274f9d-1358-da1d-69e9-58229df61a49', N'Colin', N'Cohen', NULL, N'Business Customer', N'gzeksgu170@jtbrpkwhf.-cdxgo.com', CAST(N'1988-04-23T03:55:02.520' AS DateTime), CAST(N'1998-08-25T03:30:54.800' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'77845d60-8041-03a2-976d-582e6f466e25', N'Sheryl', N'Dorsey', N'Barkilin  Company', N'Business Customer', N'dlkm.yogo@dnob.euobht.org', CAST(N'1963-08-19T04:24:21.090' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'09c3feea-fb6a-069b-7d99-585e122827a0', N'Isabel', N'Duffy', N'Upbanefentor WorldWide Company', N'Business Marketing', N'hsiuokue0@pqiiyxg.wfqwey.org', CAST(N'1955-07-27T12:04:16.030' AS DateTime), CAST(N'2003-09-05T21:20:38.870' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'edfb58a5-808f-6501-e491-5861824cddc7', N'Susana', N'Adkins', N'Adkilentor International ', N'Web', N'rvbzoj.jlvcifrk@fdkprjz.epudsc.org', CAST(N'1996-07-30T02:45:32.450' AS DateTime), CAST(N'1953-12-27T11:15:58.860' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1d832846-5931-e275-2a3f-587e748564a4', N'Warren', N'Ochoa', N'Grorobentor WorldWide ', N'National Sales', N'vzxoxxl@jdisku.org', CAST(N'1997-01-07T10:19:59.650' AS DateTime), CAST(N'1969-05-26T01:12:34.630' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'133b7235-5778-c21d-f5b7-588ee709bf6d', N'Guy', N'Hines', NULL, N'Technical', N'hnebc.zjokwyyszi@gundu.vlgbkt.com', CAST(N'1994-12-14T12:35:57.970' AS DateTime), CAST(N'1963-10-22T07:18:50.080' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'52a23ffb-91ad-bcc0-e93b-58abef6b787a', N'Oliver', N'Copeland', N'Monquesticator Direct Inc', N'Technical', NULL, CAST(N'1990-09-01T06:31:13.150' AS DateTime), CAST(N'1982-12-06T10:34:41.300' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4d787436-cc2c-9f68-77d5-58b34e6c4257', N'Alicia', N'Cortez', N'Zeepebover Holdings Group', N'Web', N'qzir.tmqteyzjc@adeu-b.net', CAST(N'1974-07-11T19:43:29.540' AS DateTime), CAST(N'2016-11-17T14:26:32.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'26b27b26-e6ee-bbf1-7174-595025509579', N'Jodi', N'Barnes', N'Winbanazz Direct Inc', N'Web', N'cfewral9@lyglmul.tue-ah.net', CAST(N'1990-12-23T00:04:12.180' AS DateTime), CAST(N'1970-05-07T05:57:16.210' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3799dfc8-7b2d-89ca-743a-59c92e1c4f45', N'Brandy', N'Rosales', N'Parzapefover International ', N'Web', N'ujfw@cxlyh.nrvwzl.net', CAST(N'2013-04-08T09:37:20.440' AS DateTime), CAST(N'2000-09-28T13:48:24.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4c80e1ef-e9ac-451a-193b-59d997074b0a', N'Felipe', N'Cherry', NULL, N'Prepaid Customer', N'xxtqrac.euhkfgl@ffhuvi.ybefyk.com', CAST(N'1983-05-06T16:46:35.840' AS DateTime), CAST(N'1968-08-17T23:43:26.970' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b98474e7-743b-3d3d-0281-59ec5378fd66', N'Mike', N'Velasquez', N'Rapzapover International ', N'Accounting', N'rgzwf@zsds.-mxujj.com', CAST(N'1981-03-16T00:54:47.990' AS DateTime), CAST(N'1954-03-31T09:05:48.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fa9cbc1d-7521-a4a1-b204-5a59ec1457bc', N'Tommy', N'Rosario', N'Supcadantor International ', N'Technical', N'dfaqd2@ypr-cy.net', CAST(N'1983-06-28T10:46:24.110' AS DateTime), CAST(N'2013-01-19T01:12:28.160' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'899e4fc5-4810-0f60-ddd3-5b3db85a02c3', N'Milton', N'Good', NULL, N'Web', N'ehtxr67@qbbimh.net', CAST(N'1979-11-21T13:14:41.830' AS DateTime), CAST(N'1984-12-04T16:01:19.970' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'66a3a4a3-6315-4974-df77-5b52f0fa2fa7', N'Tonia', N'Obrien', NULL, N'Technical', N'wtyeox.gxjwe@rltqx.bvmlkv.com', CAST(N'2006-12-15T22:39:25.390' AS DateTime), CAST(N'1966-05-16T10:02:28.890' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7eef051f-5a0b-6568-77d8-5b8d4d6b646b', N'Rosemary', N'Levine', NULL, N'Consumer Marketing', N'hrpqod.fknmdtlti@tiyhju.com', CAST(N'2003-11-28T20:15:29.860' AS DateTime), CAST(N'1987-08-01T16:46:17.550' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fc24c93a-612c-3958-0380-5b9b5b134e9f', N'Jimmy', N'Phillips', NULL, N'Service', N'wajqb.onshw@-hyyoj.com', CAST(N'2014-06-14T03:43:23.360' AS DateTime), CAST(N'2011-07-15T12:28:57.120' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f5b29f7c-3f47-fac4-05bf-5bae35d65b65', N'Roger', N'Richardson', NULL, N'Service', N'jagwg72@jsbvvpnek.xodf-w.com', CAST(N'2012-05-21T21:07:20.610' AS DateTime), CAST(N'1956-10-01T20:45:03.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'26a05e14-0c0c-3fc7-533d-5bcbc3fcade3', N'David', N'Warren', N'Truwerinar Direct Group', N'Service', N'burnzfje128@cifjxylyf.dtixuv.com', CAST(N'1960-07-20T02:57:08.330' AS DateTime), CAST(N'1998-02-15T09:46:16.850' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'58805252-fcd9-08e7-619f-5bcbd1691d90', N'Tom', N'Zamora', N'Thrududupistor WorldWide ', N'Accounting', N'ilcuejl.frsl@bhogvb.tyhzrf.org', CAST(N'2001-03-14T06:09:27.740' AS DateTime), CAST(N'1993-02-12T10:31:10.800' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'178cf09b-f48a-5a21-fe92-5bfac20585a9', N'Shelby', N'Duncan', NULL, N'Web', N'mjvnw.wwerpaxfny@myfphu.com', CAST(N'1967-12-02T04:59:47.070' AS DateTime), CAST(N'2001-07-01T21:25:19.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9bcb97bb-897c-68e7-f30c-5c050489e3b9', N'Trent', N'Glass', NULL, N'Consumer Customer', N'gnbagqf.qsqlln@atrhil.com', CAST(N'2011-02-05T13:09:58.070' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'861a1c0c-ccd4-9d82-f634-5c20e6279f3e', N'Tyler', N'Gates', N'Doperefin  ', N'Corporate Marketing', N'uaau@oegjea.org', CAST(N'2011-07-13T11:34:24.410' AS DateTime), CAST(N'1972-05-31T10:57:46.890' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cf23caed-bd72-682c-024d-5c3d5a8af49c', N'Leon', N'Jackson', NULL, N'Accounting', N'cixw@xvjgca.org', CAST(N'2011-04-26T22:53:56.910' AS DateTime), CAST(N'1954-03-01T07:39:57.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e46e7b16-7018-5141-377a-5c482f735ecf', N'Noel', N'Peters', NULL, N'Accounting', N'fufkakzt812@gszanx.org', CAST(N'1966-05-13T00:40:35.210' AS DateTime), CAST(N'1992-03-27T09:21:33.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c0152988-5e1b-7a2b-b743-5cd587cd68fa', N'Erica', N'Frey', NULL, N'Accounting', N'vpwrvmdx50@nakbep.org', CAST(N'2001-04-15T22:22:38.540' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'90ff1cc5-afd0-328c-d465-5ce1323fd5c0', N'Sherrie', N'Carr', N'Surzapover Direct Inc', N'Prepaid Customer', N'ndrkad@juusoedsx.gybrjm.com', CAST(N'1961-12-25T13:03:28.520' AS DateTime), CAST(N'1982-09-04T22:15:14.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2caff05d-7f6e-a9f2-faa7-5d0b4de57c6f', N'Rachelle', N'Olson', N'Parnipopan Direct ', N'Corporate Sales', N'ibtbk.mazneecs@gzvmyeci.ftswoz.com', CAST(N'1974-05-02T11:30:49.960' AS DateTime), CAST(N'2011-09-24T01:42:29.880' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e12b2f16-8bfe-3bfc-0971-5d0c4806c504', N'Lorenzo', N'Hurley', NULL, N'Prepaid Customer', N'afkz.ulkmbahfii@bziddd.bkvxoq.org', CAST(N'2003-08-18T05:30:17.350' AS DateTime), CAST(N'1989-07-08T03:01:08.690' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0706105e-ac96-b575-fd17-5d7d25024c91', N'Lisa', N'Morrow', NULL, N'Service', N'zxejlovd1@cnrbx.ouaevt.com', CAST(N'1978-03-27T05:55:31.990' AS DateTime), CAST(N'2000-09-24T21:50:41.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'27bc56d8-044e-247b-91b8-5d80478e6b4d', N'Lora', N'Rose', N'Monzapar Holdings Group', N'Technical', N'xcjpvz@fjepmibvc.vogvdp.net', CAST(N'1966-02-20T04:51:56.110' AS DateTime), CAST(N'1982-01-20T11:22:09.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ade26f20-b028-3fd4-08f9-5d85526d5aef', N'Shane', N'Mills', N'Emtinackazz International ', N'Sales', N'ueaenyf.xlridlwwo@afdeyy.com', CAST(N'2009-11-08T07:43:18.840' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'09507f84-2108-305e-a561-5d8fda9c397f', N'Tasha', N'Berg', NULL, N'Prepaid Customer', N'luoxq.vqqipexns@trgrgg.org', CAST(N'1958-02-10T03:24:23.850' AS DateTime), CAST(N'1977-02-11T15:38:39.540' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ec48face-b1e6-877f-4ec4-5d992adfa63a', N'Nathan', N'Rivas', N'Grodudexower Direct ', N'Technical', N'icyw.fkgbxvqhzs@jgp-dk.com', CAST(N'1995-08-23T05:40:59.810' AS DateTime), CAST(N'2018-08-08T18:22:43.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'291172c1-c9a9-06e9-768a-5e7fa6dbefbc', N'Lorenzo', N'O''Connell', NULL, N'Technical', N'emsdrhu@dwii.zbcdec.net', CAST(N'1968-09-08T06:05:07.160' AS DateTime), CAST(N'1984-03-12T19:42:26.920' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4533ccc1-b08b-cc55-65af-5ecafb013262', N'Armando', N'Ellison', NULL, N'Web', N'knvx61@npo-rv.org', CAST(N'1963-04-21T12:15:06.760' AS DateTime), CAST(N'1979-12-08T13:15:53.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5c062ee9-4077-2460-c13a-5f1e3433368b', N'Antonio', N'Dougherty', N'Lomquestilex  ', N'Accounting', N'utdyonh980@mzmsdp.org', CAST(N'1983-05-03T20:09:23.360' AS DateTime), CAST(N'1996-11-11T23:37:03.550' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'63dd5192-750f-5983-4d46-5f3e32aa953d', N'Kathy', N'Peck', N'Varwerpar Direct ', N'Corporate Marketing', N'nrbaib6@mwfnja.org', CAST(N'1964-03-24T12:13:49.090' AS DateTime), CAST(N'2006-02-14T06:07:19.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9e603a8c-a2be-e820-c69b-5fc3fc699e0a', N'Toni', N'Little', NULL, N'Technical', N'bvysojz.vskt@ydfr-i.com', CAST(N'1960-07-23T19:30:18.740' AS DateTime), CAST(N'1980-03-06T01:56:40.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6db08f84-cbaa-27b3-fc0f-5fd62832b2bf', N'Leonardo', N'Marshall', NULL, N'Accounting', N'jkgukvx256@fmvgvnxbv.-tqfhc.net', CAST(N'1994-05-08T04:07:38.960' AS DateTime), CAST(N'1982-09-10T17:15:17.140' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ead1769d-8585-cbfc-b7a8-60312a9cb2cb', N'Roxanne', N'Benjamin', NULL, N'Web', N'aobhp5@yxnuvx.com', CAST(N'1996-04-17T22:50:52.980' AS DateTime), CAST(N'1996-12-10T21:21:54.950' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f7e156df-e7ea-2fcd-6c48-609765ac8e2f', N'Vernon', N'Palmer', NULL, N'Technical', N'ticv1@rsxufu.net', CAST(N'1960-08-26T06:49:26.850' AS DateTime), CAST(N'2015-06-10T18:31:02.280' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'da6cd1df-c621-44ad-375d-609c9e3d6052', N'Aaron', N'Landry', N'Lommunopax Holdings ', N'Prepaid Customer', N'wointi.qiunodix@q-qqgq.org', CAST(N'1987-02-15T11:52:40.380' AS DateTime), CAST(N'1995-07-04T00:52:11.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f04f40d7-065d-b26b-d30d-60b54dde4691', N'Charity', N'Harrell', NULL, N'Technical', N'hqfahy3@cksxem.njostx.org', CAST(N'1976-07-11T13:29:20.270' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'24604b18-3b24-f414-343b-60fe6fa4af91', N'Shad', N'Lam', N'Gropickexentor  ', N'Prepaid Customer', N'rgunsi506@-rmyck.net', CAST(N'1993-12-28T06:02:39.800' AS DateTime), CAST(N'2008-06-17T14:27:51.070' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ab64dcc8-5a0d-0749-7a7e-6158568ee832', N'Marisol', N'Mayo', NULL, N'Marketing', N'pcxbnofj.hgcgqin@pznood.com', CAST(N'2005-02-19T16:27:51.660' AS DateTime), CAST(N'1955-03-01T17:41:29.260' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd115e1cb-70a2-04db-2f82-626085624ad5', N'Earnest', N'Walters', NULL, N'Prepaid Customer', N'cmrmxlw@nijs.ixeii-.org', CAST(N'1961-03-08T15:34:55.600' AS DateTime), CAST(N'2014-12-11T01:35:24.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'72f0f84a-c3a2-0346-5f64-626d30d41ff0', N'Stacy', N'Thompson', N'Bardudentor Holdings Group', N'Accounting', N'rduu2@jahvf.nrjeik.org', CAST(N'1983-03-09T11:29:50.170' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'19f5366f-864a-fe18-0cc5-62a5e9245455', N'Roland', N'Grimes', N'Qwisapor Direct ', N'Customer', N'itsx@mbceaxlym.hkrfqu.net', CAST(N'1998-06-17T14:51:12.410' AS DateTime), CAST(N'1986-07-18T01:05:17.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'579f3943-e1ca-6a85-e1a4-62fae90e7d33', N'Fred', N'Parrish', N'Hapmunexover WorldWide ', N'Sales', N'oxbyir98@npjgr.nlmjkb.net', CAST(N'1973-12-12T05:31:57.850' AS DateTime), CAST(N'1986-02-14T11:09:54.530' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a0f8c1fa-f149-2cb2-9899-6316ac9a0bf8', N'Kerri', N'Graves', N'Ciphupar Holdings ', N'Technical Customer', N'fhhov@lgpmfc.org', CAST(N'1959-02-03T13:25:16.390' AS DateTime), CAST(N'1969-05-29T07:14:32.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5a23ae4a-4115-a722-0cd0-635f400fe11e', N'Lakeisha', N'Wallace', NULL, N'Web', N'yyfpp.rgkvtrcmi@bgcln.cs---s.org', CAST(N'1975-08-06T13:08:38.440' AS DateTime), CAST(N'2003-03-21T23:27:09.960' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'edf8bc8d-198e-2287-3fa1-63697766bfd6', N'Karin', N'Key', N'Thrujubefentor  ', N'Accounting', N'gsrxuvky@eceuav.ruqxdu.com', CAST(N'1979-09-27T22:00:38.520' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2fefc7ad-eada-812a-20e2-63981537ab60', N'David', N'Novak', N'Addimar  ', N'Prepaid Customer', N'vilsqnmx.nilgasgdgk@egc-ko.org', CAST(N'1986-06-16T06:11:48.050' AS DateTime), CAST(N'1953-08-14T10:14:06.860' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd25514d0-a69c-f268-7305-63d033e90782', N'Bobbie', N'Hurst', NULL, N'Technical', N'lizqya.urxobczm@sqhynexqf.wflvsb.org', CAST(N'2006-02-13T17:17:10.340' AS DateTime), CAST(N'1954-10-02T14:32:34.310' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'40dbd268-2166-8a4e-db1b-63de839b5ee4', N'Bobby', N'Bright', NULL, N'Technical', N'pekiruhz.umxwhjois@kwkpfybk.msoehf.net', CAST(N'1989-06-07T13:57:34.420' AS DateTime), CAST(N'1971-04-19T21:12:55.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'aa6b3675-37b6-d0af-1cb8-64125b02742b', N'Casey', N'Bauer', N'Varrobicator Holdings ', N'Web', N'kpsmket.mjfak@zxplw.xkxwqk.net', CAST(N'1994-07-22T12:06:13.220' AS DateTime), CAST(N'2007-10-15T05:36:11.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0d245227-49b1-0cd5-b2b8-6436052917eb', N'Louis', N'Hayden', N'Rapcadower  ', N'Web', N'phnguqvu@jaew.jbcwdd.org', CAST(N'2005-12-08T11:55:48.750' AS DateTime), CAST(N'1977-02-01T01:35:39.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'68b28bd3-ed09-b7c8-8f97-644845005173', N'Betsy', N'Perkins', NULL, N'International Marketing', N'vwokxvn@tcowzpggq.xvcmop.com', CAST(N'1994-11-10T07:54:50.430' AS DateTime), CAST(N'1990-03-31T17:01:51.840' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'daf0f920-d24f-dd4e-7577-64747c97a6dc', N'Darryl', N'Richmond', N'Hapcadentor Direct ', N'Technical', N'ctshxixf@ssbleq.bzpukl.net', CAST(N'1958-04-29T04:43:59.900' AS DateTime), CAST(N'1966-05-18T15:12:28.630' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'90124dd5-ef9d-b13d-7b17-64fcd6260f0c', N'Demond', N'Ochoa', N'Qwifropanicator WorldWide Company', N'Accounting', N'aixhofzv79@chebsu.net', CAST(N'2000-10-29T04:07:30.650' AS DateTime), CAST(N'1969-09-27T14:49:20.070' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'82a1d6ff-5c8f-dd03-2e0c-652437ec8f6f', N'Allen', N'Ritter', N'Grobanower WorldWide Corp.', N'Customer', N'hqxbwzjx2@mjheoy.cqujdw.com', CAST(N'1971-07-14T03:53:09.270' AS DateTime), CAST(N'1994-04-05T16:37:22.910' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'56a51711-262f-2479-bba1-653d0c12445f', N'Heather', N'Odonnell', N'Enderedgantor Direct Corp.', N'Consumer Sales', N'rkzji72@udpm.plfldh.net', CAST(N'1985-11-19T20:11:58.240' AS DateTime), CAST(N'1955-11-16T04:38:11.060' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9d7b6e45-17f4-2333-8b5e-65c5d85677c0', N'Wallace', N'Sanders', NULL, N'Service', N'vzbq.nayynm@ulppohfh.thomnl.org', CAST(N'1981-03-28T03:36:07.120' AS DateTime), CAST(N'1968-12-31T20:09:56.420' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f11641eb-bd58-38c6-9f05-65e2c0815485', N'Antoine', N'Gamble', NULL, N'Technical', N'ikixoip@udhnyg.net', CAST(N'1984-08-18T21:08:51.750' AS DateTime), CAST(N'2005-11-10T03:11:46.440' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a0edc3fc-9d15-c026-3a37-6601518b3112', N'Olivia', N'Berry', N'Partinex WorldWide ', N'Web', N'xqyac.dloqcplak@givooz.com', CAST(N'1966-01-25T09:14:15.100' AS DateTime), CAST(N'1984-07-15T13:36:18.300' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6d16e61b-c6cf-2253-3b0d-661560343551', N'Lee', N'Holden', N'Innipover  ', N'Service', N'vweacqep.aozzvx@jnxuit.com', CAST(N'1972-10-27T02:06:04.090' AS DateTime), CAST(N'1957-08-30T03:08:03.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8237e6ab-b7ac-b7d8-ea02-664db77d4cfd', N'Derick', N'Leon', NULL, N'Prepaid Customer', N'ysqhmy.ifhxewiukn@jpfns.ybjkgi.net', CAST(N'2000-02-05T11:33:25.850' AS DateTime), CAST(N'2001-11-03T10:53:32.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'730b2dcd-0506-6235-440b-665bfeef13de', N'Demetrius', N'Rivera', N'Insapax Holdings Inc', N'Service', N'shlsc.vquyposnq@hihpjhw.peztrt.org', CAST(N'2004-12-16T23:14:46.810' AS DateTime), CAST(N'1997-04-09T22:32:03.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2b536fc3-31f4-6fbd-1da1-66866d432cc9', N'Marisa', N'Ward', N'Emdimazz Direct Inc', N'Technical', N'grdjt.gwrzoqu@bfdtvipe.owlwaq.org', CAST(N'1958-04-18T01:10:00.400' AS DateTime), CAST(N'1995-04-22T17:58:33.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'deb523e6-ce3a-fec7-0dd1-66e889d5e40f', N'Kimberly', N'Brady', NULL, N'Service', N'gghgxncp855@urdsap.com', CAST(N'1971-06-29T22:50:19.800' AS DateTime), CAST(N'1971-12-09T02:09:14.420' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ad8abb81-dc5c-5add-2f0e-67472e9b97b7', N'Ramona', N'Woodard', N'Surquestommar Direct Corp.', N'Technical', N'qxfiy5@yn-xnd.org', CAST(N'1979-10-25T03:25:32.380' AS DateTime), CAST(N'2013-02-28T17:12:54.960' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1df139d7-b6ba-d32d-3d88-67784697bce9', N'Kerrie', N'Vance', NULL, N'Technical', N'tfzjszsy.qtvqmbdqd@rbgoxw.net', CAST(N'1977-06-11T04:24:45.180' AS DateTime), CAST(N'1995-07-21T16:07:55.080' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'228ddd5c-6d6d-b2f1-9d2d-67c983fbf436', N'Ramon', N'Aguilar', NULL, N'Technical', N'ytbjdqx@ohwspe.bkohxg.org', CAST(N'1993-10-14T10:19:29.430' AS DateTime), CAST(N'1956-05-19T11:08:14.170' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2ee75969-619d-c499-95d4-684923989945', N'Omar', N'Griffith', NULL, N'Accounting', N'otpgjctu22@eewx-x.com', CAST(N'1967-04-28T19:23:40.470' AS DateTime), CAST(N'1969-06-11T18:11:16.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'420d6a7e-7be8-bfc3-be5b-694c59d43cb7', N'Tammy', N'Mckenzie', NULL, N'Technical', N'qpdkca.ycnjb@arvpunf.on-hbo.com', CAST(N'2018-11-13T03:11:58.230' AS DateTime), CAST(N'2013-04-20T20:54:34.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5a3253c5-f63a-5cbb-6caf-696021928ee8', N'Kim', N'Nichols', N'Thruerupan  ', N'Technical', N'rpfy@xmxzlj.com', CAST(N'2008-01-20T06:52:08.550' AS DateTime), CAST(N'1974-07-08T14:10:59.540' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9f35a060-c7d0-d1b3-1208-69a512a72444', N'Randi', N'Joyce', N'Insapax WorldWide ', N'Prepaid Customer', N'fmexn.hfcfallmg@wyszzpc.rf-rsl.net', CAST(N'2006-12-15T20:03:46.030' AS DateTime), CAST(N'1990-11-02T15:48:15.300' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd2a8d1b1-ae9d-84f8-acb7-69cc67433f9f', N'Charlene', N'Singleton', NULL, N'Web', N'fhoo@nqlheshig.hrgjnd.org', CAST(N'1965-10-26T01:41:19.500' AS DateTime), CAST(N'1999-05-11T12:11:11.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'31febac3-7421-fd2b-878f-69e666cc7146', N'Zachary', N'Weber', NULL, N'Technical', N'mwphnw.sbnxvi@oosbeq.rgyzrn.com', CAST(N'1991-06-10T01:33:07.120' AS DateTime), CAST(N'1973-09-08T20:03:30.150' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'aa7b8bb5-4e9b-d617-c87c-6a4b07f010c5', N'Demetrius', N'Campbell', NULL, N'Customer', N'egum.qaenjlu@iydcyj.com', CAST(N'1998-05-05T04:56:39.580' AS DateTime), CAST(N'1971-08-11T03:09:45.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'49fba2f4-8c26-3c4f-e9f0-6a7e3ec256f7', N'Katherine', N'Vega', N'Truglibegentor  ', N'Accessory Customer', N'zqjikyag23@kughzj.ahukkx.com', CAST(N'1966-03-01T10:06:39.960' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cc9aeb04-0c61-4be0-88bd-6ab232daa1db', N'Tonia', N'Farrell', N'Surhupefax WorldWide ', N'Accounting', N'qytgjfw@nktnua.net', CAST(N'1953-05-29T13:05:31.410' AS DateTime), CAST(N'1990-07-13T08:57:12.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b0dfcb7c-96d7-a1d9-6d60-6ae0133e2e92', N'Alfonso', N'Lin', NULL, N'Sales', N'jpcyl.qdepjkjoj@pmmgas.net', CAST(N'1971-04-10T00:00:32.210' AS DateTime), CAST(N'2001-10-04T12:54:43.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5c45c0f7-23ae-e6b9-c604-6b3162af3cf8', N'Marcos', N'Robles', N'Klirobinor Direct Corp.', N'Technical', N'fmkd@vakpkh.zayg-q.net', CAST(N'1957-01-16T17:37:21.850' AS DateTime), CAST(N'2018-10-14T16:35:31.520' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'158e9183-ed1d-b5cf-c032-6b4e4eb650b1', N'Christine', N'Bell', NULL, N'Service', N'qakjdvej05@bdvdh-.net', CAST(N'1989-02-17T04:43:02.260' AS DateTime), CAST(N'1987-09-17T10:04:08.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b94db488-0525-ea84-99d8-6bd25ead5d68', N'Jana', N'Vega', N'Rehupazz  ', N'National Sales', N'ecfea.ohwni@hcrvyz.org', CAST(N'1988-10-21T10:09:53.890' AS DateTime), CAST(N'1968-10-20T10:45:56.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e18e6400-fac6-55f8-9d0b-6caa05ae72f0', N'Daniel', N'Yoder', NULL, N'Technical', N'ssxqbyt9@tmmhglz.szmuts.net', CAST(N'2016-03-27T23:45:03.800' AS DateTime), CAST(N'1960-07-14T10:17:35.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8f38160b-039f-2aec-c62d-6cbd7621476f', N'Brian', N'Aguirre', N'Varzapex Holdings ', N'Service', N'zghqs.ozpvsqa@jfkxsi.rrhdbt.org', CAST(N'1980-09-28T22:06:43.150' AS DateTime), CAST(N'2005-01-21T10:11:07.040' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7d595f81-538e-dae6-49ec-6ccfeff52111', N'Anitra', N'Terry', N'Zeenipadazz Holdings ', N'Prepaid Customer', N'sgotdn.juqqduqoj@rtmvio.jpcmgo.org', CAST(N'1974-03-12T13:05:37.790' AS DateTime), CAST(N'2014-01-11T02:40:39.690' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6ee94258-9b8f-5112-f8ae-6d407ced4750', N'Stephanie', N'Schroeder', NULL, N'Technical', N'nena@xwimbkvek.stbaef.com', CAST(N'2009-02-21T12:13:00.040' AS DateTime), CAST(N'2011-12-10T21:44:01.820' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'48313c9c-1240-2c3f-dc19-6d4d5fd6e0e4', N'Arturo', N'Carson', N'Inerplax  Corp.', N'National Marketing', N'krokn53@gauadb.net', CAST(N'2016-07-26T07:16:28.290' AS DateTime), CAST(N'1984-01-30T21:04:18.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0d28c13c-781a-116b-e07a-6dd5810901e1', N'Mindy', N'Odom', N'Grodimackantor International ', N'National Customer', N'ppaubq.yqtd@hnbznd.xf-wqq.net', CAST(N'1970-09-14T10:53:39.910' AS DateTime), CAST(N'1975-07-08T22:42:21.160' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ba09960f-c1bc-7409-8825-6dffc1d5036e', N'Matthew', N'Burke', NULL, N'Prepaid Customer', N'ixsvtsup.hcqx@mxvztd.net', CAST(N'1968-01-08T08:32:20.470' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0430503a-0290-27e8-05b9-6e0e0a130e2a', N'Vicki', N'Valenzuela', N'Zeecadefin Direct Corp.', N'Service', N'jysemw.mfppaqfl@gunobjk.movbmb.com', CAST(N'1971-12-29T15:28:02.480' AS DateTime), CAST(N'1997-07-24T15:43:36.420' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'66bf0ac6-fc14-5b23-46df-6e2b2873bcf9', N'Rebecca', N'Forbes', NULL, N'Prepaid Customer', N'jreckpx6@tgvfyo.org', CAST(N'2000-08-26T00:41:46.990' AS DateTime), CAST(N'1993-03-14T09:10:36.570' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'238bef93-b80a-598b-72f8-6eb0c10f256e', N'Ronald', N'Wolf', N'Trujubentor  Company', N'Technical', N'womsch3@lgizybdxa.lsmtwf.net', CAST(N'2006-02-12T07:57:37.820' AS DateTime), CAST(N'1969-06-23T21:10:51.040' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9deccb9c-b0d8-c12e-6d58-6ef7b442aefe', N'Jason', N'Griffith', NULL, N'Technical', N'mdxo6@uruqzh.org', CAST(N'1953-07-04T18:16:35.600' AS DateTime), CAST(N'1965-01-19T01:30:29.920' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'323aad12-7ab2-c45f-53c4-6f11a940a191', N'Erika', N'Kaufman', N'Lomtinegor WorldWide ', N'Web', N'hynlstz.ofdrnvac@lsnvnr.gqqhhd.net', CAST(N'1985-05-23T13:26:12.840' AS DateTime), CAST(N'1953-02-02T06:56:37.960' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2384913c-0421-394f-db46-6f1428d74293', N'Laura', N'Cantu', NULL, N'Technical', N'kdlscdhb.jjpo@axfc.szxcww.net', CAST(N'2004-02-28T17:07:31.400' AS DateTime), CAST(N'2013-10-16T19:22:16.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9efdcea9-dcb1-c6fe-2a65-70097f0757c0', N'Tamika', N'Estrada', N'Lomsipantor Direct ', N'Web', N'ldknlb.fmuf@iyaqew.org', CAST(N'2014-02-09T02:18:14.610' AS DateTime), CAST(N'2010-03-01T10:47:28.380' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b5746fe0-47b9-fff3-6b89-709c5efbcc51', N'Wendy', N'Huber', N'Klimunentor Holdings Corp.', N'Prepaid Customer', N'dkrelfrf700@rvuwp-.net', CAST(N'1973-10-31T00:05:15.210' AS DateTime), CAST(N'2012-02-23T03:58:56.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c1e952d2-5bef-e2c0-a740-70a3019e057d', N'Miranda', N'Mccall', NULL, N'Business Marketing', N'deyijkne.xrom@hvijkhzd.bwcbyg.net', CAST(N'1982-06-13T06:47:09.240' AS DateTime), CAST(N'2014-07-07T15:09:11.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4539ed4b-7a83-faea-81ca-70d8d36cedb6', N'Molly', N'Collins', N'Trurobegistor Direct ', N'Web', N'msgaf965@ni-yeh.com', CAST(N'1988-10-12T09:26:53.690' AS DateTime), CAST(N'1982-01-29T16:11:26.120' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2043cf4b-8d53-0cab-29f7-716eec0ea34e', N'Stephan', N'Hill', N'Surtumicator International ', N'Accounting', N'flogjc8@epfcwpgbz.ytfdwk.net', CAST(N'2011-01-05T07:33:40.380' AS DateTime), CAST(N'1985-12-28T14:27:31.220' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b00a9022-7859-9b78-029b-71f11641c1e3', N'Marianne', N'Robbins', NULL, N'Technical', N'hqaorea38@aiyuhuta.jlx-rl.net', CAST(N'1956-06-11T04:45:05.950' AS DateTime), CAST(N'1995-07-09T10:42:25.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c0c32a6d-c1dc-0a35-6aed-723f0bf2990f', N'Charity', N'Wheeler', NULL, N'Corporate Marketing', N'alql.iumxffui@yxmg.vhvojz.org', CAST(N'2010-05-01T20:59:15.040' AS DateTime), CAST(N'1967-09-27T14:47:10.960' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6924b18e-d8b8-1fa4-edca-730551dcec7c', N'Tonya', N'Landry', N'Hapmunepantor International ', N'Prepaid Customer', N'lwcbsc8@lsbbyc.net', CAST(N'1986-10-28T20:45:08.610' AS DateTime), CAST(N'1989-10-07T20:46:41.430' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'35e7e384-178d-dd26-6dc1-7311b9e58d8b', N'Kurt', N'Lara', NULL, N'Web', N'ojjjo@ribuwz.org', CAST(N'1954-04-10T03:24:10.690' AS DateTime), CAST(N'2004-06-07T10:07:18.790' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f597e891-dc80-d20a-7209-73230766085d', N'Bethany', N'Lozano', N'Groquestantor WorldWide Corp.', N'Web', N'qpbcd@qxosuw.xprgmp.net', CAST(N'1962-03-26T05:37:08.200' AS DateTime), CAST(N'1996-04-07T02:40:46.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'27e57bf3-7fd1-d1a1-2735-736b8f4b6747', N'Kimberley', N'Parsons', N'Hapquestefantor  Group', N'Accounting', N'naeg.kaktjwhyo@nomajn.net', CAST(N'2014-09-17T09:45:02.490' AS DateTime), CAST(N'1965-07-03T01:18:49.560' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c750c6ab-ed17-4290-4cf1-741a519f6ab0', N'Damon', N'Roach', N'Qwimunentor  ', N'Accounting', N'xebae99@ftlnio.com', CAST(N'1997-12-03T13:53:55.890' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'50e7f883-98ba-eb45-6b46-743440390f32', N'Beth', N'Dunlap', NULL, N'Service', N'lncqx455@nd-ius.org', CAST(N'2018-02-03T20:26:03.770' AS DateTime), CAST(N'1961-06-24T07:37:48.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'eff206df-aad1-3162-dd80-743b273278a1', N'Shanna', N'Hardy', N'Emvenewar WorldWide Company', N'Technical', N'wvfifvjl.emjjp@eyseuh.com', CAST(N'1982-08-26T15:49:49.540' AS DateTime), CAST(N'1979-03-03T13:04:43.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cd0b82b2-9fd1-bbd8-e42f-744758a0a5e6', N'Angelina', N'Santos', N'Dopbanin Direct ', N'Service', N'wcpuknss@purwxg.rsbwyz.com', CAST(N'1974-07-02T11:13:43.850' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9c535707-c353-2505-c20e-749d308218f4', N'Judy', N'Hanson', NULL, N'Service', N'yzsw.lhwdabi@eieszos.embxag.org', CAST(N'1961-04-10T05:17:00.530' AS DateTime), CAST(N'2003-11-03T07:04:26.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6f8c4c79-1044-b5bc-9c58-74ef4311fb1c', N'Katherine', N'O''Neal', NULL, N'Prepaid Customer', N'twfhrhga@j-gsb-.org', CAST(N'1977-04-22T03:47:16.280' AS DateTime), CAST(N'1988-07-12T03:44:39.150' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c89219ca-3cbd-729d-49f5-74fcd35f4813', N'Kellie', N'Harding', NULL, N'Web', N'sijpuons.tywlb@ksdyf-.net', CAST(N'1992-03-29T03:33:27.830' AS DateTime), CAST(N'1987-07-06T14:54:12.600' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4016fa9d-1d61-a8a3-a60b-751e1d21b0a9', N'Kevin', N'Olsen', N'Klidimor WorldWide ', N'Service', N'vfotrc.oaoms@yg-few.com', CAST(N'1986-08-30T18:49:16.560' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0818efb8-ac5e-9564-c7d2-752fa5e467cf', N'Gregory', N'Greene', NULL, N'Accounting', N'izhfo3@zfjwwpm.maxwfl.net', CAST(N'1996-01-27T10:27:29.510' AS DateTime), CAST(N'1969-12-27T16:14:25.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4ced2e10-3951-0302-7ef5-756dd5567095', N'Karl', N'Mullins', NULL, N'Web', N'aqyu.wambddxbrd@pfxdgdue.rrmdkn.org', CAST(N'1961-05-28T07:54:14.290' AS DateTime), CAST(N'1969-02-24T06:40:12.600' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd89d1123-7e29-55c1-55f0-75a0b2abf8f3', N'Melvin', N'Zuniga', N'Qwisapicator Holdings ', N'Accounting', N'ytlfzv21@npikom.com', CAST(N'1988-01-02T15:32:37.880' AS DateTime), CAST(N'1995-07-03T02:02:45.140' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5b640b1c-79aa-24b9-8368-75cdfddfc170', N'Elizabeth', N'Villegas', N'Cipquestar WorldWide ', N'Web', N'gbral.kznwo@fhyfch.net', CAST(N'1959-10-12T10:34:16.420' AS DateTime), CAST(N'1957-07-30T02:16:13.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3dad8a99-2dc1-f71d-7f3f-75fd27b6cc4a', N'Latoya', N'Wade', NULL, N'Prepaid Customer', N'rqhh.zzkkniuxl@ctccga.org', CAST(N'2011-05-08T06:29:50.730' AS DateTime), CAST(N'2013-04-19T20:45:49.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd06a0502-33c2-2403-0391-75fe1f1e48a1', N'Miranda', N'Barnes', NULL, N'Service', N'ategielf.dxzamhsq@zbpzwu.net', CAST(N'1984-09-12T18:51:43.750' AS DateTime), CAST(N'1995-07-15T06:51:38.080' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'96c060a3-3303-de7c-93d6-75feb33bea04', N'Nichole', N'Coffey', N'Monsipilower Holdings Corp.', N'Prepaid Customer', N'uwpfom4@rfqmki.com', CAST(N'1958-04-14T02:23:57.220' AS DateTime), CAST(N'1977-12-03T06:22:37.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd36b7c76-451a-fd0c-f9ab-761d17a5e66a', N'Chandra', N'Spence', N'Rapzapin Direct Company', N'Corporate Customer', N'wzevz180@fkuibn.org', CAST(N'2007-02-19T18:10:44.410' AS DateTime), CAST(N'1958-06-28T17:37:03.150' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8919b2e9-6b4a-d261-668f-762d6c05295a', N'Duane', N'Hubbard', NULL, N'Prepaid Customer', N'hvyfva.tphrkqgxc@lqwy.zndtaj.net', CAST(N'2003-12-26T23:26:15.460' AS DateTime), CAST(N'1960-08-22T16:33:18.570' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c0910dea-e3b5-7b8c-a752-765be625c0e3', N'Dante', N'Rivas', N'Ciptumower  ', N'Technical', N'ltsuitjj@qdriznx.s-cavb.com', CAST(N'1975-02-17T04:41:15.570' AS DateTime), CAST(N'1972-12-19T00:36:56.240' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ee62e88e-3c15-32eb-f8cc-768bbd368705', N'Caroline', N'Hinton', NULL, N'Web', N'qnqzkmy559@cnnyka.com', CAST(N'1954-03-13T08:37:14.370' AS DateTime), CAST(N'2004-12-11T16:36:48.500' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3fcde392-008c-69e5-e9e3-76a0818da102', N'Antoine', N'Howard', NULL, N'Accounting', N'wmrzbbwd@jiwp.mihzgg.net', CAST(N'1953-08-10T09:18:57.730' AS DateTime), CAST(N'1969-08-27T18:07:27.520' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'37a5d6cc-6153-9e5d-2075-76deb55cad74', N'Grace', N'Dunn', N'Fropickackistor WorldWide ', N'Technical', N'wergrxy.bbxyko@gynnur.org', CAST(N'1989-07-15T11:59:14.910' AS DateTime), CAST(N'1963-12-30T20:50:38.480' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f18f017d-04da-3bf3-7cc4-76f4f5d5eede', N'Demond', N'Sanchez', N'Frotanegar International ', N'Accounting', N'uvhgb.dgmlcompo@mupxfb.uswlci.com', CAST(N'2004-10-24T19:15:16.280' AS DateTime), CAST(N'1977-03-23T02:01:18.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2fa317f5-69e0-e9c2-8908-77345500106c', N'Evan', N'Sparks', N'Varvenupentor International ', N'Technical', N'peyth9@sfquos.zej-dg.org', CAST(N'1983-03-20T14:22:50.210' AS DateTime), CAST(N'1982-12-06T08:54:33.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fb5f9570-f23b-2ad3-e09f-7818d46453c2', N'April', N'Holloway', N'Lomdimommazz Direct ', N'Service', N'vinpx.wzhwyxk@uspdxo.net', CAST(N'2006-03-13T03:17:58.060' AS DateTime), CAST(N'1976-06-15T20:19:58.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ba00bc4e-e1c4-dbc7-952b-78286b867814', N'Allyson', N'Mckenzie', N'Endsapanex WorldWide Group', N'Sales', N'mcnx59@zatipl.org', CAST(N'1991-06-23T12:45:55.490' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e9708cb9-c244-0522-dd98-783b41935735', N'Tommy', N'Mcclain', NULL, N'Accounting', N'kdhf@pwxais.net', CAST(N'1980-02-07T20:55:18.460' AS DateTime), CAST(N'1998-08-28T18:07:21.170' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'78b6df6b-daf4-ed9b-1ef2-789423274505', N'Brendan', N'Cisneros', NULL, N'Web', N'yyoqwr.pckudqdarz@rxrnqv.net', CAST(N'1999-12-08T07:57:24.800' AS DateTime), CAST(N'1953-11-25T18:24:51.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8b224545-a456-60d4-5a08-79367fd275f5', N'Orlando', N'Robertson', N'Zeebanollin WorldWide Group', N'Web', N'ojnlzn070@yzckazu.togevl.com', CAST(N'1981-07-22T07:33:32.060' AS DateTime), CAST(N'1995-11-19T19:19:31.920' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7127324a-e6f4-519a-f263-793baf70b8f1', N'Kerrie', N'Osborn', N'Adpeban  ', N'Prepaid Customer', N'dmwsgzs.ptmrzinzm@bxqowv.com', CAST(N'1965-07-14T07:41:58.220' AS DateTime), CAST(N'2017-03-06T02:10:15.990' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5108228b-98b1-0bb1-ffa5-7943f6c1e08f', N'Tammi', N'Bird', N'Tippickegan  Company', N'Prepaid Customer', N'shodjhgd26@ewjumffb.xxdzdg.com', CAST(N'1999-03-04T10:46:37.330' AS DateTime), CAST(N'2011-07-01T06:23:32.510' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fe01118a-9745-39bb-98b9-795b7e64901f', N'Reginald', N'Rush', N'Trusapackex International ', N'Accounting', N'sjzltxci.kjtobpgrdh@jice-g.net', CAST(N'1993-12-03T06:48:04.080' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f9e03485-7f12-ca44-6e47-7960c8dbc173', N'Karin', N'Gibbs', NULL, N'International Sales', N'xdfy.ushuby@pqopvt.com', CAST(N'1971-09-05T16:45:22.140' AS DateTime), CAST(N'2004-08-13T04:54:27.560' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'90063b2e-aa52-3987-4f91-79d2d0153842', N'Karrie', N'Benjamin', N'Enddimicator  ', N'Technical', N'vwaoyga48@dwzhorm.rgxcdc.com', CAST(N'1990-06-12T20:10:49.310' AS DateTime), CAST(N'1972-11-16T20:22:52.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd838f4e1-63dd-7fcd-f1ca-79d86c7b894a', N'Kari', N'Farmer', NULL, N'Technical', N'gmreb.sqdzfflr@sljbwr.com', CAST(N'1973-12-28T19:01:36.560' AS DateTime), CAST(N'1955-03-01T09:48:21.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'054e8c96-4c91-a5e0-e447-79e1d94a500e', N'Wade', N'Pearson', NULL, N'Web', N'odkull.vqrxk@nnijmw.org', CAST(N'1992-04-12T20:12:26.460' AS DateTime), CAST(N'2008-02-17T08:29:09.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2d4bcb31-4795-2aea-0b28-7a8722ab6410', N'Kenny', N'Daniel', N'Tipnipadex WorldWide Company', N'Accounting', N'gppgtc30@tbiq.yakwhu.org', CAST(N'1997-12-19T23:46:18.270' AS DateTime), CAST(N'1990-11-05T09:09:57.810' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e65c9fc6-6666-6b89-c81a-7ab4426a2efd', N'Kristen', N'Chung', NULL, N'Service', N'aqjf8@jyydag.net', CAST(N'1957-02-17T23:32:31.600' AS DateTime), CAST(N'1964-01-19T07:16:55.380' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'80cf12d8-2d38-e03d-c6d5-7acfe537063e', N'Stacey', N'Mckay', NULL, N'Technical', N'qhamcfyc.drnbaaxms@tfbhhwzvp.ycdrra.net', CAST(N'1966-11-07T10:18:44.170' AS DateTime), CAST(N'1965-07-29T16:56:19.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9d2a886b-5cdc-d3d2-44b3-7ad9c726804d', N'Abraham', N'Cline', N'Winfropax  Corp.', N'Marketing', N'lysttzqp14@pnomew.com', CAST(N'1962-03-04T20:19:25.700' AS DateTime), CAST(N'2003-04-15T19:50:37.230' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'987c1797-cb11-cebf-ead0-7b0f882cec04', N'Dianna', N'Suarez', N'Cipnipistor Holdings Company', N'Accessory Sales', N'fkmtvrly.eguk@eoeyo.asdoad.com', CAST(N'1956-10-12T05:03:54.020' AS DateTime), CAST(N'2008-08-08T07:39:22.220' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8413d734-24e4-53aa-38ff-7ba6b8cf19ba', N'Myra', N'Zuniga', N'Enddudadentor Holdings ', N'Marketing', N'btttfw@oroath.org', CAST(N'1955-10-20T15:06:10.980' AS DateTime), CAST(N'1985-05-16T00:51:49.460' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'dc4989a9-ab0f-bd69-72c4-7c52064b888c', N'Alisa', N'Haley', NULL, N'Service', N'zzfr34@datbcq.net', CAST(N'1972-05-25T10:12:22.990' AS DateTime), CAST(N'1994-08-22T11:50:39.880' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd69b8116-148b-1d8b-4887-7c5efb714995', N'Gail', N'Ritter', N'Barrobover  ', N'Web', N'cnls@cwupoviga.fmqdlr.org', CAST(N'1992-05-17T23:54:30.550' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ea1d492d-9e62-67e3-cc3b-7c9443974117', N'Omar', N'Conway', N'Cipwerpicax Direct Group', N'Technical', N'pgmwnuyr3@pqdedj.vjocly.com', CAST(N'1973-06-01T22:24:44.910' AS DateTime), CAST(N'1983-08-21T16:16:59.840' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b6970fcb-d0b4-088f-2a49-7ce4bf8440c7', N'Autumn', N'Meyers', NULL, N'Accounting', N'ougw.nznpowqz@xlns.rbuf-u.net', CAST(N'2004-05-15T16:56:14.850' AS DateTime), CAST(N'1984-12-27T13:03:57.540' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7ba99c42-ff5d-5a26-77fa-7cec462cd3d1', N'Albert', N'Wiggins', N'Truwerefentor WorldWide Company', N'Technical', N'ioiovra347@uvwz.oisukn.org', CAST(N'1967-03-13T17:33:46.880' AS DateTime), CAST(N'2016-07-22T20:38:31.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fd311dc0-e2c2-f82c-be36-7cfaa08ec84d', N'Guy', N'Gordon', NULL, N'National Marketing', N'wezeobkk80@ceir.dtuelo.net', CAST(N'2016-02-13T01:46:40.510' AS DateTime), CAST(N'1953-10-26T02:15:15.260' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ca04c785-c3cc-f958-32e3-7da3515e3efe', N'Karla', N'Hale', N'Tipdudedax International ', N'International Customer', N'ebvoemqx.dtyhmbr@utx-ha.net', CAST(N'1990-04-26T04:13:17.130' AS DateTime), CAST(N'2011-01-26T07:40:24.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a3675614-52d8-b158-c564-7da7e5f3bd8b', N'Carlos', N'Zimmerman', NULL, N'Technical', N'iloc1@cnptqx.net', CAST(N'1967-11-10T19:04:19.070' AS DateTime), CAST(N'1958-12-10T15:11:12.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a44b5ae9-ab44-603d-cc6b-7dd7a4ea1e7f', N'Harvey', N'Cooley', NULL, N'International Customer', N'cptaxut.wphemqtrw@hbowxt.qpxnsr.com', CAST(N'1959-10-18T13:22:20.290' AS DateTime), CAST(N'1993-08-01T22:40:57.420' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5a4d3d1d-bac3-cb3a-3976-7e29d71f31bd', N'Myron', N'Rice', NULL, N'National Marketing', N'hikiuwm090@wcurztan.nofwpv.com', CAST(N'1954-08-10T23:29:50.420' AS DateTime), CAST(N'1966-02-15T16:20:33.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7c8d5ca6-cf76-1006-b587-7e30775eb3c8', N'Carmen', N'Zimmerman', NULL, N'Web', N'onaygn.ktsvb@jzkqzujtb.irnvoe.com', CAST(N'2014-01-14T05:03:40.500' AS DateTime), CAST(N'2004-06-03T00:22:55.820' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'761303bd-4f3a-3490-4677-7e83c3e25968', N'Paige', N'Lindsey', N'Emwericator International ', N'Accounting', N'qimuhhc.wqeqgwcyqq@t-hvde.net', CAST(N'1965-03-11T01:29:35.770' AS DateTime), CAST(N'1953-07-19T17:48:41.850' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cace4126-0c54-5e89-ea9d-7ef55979d312', N'Trent', N'Bowman', N'Resapar  Company', N'Technical', N'rkarl08@qjhjqlwt.oopzyt.net', CAST(N'2010-05-21T23:11:15.130' AS DateTime), CAST(N'2011-06-04T09:50:01.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4db67abd-ce6d-5c9d-5d25-7f035fb45996', N'Whitney', N'Hendricks', NULL, N'Service', N'bwyq61@ppjswj.-tartu.org', CAST(N'2003-10-20T18:09:30.270' AS DateTime), CAST(N'1978-03-15T09:18:19.020' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'70a56c5e-9476-c742-debf-7f1b120f792b', N'Jorge', N'Lam', NULL, N'Business Customer', N'bxofc.njuxvwilk@-yknmk.org', CAST(N'1956-05-18T05:51:36.110' AS DateTime), CAST(N'1972-04-25T11:43:01.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e8999356-f0f6-96e3-5466-7f345b2a747e', N'Stanley', N'Hickman', N'Supnipan  Company', N'Prepaid Customer', N'gywl.kdkbjxu@qroxc.hztgnw.com', CAST(N'1974-11-10T02:20:35.700' AS DateTime), CAST(N'1989-04-18T07:30:15.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0d14bb37-2feb-daf1-daa9-7f7b30f59506', N'Sandy', N'Lin', N'Partinupazz WorldWide ', N'Technical', N'adnaf989@zuk-pv.net', CAST(N'1965-09-01T08:58:55.160' AS DateTime), CAST(N'1965-05-08T13:08:33.800' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a2630276-4fea-c275-cd44-7f843e669616', N'Laura', N'Atkinson', N'Cipdudistor  Company', N'Technical', N'spnw.udwtoywk@xnapvt.net', CAST(N'1990-10-30T08:31:24.150' AS DateTime), CAST(N'1965-11-24T18:45:30.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'961effff-888f-3115-1f31-7f94424f3dc8', N'Theresa', N'Ross', N'Lompebower  ', N'Corporate Customer', N'bdrhdo.etjpajppt@mxvb.uhvqac.net', CAST(N'1954-08-27T06:01:26.670' AS DateTime), CAST(N'2011-12-26T16:55:03.810' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'abc276a7-581e-f55e-f1c2-7fc9055b09b5', N'Rose', N'Raymond', NULL, N'Prepaid Customer', N'tfkassvx6@tdwwyh.jrotwv.net', CAST(N'2004-10-12T02:38:00.020' AS DateTime), CAST(N'2004-02-09T11:16:43.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8c4569b0-ef7f-4705-820b-8032a5ca2dbf', N'Marc', N'Ewing', N'Trupickan International Corp.', N'Service', N'zziycvc@njsr.pkjiuk.com', CAST(N'1985-09-07T05:06:35.450' AS DateTime), CAST(N'1957-12-23T08:09:03.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f0c09b5d-56b1-412c-8232-803e29e38b17', N'Peggy', N'Higgins', N'Inzapopax  ', N'Prepaid Customer', N'smcelqv967@ehjtaq.net', CAST(N'1966-10-11T02:58:34.520' AS DateTime), CAST(N'1995-07-03T17:31:07.710' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'62f21f55-b3bf-217a-8697-8070f966923c', N'Stacy', N'Key', N'Vartinex WorldWide ', N'National Sales', N'iddylk.opggmjlgd@fxykjnx.jyyxnk.com', CAST(N'1975-03-08T20:04:11.570' AS DateTime), CAST(N'1980-01-08T15:40:10.530' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7b409001-4b9f-6ae7-e34d-81066fb0fba0', N'Jonathon', N'Taylor', NULL, N'Technical', N'kdfibidr@my-ido.com', CAST(N'1978-01-14T20:59:00.050' AS DateTime), CAST(N'1960-10-27T14:39:01.300' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'85e9a700-7843-d2da-3040-816b7e6b409e', N'Lynette', N'Moody', N'Lomsapentor Direct ', N'Consumer Customer', N'fcuvqtnj.aamxcgwpv@zayvdky.czuzxr.net', CAST(N'1996-11-09T07:33:53.400' AS DateTime), CAST(N'1961-09-12T22:30:00.800' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3efbc46a-196f-2e2b-0211-81729649d9ea', N'Sandy', N'Ali', NULL, N'Web', N'dtygvc19@gdabdrrw.eay-nl.net', CAST(N'1998-10-04T13:41:59.920' AS DateTime), CAST(N'2000-06-04T23:42:47.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'15e0898e-acc5-7cc1-e4f5-81f3f6e90e60', N'Kathryn', N'Hanson', NULL, N'Technical', N'iabufrc.pszjw@piiirh.com', CAST(N'1975-09-30T09:53:35.900' AS DateTime), CAST(N'2003-11-21T17:53:15.500' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b3569359-a438-02c0-a745-8268e83a67f9', N'Max', N'Bentley', NULL, N'Web', N'vufeik.popxac@qovpk.mtpxfg.org', CAST(N'1975-12-23T12:17:47.990' AS DateTime), CAST(N'2011-04-22T07:27:46.140' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3f22acd3-6bbf-6bee-6f05-831f87e1410d', N'Joyce', N'Mc Daniel', N'Winjubupistor International ', N'Service', N'eazxuxyf.gbgapbq@vogkjt.suylql.org', CAST(N'2005-07-15T07:06:42.980' AS DateTime), CAST(N'1955-10-10T17:37:16.100' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'dec13aa2-8799-7556-e16f-832860162549', N'Holly', N'Barnes', NULL, N'Technical', NULL, CAST(N'1957-10-26T01:39:36.770' AS DateTime), CAST(N'1985-07-22T01:51:10.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'78780482-51e3-7979-2a01-8356b1874f8c', N'Aaron', N'Calderon', NULL, N'Accounting', N'nrwzwwi.psaum@fsrqnl.com', CAST(N'2009-09-06T13:12:53.940' AS DateTime), CAST(N'2018-06-13T15:45:34.600' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4a7eaa44-ee26-6b64-c1fd-83956d431a06', N'Terrance', N'Berger', NULL, N'Accounting', N'vllkfbz.flqamebmlw@jijvdj.com', CAST(N'2000-08-28T14:05:02.720' AS DateTime), CAST(N'1963-02-09T22:26:42.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8f5305e3-e2da-dab2-462b-83b222cb54da', N'Stephan', N'Morse', N'Thruglibar Holdings Corp.', N'Prepaid Customer', N'izlqqcts@iuvqny.com', CAST(N'1966-07-12T01:00:40.380' AS DateTime), CAST(N'1989-10-28T01:45:39.750' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'09fe4eae-193c-0012-7549-840229736197', N'Ralph', N'Copeland', N'Adpickazz Direct Group', N'Web', N'qhdq.nyhl@anvp.ofcg-s.org', CAST(N'1958-07-28T10:42:52.810' AS DateTime), CAST(N'1994-04-11T11:06:17.770' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd6e33c2e-4de0-85bc-b579-84156dc8b087', N'Dominick', N'Rollins', NULL, N'Service', N'zsnldcib63@bfqcbr.com', CAST(N'1999-11-22T19:38:15.750' AS DateTime), CAST(N'2003-12-07T17:19:25.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b0d51a5a-82b2-72cd-d1cd-84297bdc9ce0', N'Darla', N'Haney', NULL, N'Service', N'oqjtjy307@vglmfick.--iirc.com', CAST(N'1986-08-29T23:56:13.160' AS DateTime), CAST(N'1976-06-06T05:05:59.380' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'db73590e-81b8-c89d-5626-846d5aaf1ea8', N'Marianne', N'Benson', NULL, N'Service', N'upykpex@jodft-.org', CAST(N'2009-09-22T06:58:38.200' AS DateTime), CAST(N'1959-07-10T01:32:06.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'15947897-df60-2f6c-9240-84707ce32190', N'Neal', N'Keller', N'Emcadplazz Direct ', N'Marketing', N'vbnghxhe@eueo-i.org', CAST(N'2007-05-04T22:09:02.040' AS DateTime), CAST(N'2017-09-11T19:16:24.530' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a94a318e-ec51-a120-a37a-84a4f636224a', N'Lesley', N'Clarke', NULL, N'Customer', N'spjzfbem659@wqmjly.gkkoyb.org', CAST(N'2004-04-20T01:14:32.240' AS DateTime), CAST(N'2010-01-31T04:16:02.200' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'75841d4d-07bf-3cf7-c42f-84a55018e69d', N'Luke', N'Ellison', NULL, N'Accounting', N'tkgujjv7@alkdqg.mloagi.net', CAST(N'1959-08-03T17:43:33.430' AS DateTime), CAST(N'1975-12-24T16:03:18.210' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'aa258bb0-c49d-fc36-ad1d-84ee00b02b99', N'Kelley', N'Flores', N'Tiphupinor Direct ', N'Service', N'qsreltes.tcsrpahbz@alvvyi.com', CAST(N'2009-01-17T11:06:18.830' AS DateTime), CAST(N'1953-04-23T07:51:16.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3f9553a2-9832-4a41-61ca-84f41ca0e766', N'Alma', N'Livingston', NULL, N'Accounting', N'xfup.uotovecet@becbqxf.bjezck.com', CAST(N'1993-10-08T12:34:39.380' AS DateTime), CAST(N'1971-07-30T18:07:19.820' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a7a51730-c380-57b2-91a5-84f8138327d0', N'Jeannie', N'Daniel', N'Mondudefar Direct ', N'Technical', N'ujal.mlyznvu@lryhqt.net', CAST(N'2011-03-17T12:12:33.390' AS DateTime), CAST(N'1994-09-06T14:38:19.990' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2c082b65-9f94-f421-2e21-8558491980cd', N'Mitchell', N'Lamb', N'Trujubor  Group', N'Accounting', N'xkpf.tfoodge@zrws.znobwg.net', CAST(N'1994-04-22T08:41:09.810' AS DateTime), CAST(N'1962-06-15T10:52:26.960' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'353beec2-32f0-c002-2f06-85bf2856b848', N'Howard', N'Black', N'Windudentor  Group', N'Service', N'phspx816@tfkuio.net', CAST(N'1991-11-25T11:43:47.570' AS DateTime), CAST(N'2005-12-17T22:26:28.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c532dbd6-9fad-4038-8e88-85e7b95dd297', N'Billie', N'Cain', N'Hapsapentor  Company', N'Prepaid Customer', N'mreilqo.abprj@wkhuae.com', CAST(N'1995-07-08T18:53:17.680' AS DateTime), CAST(N'1963-02-19T14:50:15.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'86cd2bc4-a850-3765-3e0c-85fd5b8e3d71', N'Wesley', N'Shannon', NULL, N'Accounting', N'piow.uzehqev@vqxbnxksu.fqkpgk.net', CAST(N'1982-07-08T14:23:52.420' AS DateTime), CAST(N'1957-04-20T04:45:52.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bace73a9-f98b-cd75-8615-86473fe30c29', N'Miriam', N'Townsend', N'Grosipopan  ', N'Web', N'rejcx@ndfjvr.dyhokz.org', CAST(N'1990-06-15T23:58:22.270' AS DateTime), CAST(N'1965-03-05T20:28:48.990' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'39e4a4b7-bbb5-de93-e172-86593cf111fc', N'Devin', N'Hurst', NULL, N'Prepaid Customer', N'fjnkmqj@gretuuydu.oafuxn.com', CAST(N'2002-12-19T20:18:32.580' AS DateTime), CAST(N'1968-04-01T01:53:50.870' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e3e7ad1c-9b66-bb25-883b-866f2bb86877', N'Robin', N'Davis', N'Winmunin International ', N'Accounting', N'vxdgkbkp.siryfdz@tvetcf.com', CAST(N'1955-12-05T03:59:09.220' AS DateTime), CAST(N'2005-03-03T22:20:10.170' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'de0a1662-190f-dce3-5919-86b9361fb8f1', N'Reginald', N'Mckee', NULL, N'Prepaid Customer', N'qjwnimq49@dbonvm.org', CAST(N'2012-01-22T22:22:50.300' AS DateTime), CAST(N'2012-03-21T00:36:11.430' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e85618f7-f4ed-3bbe-9d56-86cd5f3a7fc9', N'Scotty', N'Escobar', NULL, N'International Customer', N'exmlx1@gtowgq.com', CAST(N'1955-08-28T09:42:36.420' AS DateTime), CAST(N'1970-10-22T22:53:00.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e5ff4d62-0cf1-6b5b-e19c-872f4a548827', N'Roberto', N'Olsen', NULL, N'Accounting', N'klfc.lcmftrussw@i-uxvb.org', CAST(N'2011-02-06T13:28:42.370' AS DateTime), CAST(N'1964-10-22T19:08:01.600' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8cfceb7c-f311-c333-1efd-873672fe655d', N'Trent', N'Osborn', N'Frofropplover WorldWide Group', N'Service', N'isga6@uqlm.pqwibn.org', CAST(N'1966-02-20T13:09:18.440' AS DateTime), CAST(N'1992-04-17T20:38:19.520' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a06a5641-cbcc-d08e-1263-873c2026fa43', N'Shaun', N'Rosales', NULL, N'Prepaid Customer', N'iuyj.kzict@cosm.bhyfvj.com', CAST(N'1992-05-15T03:22:26.470' AS DateTime), CAST(N'1981-08-20T22:20:21.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5edf2c53-c101-2a8d-4bb8-878d48bfa003', N'Carey', N'Nunez', N'Zeedimicistor Holdings Group', N'Service', N'aprl.ulbngjsm@ckjnui.fodrhv.com', CAST(N'1965-12-20T19:30:44.020' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7e624063-26dd-5177-e259-87cf832e3942', N'James', N'Patrick', N'Supkilan Holdings Corp.', N'Marketing', N'ejkv996@nsicrhv.scddmv.org', CAST(N'1965-02-25T05:10:30.410' AS DateTime), CAST(N'1966-07-31T19:05:23.310' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7233b3d3-dc6f-a711-83ec-884f09ec019c', N'Janice', N'English', NULL, N'Service', N'hkoban.eetuezzk@dldd.nxfpo-.net', CAST(N'1990-12-13T02:41:37.470' AS DateTime), CAST(N'1991-05-24T21:48:28.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2d839837-3711-89c4-fade-885c9eb17a9e', N'Trent', N'Reese', N'Cipweran WorldWide ', N'Prepaid Customer', N'digmrjjv.wxrnwvl@daupbn.com', CAST(N'1998-08-06T06:55:33.420' AS DateTime), CAST(N'1979-12-09T00:44:14.070' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'80fa4d75-8b13-d4ac-71b7-886a1bfddbb2', N'Sophia', N'Kim', N'Inmunedentor Direct Corp.', N'Technical Customer', N'fcxujjzq461@fomfqg.org', CAST(N'1978-05-18T14:33:20.600' AS DateTime), CAST(N'1977-10-19T23:59:38.590' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'348e692e-6c10-7ede-b229-88b0b1b4102b', N'Penny', N'Wu', NULL, N'Web', N'mrxgd.ozlitgzfjz@gjduiyk.eyylpf.com', CAST(N'1990-12-11T23:01:45.710' AS DateTime), CAST(N'1958-01-27T09:20:11.630' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c7c89e76-f611-c9f3-679d-88cc94551ef4', N'Hannah', N'Cochran', N'Adnipplicator  ', N'Technical', N'bikvjk.nbibfltmfd@i-mokk.net', CAST(N'2005-05-20T12:43:21.840' AS DateTime), CAST(N'1962-08-03T02:28:25.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f02618fa-3c82-4427-e746-890618b8624b', N'Preston', N'Vazquez', N'Cippebupantor  Corp.', N'Technical', N'wtsyt.wchzxowaa@kvygmj.-x-ow-.org', CAST(N'1965-11-03T09:30:25.830' AS DateTime), CAST(N'2018-11-04T11:05:19.880' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'160ddc98-f83c-db41-e501-89c9a5ce1c32', N'Dena', N'Guerrero', NULL, N'Technical', N'qdmtzfoi415@voewrx.org', CAST(N'2011-06-29T08:29:26.870' AS DateTime), CAST(N'1960-03-31T00:05:34.810' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1f55ef41-5151-ec41-f403-8a3e65b01044', N'Jose', N'Gallagher', N'Inglibonantor International ', N'Prepaid Customer', N'fuasrbin.huml@fumyed.com', CAST(N'2006-12-22T08:29:04.090' AS DateTime), CAST(N'1960-10-10T04:01:22.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'55036e2e-a022-67b9-307a-8a53ab77d5cf', N'Teddy', N'Ingram', NULL, N'Marketing', N'hikck622@keie-g.com', CAST(N'2015-12-17T09:58:05.820' AS DateTime), CAST(N'1983-01-27T12:25:44.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fc75e7f2-e1aa-8116-ab49-8a783aa4fd89', N'Sherry', N'Middleton', N'Ciphuponor  Company', N'National Sales', N'wcelllk630@pbnzne.org', CAST(N'1977-10-30T13:40:10.300' AS DateTime), CAST(N'1975-02-16T17:31:21.390' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a2dfb51f-0c48-8360-1079-8abc708d98f2', N'Melanie', N'Little', NULL, N'Technical', N'xalxigzc@lfwayp.com', CAST(N'1966-04-28T22:31:10.940' AS DateTime), CAST(N'1963-07-28T02:19:02.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'45821833-19bd-5d2d-fce1-8b1f11e54225', N'Rene', N'Simmons', NULL, N'Web', N'jkanvqqu.ocdxz@hqcso.uvqjhf.com', CAST(N'1975-03-07T15:05:21.280' AS DateTime), CAST(N'2005-11-04T14:14:05.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6631dc47-194d-f479-6fe4-8b39b3139efe', N'Tamika', N'Johns', NULL, N'Sales', N'jnzyw27@lmyiir.org', CAST(N'1957-07-14T05:16:37.670' AS DateTime), CAST(N'1997-01-07T17:52:35.570' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fa451e7f-8c5a-80a8-ccb3-8b75c74513b2', N'Naomi', N'Ballard', N'Endsipinax  ', N'Technical Marketing', N'tlmzsje@rvhiyc.com', CAST(N'1956-05-07T21:42:57.700' AS DateTime), CAST(N'1955-11-05T16:59:55.060' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7968c1fa-202e-6636-af2a-8baa0cd41a63', N'Bridget', N'Wilson', N'Lomkilefor  ', N'Web', N'adug71@nbsepgezw.oymwis.net', CAST(N'1954-12-30T10:46:14.640' AS DateTime), CAST(N'1998-02-05T04:02:29.970' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'dc4abeda-2e99-6323-fb6b-8bb70b80adcb', N'Felicia', N'Mccann', N'Uptanommar Direct ', N'Service', N'fpkucryc.gaiguwfug@hbmiuluc.hwghoq.com', CAST(N'2018-07-08T03:34:18.940' AS DateTime), CAST(N'1965-03-06T09:59:50.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'53d9b868-4ab7-9f05-2a72-8bea639d82a6', N'Kristina', N'Conrad', NULL, N'Accounting', N'zfdimggh94@pmel.sbqmv-.net', CAST(N'1963-01-09T05:23:33.840' AS DateTime), CAST(N'1982-04-04T15:59:45.000' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bf34923b-3175-2e40-bf1d-8c7d0ee96bbd', N'Karin', N'Vega', NULL, N'Technical', N'buodbur.suzsa@sfning.org', CAST(N'1974-06-07T05:22:53.960' AS DateTime), CAST(N'1960-10-04T15:33:44.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c92ffd8e-1f52-536e-ff6a-8ca63643e602', N'Leon', N'Beck', NULL, N'Accounting', N'jmuq@hwooa-.com', CAST(N'1989-04-26T17:51:03.780' AS DateTime), CAST(N'2015-09-16T17:13:54.530' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'63229630-a107-936a-bcbb-8d0b66ce2062', N'Phillip', N'Padilla', NULL, N'Sales', N'wtxmgya.dytzimf@sryrky.org', CAST(N'1984-04-05T15:06:22.070' AS DateTime), CAST(N'2001-09-13T18:36:53.990' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a791ae4b-a558-dbb7-a997-8d41a538aa9c', N'Cody', N'Fields', N'Growerpedgower Direct Group', NULL, N'aanrt@gw-mvw.org', CAST(N'1964-01-22T08:31:23.180' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ce370bd9-a233-f86e-11f6-8dc94bd142cc', N'Ronnie', N'Novak', N'Qwibanazz Direct ', N'Accounting', N'rhijbv.hfikyvne@fgcjxrb.c-apbx.net', CAST(N'1974-11-22T05:35:13.620' AS DateTime), CAST(N'1967-07-17T21:46:08.680' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bc9c0369-9187-7d86-2afc-8dd6f46e6de3', N'Marlene', N'Morton', NULL, N'Service', N'nevxnv@lyxzoawit.bflhxn.org', CAST(N'1984-01-13T03:06:54.370' AS DateTime), CAST(N'2005-01-09T06:25:01.850' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e14b4823-ed0e-1a56-8a95-8eb26573bb5d', N'Kara', N'Tate', N'Adtinax International ', N'Technical', N'pwky@pr-kia.net', CAST(N'1966-09-30T14:32:20.100' AS DateTime), CAST(N'1975-07-10T06:05:22.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'99136cdb-1f7b-c0ba-6e1b-8edf1d9db168', N'Greg', N'Church', NULL, N'Accounting', N'gvjmjq85@wgolu.lxuwoo.org', CAST(N'2003-04-17T13:48:24.110' AS DateTime), CAST(N'1978-12-10T00:13:28.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'26bed32d-9b63-d717-b6b5-8f2edb948db0', N'Robyn', N'Schmitt', NULL, N'Prepaid Customer', N'uqat.kiuagkw@rcot-x.org', CAST(N'1987-01-06T07:31:44.470' AS DateTime), CAST(N'2011-06-13T10:39:59.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8af05496-954a-18c8-d3db-8f3ff7abc3cf', N'Dallas', N'Gardner', NULL, N'Technical', N'sfcil.ytudajftue@uvkkkc.xzhzua.org', CAST(N'1954-05-24T20:51:44.930' AS DateTime), CAST(N'1999-05-10T18:27:44.950' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8b588ba8-f66f-5dae-52a8-8f47d873c7c2', N'Suzanne', N'Jacobson', N'Klitinackover Direct ', N'Prepaid Customer', N'ocgqf.tdqiskpcz@mdwrx.-bckhw.org', CAST(N'2014-09-10T06:00:13.320' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5df069d7-1d5b-8dab-5d50-8f9c0bf4b4ef', N'Samuel', N'Riggs', N'Qwimunin Holdings ', N'Service', N'wijsnqsm.mbxms@ifhqbqfq.-itcev.org', CAST(N'1982-09-07T15:55:43.850' AS DateTime), CAST(N'1955-12-26T04:23:22.820' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3471dadd-ed91-fa5f-3202-8fa609fea299', N'Levi', N'Fuller', NULL, N'Accounting', N'rjun40@pthudh.com', CAST(N'1981-04-10T02:41:40.390' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd5dce10e-513c-449d-8e34-8fe771fa464a', N'Stefanie', N'Buckley', NULL, N'Accounting', N'pzvx.lsmsdt@tjsvqp.org', CAST(N'1978-04-17T12:42:21.220' AS DateTime), CAST(N'2012-10-08T14:11:55.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1ea29037-0643-5b11-a0f1-909bf0be2c5e', N'Elisa', N'Cooke', NULL, N'Prepaid Customer', N'aypu7@sdmlqizsz.u-umln.net', CAST(N'1998-01-14T06:25:38.590' AS DateTime), CAST(N'2010-04-15T06:13:08.120' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'50f20e07-f69a-e351-f002-910cef404686', N'Gena', N'Frank', NULL, N'Technical', N'tbiydoa.nrmt@seyjqxsje.cupcgn.org', CAST(N'2013-09-27T14:57:40.120' AS DateTime), CAST(N'1988-12-13T01:02:52.990' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7995ebdc-9ff1-f248-08fa-916b2e31263c', N'Penny', N'Hunter', NULL, N'Prepaid Customer', N'eruf.yrmrgpvey@dfjz.fjsyh-.org', CAST(N'1978-07-19T19:39:27.260' AS DateTime), CAST(N'2017-04-27T05:55:19.230' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8ee3b0a2-17e8-5e4d-78bf-91702b1f2ad4', N'Hilary', N'Obrien', N'Redimar Direct ', N'Technical', N'lfamjzd.luzyyfo@lnvmub.org', CAST(N'1964-12-04T08:48:15.760' AS DateTime), CAST(N'2000-09-15T08:58:42.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f10716c0-932b-39dd-438e-919db7f7ecaf', N'Carla', N'Jackson', N'Supsapower International ', N'Customer', N'odghglie.ulvewebp@pxxghs.ekdvnn.net', CAST(N'2000-06-07T11:55:26.920' AS DateTime), CAST(N'2008-06-06T10:19:07.430' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'58e389ad-b7f9-9b59-134d-91e8d8084ae2', N'Rickey', N'Sweeney', NULL, N'Prepaid Customer', NULL, CAST(N'1971-05-14T15:42:34.700' AS DateTime), CAST(N'1991-02-20T08:58:51.290' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'45cad154-4fe6-9fdf-8431-92a851e8114c', N'Duane', N'Frazier', NULL, N'Prepaid Customer', N'asxukiv311@jrhpaqfzk.atinct.net', CAST(N'1956-08-25T20:52:39.200' AS DateTime), CAST(N'1962-03-02T09:31:43.510' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e6a26b9d-4fd9-77ac-9e13-92e17256a07f', N'Nikki', N'Pearson', N'Truerax  Corp.', N'National Customer', N'suaxmhrx.dyrwth@pvepju.net', CAST(N'2002-08-13T07:49:52.890' AS DateTime), CAST(N'1976-02-17T17:36:53.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5dcf8b32-c86e-2a23-c9a6-92fa36e57a79', N'Timmy', N'Shepherd', NULL, N'Prepaid Customer', N'eaarr.brrpf@vwpsawm.hexlfy.org', CAST(N'1957-07-05T08:57:41.410' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ebdeec85-334b-a1a6-7360-936a5e203b9b', N'Warren', N'Berry', N'Monwerpaquicator  ', N'Prepaid Customer', N'ruhg.vllonmcm@fooabf.com', CAST(N'2003-02-06T07:52:07.880' AS DateTime), CAST(N'1991-11-28T13:17:03.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'612cc783-bd84-acd9-1f2e-93bad43a660b', N'Cesar', N'Vance', NULL, N'Technical', N'mbecsx5@afaykt.org', CAST(N'2007-09-14T04:11:26.050' AS DateTime), CAST(N'1960-03-28T12:58:12.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'21b48d96-1e73-8dbc-accf-93e294bd5e35', N'Angie', N'Dickson', N'Vardimower International Corp.', N'Technical', N'zyyxdd1@zgsvwywqk.eecpsq.net', CAST(N'2006-10-08T13:45:33.320' AS DateTime), CAST(N'2005-03-01T03:22:30.810' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2e16a7ba-e2a8-9ab8-afe4-940dbc5bf800', N'Irma', N'Hicks', N'Cippebamin  ', N'Web', NULL, CAST(N'1994-02-03T13:19:07.150' AS DateTime), CAST(N'1964-06-12T22:53:58.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0016e8c6-d56f-d7a3-1764-9466385d3484', N'Jerrod', N'Valencia', N'Uptumadover WorldWide ', N'Technical', N'lhkhqf.jwkf@vriczyw.hiwjru.com', CAST(N'1993-07-28T09:30:59.310' AS DateTime), CAST(N'2012-08-16T01:56:42.220' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e878f0ac-b8c6-aa65-b51f-948798c29268', N'Phillip', N'Neal', N'Zeesipaquex  Inc', N'Accounting', N'crkrk881@giiheetp.ykbuxf.com', CAST(N'1979-08-03T15:12:14.780' AS DateTime), CAST(N'1990-01-27T18:13:12.140' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'32c5b4db-de85-311e-ab48-94c425903cf2', N'Everett', N'Chambers', NULL, N'International Customer', N'yiwj.wddmqc@mvjyobpn.cgiiuu.com', CAST(N'2001-11-07T13:09:35.420' AS DateTime), CAST(N'1971-01-24T18:07:41.200' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e08c2dac-5e73-e48b-6182-94ff1b6346c3', N'Darrick', N'Mata', N'Insipinover WorldWide ', N'Accounting', N'hhaeqqv.bxxrduku@-ilbxu.org', CAST(N'1969-03-19T23:05:44.830' AS DateTime), CAST(N'2001-06-12T18:16:01.140' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2d41feda-710f-78ec-9621-95078f4a4d8e', N'Dina', N'Mcgrath', N'Grotumaman  ', N'Service', N'rirvi.jnsnfpoh@lfvvv.jdc-cu.net', CAST(N'1982-11-28T20:10:46.430' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6823ee75-e3c6-1533-4812-95c30721768f', N'Virgil', N'Waller', N'Zeequesticantor Holdings Company', N'Technical', N'iazbbf@kgcn-z.net', CAST(N'1992-06-20T19:46:37.820' AS DateTime), CAST(N'2006-04-05T10:39:31.000' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'15eafd11-8ba1-0d3d-cbf1-95d5781193e7', N'Randal', N'Nicholson', NULL, N'Accounting', N'hvpxkrc.ocssyq@dfnfx.e-ndnj.com', CAST(N'1992-01-15T08:28:57.530' AS DateTime), CAST(N'1968-01-04T10:28:19.790' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a3764951-94d6-3769-2b6d-95f3b9ef060e', N'Claudia', N'Newman', N'Dopcadaquin Holdings ', N'Prepaid Customer', N'dvlskbpd.mtlwrjqzxg@dnlvyj.ekc-kd.com', CAST(N'1980-07-01T17:48:54.780' AS DateTime), CAST(N'1965-09-15T01:00:51.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'95def12d-601d-db1e-aa0c-95fd2eb2f34e', N'Frank', N'Parker', N'Pareronentor  Group', N'Web', N'ipyhcpm@ocskkg.org', CAST(N'1981-06-15T03:46:52.000' AS DateTime), CAST(N'1993-06-30T04:44:23.650' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'78dae9da-62c7-618a-10b3-9616f2c7d3aa', N'Melinda', N'Rubio', N'Tupbanicor Holdings Inc', N'Service', N'vymov.vemhgph@ubgfxk.ujyhiy.com', CAST(N'1962-07-11T01:46:37.340' AS DateTime), CAST(N'1979-05-23T17:50:48.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6c12cc68-a5b6-72fa-75ae-963a3795e26f', N'Rodney', N'Foley', NULL, N'Accounting', N'ymjv.kbhwtf@misdq.nlmvpd.com', CAST(N'1956-04-19T08:14:25.380' AS DateTime), CAST(N'1988-03-27T07:17:54.240' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'845015a1-4d67-2b00-0fe2-965224795e47', N'Elena', N'Petersen', NULL, N'Technical', N'hnkewqj3@ufcn.zglvip.net', CAST(N'1984-10-25T05:10:21.340' AS DateTime), CAST(N'2010-03-05T17:55:40.960' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3d7d0599-0b67-3d94-c701-9658be88d3d0', N'Janelle', N'Richardson', N'Insapaquower Direct ', N'Accounting', N'qwmwbm.qzsoolbpv@fhbkyn.org', CAST(N'2007-05-31T06:02:34.090' AS DateTime), CAST(N'2017-04-12T04:21:20.530' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b5b3faae-dec7-9c14-693e-96a733d0edbb', N'Randall', N'Gentry', N'Surdimex  Corp.', N'Service', N'dddgdn.engwrl@bcmbqx.xktaxx.net', CAST(N'1966-10-26T03:08:35.970' AS DateTime), CAST(N'1966-06-01T23:37:37.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6d9de423-992d-7114-72f2-971ca5e9a099', N'Nina', N'Mckay', N'Bardudover International ', N'Accounting', N'tchhcjua.usupjqbogt@tdeyd.psdfbn.com', CAST(N'2016-07-15T17:53:45.320' AS DateTime), CAST(N'1964-12-23T22:00:30.770' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'861508d8-f66f-c363-d496-973ef8a7818d', N'Garry', N'Cochran', N'Dopmunex WorldWide ', N'Web', N'digx6@stvoes.net', CAST(N'1983-01-27T07:39:15.310' AS DateTime), CAST(N'1971-02-03T19:03:47.230' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5b9e9d9b-106a-9d65-a56b-9780da430095', N'Rex', N'Braun', NULL, N'Web', N'epkbpz.fguhv@lmemkgoj.swnziu.net', CAST(N'2013-12-11T09:42:21.690' AS DateTime), CAST(N'2014-12-15T03:02:13.350' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'db61f9fb-6751-1fd1-1f75-97a7cbaf3d65', N'Hector', N'Forbes', N'Upglibopor  ', N'Service', N'wbuqfhx3@hirfhcb.zpunss.org', CAST(N'1986-08-02T04:08:25.470' AS DateTime), CAST(N'2009-08-07T11:07:57.880' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd6d72663-14dc-8ccc-0a70-982a19366699', N'Preston', N'Costa', NULL, NULL, N'edier466@fpqf.mc-qep.net', CAST(N'1993-04-30T22:16:44.780' AS DateTime), CAST(N'1988-03-03T17:16:06.620' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a69867ca-b0fb-7722-c1a8-987a8e06a89b', N'John', N'Oliver', N'Truglibentor International Company', N'Web', N'ybdfaoq6@bsipmr.net', CAST(N'2009-11-17T08:46:00.290' AS DateTime), CAST(N'1965-09-25T22:15:35.310' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd96cfa78-2286-cc66-7bfe-98d23a7e88e1', N'Nakia', N'Krause', N'Klidudistor Holdings Group', N'Prepaid Customer', N'wpvdfv.cyjv@mkrinert.yyoiyi.org', CAST(N'2007-07-28T20:44:18.840' AS DateTime), CAST(N'1988-08-23T07:42:42.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2ddf17f1-9a0f-6245-3009-98f515e54e44', N'Marcella', N'Sosa', NULL, N'Technical', N'uebdzak.szgshxvf@xqwuyl.fgeoxh.com', CAST(N'2002-09-30T10:26:23.990' AS DateTime), CAST(N'1983-05-22T08:43:59.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1eb1e4dc-410e-ee09-7cc0-99033ca9c8b7', N'Nathan', N'Woods', NULL, N'National Marketing', N'ipbc562@ynnhusqj.knaote.org', CAST(N'1961-01-17T14:39:34.070' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5b345208-16e0-a373-62cd-9a1b062260a0', N'Sonia', N'Briggs', NULL, N'Accounting', N'bmpj010@frtihu.org', CAST(N'1983-03-07T12:00:03.200' AS DateTime), CAST(N'2009-06-12T16:42:06.420' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cb0b507c-870d-583b-4b2f-9a45aecc0e4e', N'Reginald', N'Alvarado', NULL, N'Service', N'nrzx1@uexljiwyd.d-erst.org', CAST(N'1977-02-18T03:08:35.010' AS DateTime), CAST(N'1982-06-08T14:19:01.140' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'60d3a9f5-f655-dfb0-e281-9a60036890dd', N'Janette', N'Wheeler', N'Klizapazz  Company', N'Web', N'febzrkfj50@jgunuyit.rynhky.net', CAST(N'1997-09-10T03:21:37.180' AS DateTime), CAST(N'1963-12-08T04:03:04.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5e803a08-597e-6e07-eb74-9ad04b1b46b0', N'Andrea', N'Cain', NULL, N'Service', N'xnst.obruy@irjqcwmr.jlmwxa.com', CAST(N'1984-05-11T10:21:53.250' AS DateTime), CAST(N'2012-08-27T07:41:58.600' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c2780b31-4733-ed48-a5ff-9b169ffddf8a', N'Peggy', N'Donaldson', NULL, N'Technical', N'urlwca.pwxon@frsmgf.net', CAST(N'1978-06-23T02:17:38.290' AS DateTime), CAST(N'2007-08-10T06:57:16.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'26dc97d2-09db-9a82-7c2b-9b17ea16be83', N'Lamar', N'Byrd', NULL, N'Technical', N'gbvn.ygfvzfe@pbsawsidp.cmwpjb.com', CAST(N'1964-03-13T22:30:45.120' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'85bae9db-7bfe-ecd9-a58e-9b4b6d50c450', N'Lana', N'Villarreal', NULL, N'Web', N'iqizrwq9@fpitnx.p-lj-s.com', CAST(N'1975-01-12T09:29:13.870' AS DateTime), CAST(N'1995-07-06T02:14:26.160' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7a29d9e5-dabc-890e-2ca4-9b4cdf504e8e', N'Jessie', N'Hill', N'Adsipepentor Holdings Company', N'International Customer', N'ldyzpwhy.vgdpas@nzqvihbgt.qmzxs-.com', CAST(N'1977-04-27T17:19:35.910' AS DateTime), CAST(N'2010-11-05T06:29:37.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1e31a551-c277-84ef-2a30-9bafe7cfe8aa', N'Janette', N'Richards', NULL, N'Accounting', N'pnsabsq@hqotxy.net', CAST(N'1994-09-14T04:39:05.100' AS DateTime), CAST(N'1982-04-12T13:29:39.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8378a9eb-6301-7d9b-4474-9bb6edbe0645', N'Penny', N'Odom', N'Suptinaquower Direct ', N'Service', N'zvqe.nfpfvdoz@inmemc.net', CAST(N'1965-04-12T01:19:11.100' AS DateTime), CAST(N'1969-10-03T22:38:32.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'88ff71de-8e98-c7d2-7ed8-9c1abe66ee21', N'Chastity', N'Garcia', N'Uneredentor Holdings ', N'Accounting', N'ankwnetq.flmri@qqavzb.net', CAST(N'1982-05-11T07:13:58.010' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5edb671a-df8a-ef63-3cb8-9c7f67954c55', N'Kimberly', N'Mc Mahon', NULL, N'Consumer Customer', N'hlljppbs.gsmygan@uazje.jri-xj.net', CAST(N'2000-02-03T12:42:36.140' AS DateTime), CAST(N'1979-10-12T04:56:44.480' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'dd59b4f2-f445-d22e-b416-9d7e38ce3bd0', N'Bret', N'Joyce', N'Upnipewazz WorldWide Company', N'Web', N'tmit.iqwtxajtzl@rsape.vfkady.org', CAST(N'1986-11-14T09:44:07.560' AS DateTime), CAST(N'2013-03-13T04:58:16.280' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4fdfbd59-41bd-49f7-1e1a-9da392d0dd3d', N'Lorenzo', N'Adams', N'Lomtanin International ', N'Web', N'sedv73@omhgre.com', CAST(N'1963-10-30T03:20:48.740' AS DateTime), CAST(N'1986-12-07T21:55:27.430' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'636e5a42-a71c-3778-70b1-9dd4f1363399', N'Felipe', N'Le', NULL, N'Service', N'swdgt.attsvnes@kuri-o.org', CAST(N'1963-01-06T10:29:55.970' AS DateTime), CAST(N'2009-02-22T10:17:29.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'51ae673a-3b31-e2c3-b369-9e0e252082f9', N'Olga', N'Alexander', NULL, N'Accounting', N'nrpow@lbzjsv.-eseof.com', CAST(N'2006-04-04T13:19:40.670' AS DateTime), CAST(N'1959-05-18T18:24:55.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd4dbd056-c0ec-92eb-e635-9e28e1e5638f', N'Lloyd', N'Macias', NULL, N'Service', N'ivchraz.mztfg@kjbsqvpx.bhyfd-.org', CAST(N'1972-12-21T01:40:09.740' AS DateTime), CAST(N'1958-06-04T03:02:13.920' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0c48d959-cc13-c5df-d1c9-9e359906f8f2', N'Jeremy', N'David', NULL, N'Web', N'ogguxetp@gnobxt.org', CAST(N'1957-06-27T02:18:12.100' AS DateTime), CAST(N'2006-04-01T15:56:38.750' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0e9df77e-ee1e-cc20-c273-9e903a945652', N'Emily', N'Marks', N'Redimilin  ', N'Accounting', N'vpobmqwe@mogevy.com', CAST(N'2016-10-19T21:32:44.120' AS DateTime), CAST(N'1992-06-04T14:51:48.850' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5f5f4471-54ba-e78f-9c7b-9ed454df7daa', N'Jamie', N'Bradley', NULL, N'Prepaid Customer', N'echhmcl.qyziliq@yxsrcs.org', CAST(N'2004-04-21T20:00:44.690' AS DateTime), CAST(N'1987-03-07T00:53:49.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'73b1db41-b2f9-3944-3d9f-9f022f7a4271', N'Howard', N'De Leon', NULL, N'Prepaid Customer', N'doip.pnzjnmwg@tvpiqtyrp.zsborw.net', CAST(N'1978-08-02T17:47:19.520' AS DateTime), CAST(N'2010-12-23T16:09:30.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bd32c87e-d13a-f98d-6b1c-9f081c918f1b', N'Aimee', N'Nash', NULL, N'Accounting', N'onfam.bijzwrd@ozgopu.com', CAST(N'1953-09-25T14:39:25.160' AS DateTime), CAST(N'1970-06-13T01:40:58.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd69804d7-534e-3c43-dbae-9f37e5e516d6', N'Dennis', N'Nunez', N'Thrusapedgistor Direct Corp.', N'Web', N'spznv.aerogvcypx@okyjoh.com', CAST(N'1997-08-17T01:32:42.850' AS DateTime), CAST(N'1989-04-21T08:54:15.690' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8dc1b5dd-b0ca-c9a6-eae8-9f6aa9ed1452', N'Regina', N'Good', NULL, N'Technical', N'owydz.yemzee@tuzk.tguhth.org', CAST(N'2000-01-19T07:06:08.210' AS DateTime), CAST(N'1997-01-02T07:52:33.540' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6839da74-c020-aec3-8556-9f890273c5f2', N'Everett', N'Gillespie', NULL, N'Technical', N'dliiqc762@zxibyv.net', CAST(N'1982-05-30T12:13:43.590' AS DateTime), CAST(N'1970-01-30T19:30:56.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'689f7e8d-bd65-eac6-a94a-9fbe8dd83dcd', N'Cornelius', N'Golden', N'Zeedimover Holdings ', N'Prepaid Customer', N'rhpuumke@vpqfkj.org', CAST(N'1953-06-16T02:38:59.260' AS DateTime), CAST(N'2011-07-19T18:10:21.760' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'239a2c3a-4940-1330-3304-9ff05c645f6e', N'Whitney', N'Mc Gee', N'Suppebax Direct ', N'Service', N'pspxgq.cgtkguaj@batdxt.com', CAST(N'1999-05-17T20:16:30.810' AS DateTime), CAST(N'1989-04-01T14:38:52.680' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a5c2ca5a-03e4-db38-7708-a16c9facfa21', N'Malcolm', N'Zavala', NULL, N'Accounting', N'fuqgh314@ogbkd-.net', CAST(N'1984-10-15T03:31:54.380' AS DateTime), CAST(N'1978-10-14T04:21:47.860' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ea06b368-3e45-4eac-da1a-a16df4c8d3ef', N'Allen', N'Small', N'Doptanonentor WorldWide ', N'Service', N'eosxw.pjetpualzq@-fgmbp.com', CAST(N'1990-08-13T10:31:05.980' AS DateTime), CAST(N'1985-12-27T02:08:42.430' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cbd6ae3c-e750-17f1-bad5-a22c08ea5252', N'Terrance', N'Dillon', N'Unpebopistor Holdings Corp.', N'Service', N'qxchtx.nvwv@pmeww-.org', CAST(N'1967-02-17T19:52:55.410' AS DateTime), CAST(N'2001-10-26T15:09:33.100' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'19499383-1d5b-b027-ffad-a27383d2aa9e', N'Noah', N'Velasquez', NULL, N'Prepaid Customer', N'ekzrii343@hock.ouv-tn.com', CAST(N'1990-10-11T18:41:16.140' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fd79c1f8-6ec3-4bc8-a606-a281b16e78a4', N'Keisha', N'Petty', NULL, N'Service', N'dtrsqkf@xsmgi.wicpvl.com', CAST(N'1970-06-13T00:46:54.000' AS DateTime), CAST(N'1972-01-20T20:10:36.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd96a1178-72d9-304e-3cbd-a2fd64c43338', N'Armando', N'Franklin', N'Growerpinicator  ', N'Web', N'lgdfd@vrphosxn.-knub-.net', CAST(N'1956-01-20T02:50:25.330' AS DateTime), CAST(N'2008-05-04T19:32:04.790' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2c0f0b65-f5ef-02dc-4482-a372eac2b9eb', N'Rochelle', N'Shepherd', NULL, N'Prepaid Customer', N'sdqro.kihis@vlmnwigy.hibvyo.com', CAST(N'2018-12-03T16:39:50.360' AS DateTime), CAST(N'2004-04-16T16:37:06.530' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3d632681-b862-b188-22e9-a399741f3931', N'Edith', N'Lam', N'Frovenewover  ', N'Technical', N'oekycjkr.dgbikmuhs@muln.blptlg.com', CAST(N'1987-03-27T01:57:19.130' AS DateTime), CAST(N'1954-02-13T05:17:54.770' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'30a6be37-ae6d-a411-ac95-a3c5bd749a27', N'Jessie', N'Martin', NULL, N'Prepaid Customer', N'npowjw727@gtkfzf.com', CAST(N'1967-07-03T18:58:08.480' AS DateTime), CAST(N'2000-04-27T09:33:40.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b9c1b2ff-325a-8554-6bdc-a43159a5b7b8', N'Sally', N'Bishop', NULL, N'Prepaid Customer', N'hbfanl.ivmhmcg@obniga.wqdjaq.net', CAST(N'1978-01-20T16:07:05.760' AS DateTime), CAST(N'2015-09-03T04:26:40.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'49b0f9f1-1268-8425-9cde-a44d56e76d00', N'Sharon', N'Ayers', N'Dopnipax  ', N'Web', N'sxswsif82@idvl.qkptah.net', CAST(N'1958-09-11T08:27:58.220' AS DateTime), CAST(N'1968-12-25T20:37:26.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5a5fdf88-6e57-0c8e-ca8e-a47294a4fcb8', N'Rachelle', N'Hatfield', NULL, N'Prepaid Customer', N'ktmgugtt.jjxf@ydktit.net', CAST(N'1968-08-22T13:33:22.470' AS DateTime), CAST(N'1972-07-09T10:16:35.630' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bb1a180f-3404-2e1a-a00f-a4a7ee521bca', N'Norma', N'Robles', NULL, N'Service', N'cuhl.dbntl@vfptel.com', CAST(N'1990-05-31T09:39:44.320' AS DateTime), CAST(N'1956-11-21T12:46:13.880' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'dbc54f3b-f076-0f18-e0f5-a4eb0cb5f151', N'Debbie', N'Moon', NULL, N'Service', N'ojehzxfl.xnjnvqmjmu@mjjp.dopbxq.net', CAST(N'2016-07-06T10:33:34.700' AS DateTime), CAST(N'1989-10-18T00:34:01.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f04445d9-2818-01ed-a2a3-a507d072d726', N'Sarah', N'Barron', N'Inerex International Corp.', N'Web', N'fbkyqm.uubgnp@imsttf.com', CAST(N'1960-07-15T09:36:38.020' AS DateTime), CAST(N'1966-06-08T14:50:11.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c05408fa-7d3f-c0a8-b100-a538ea339856', N'Marlene', N'Hardy', N'Emdimower  ', N'Accounting', N'cysmcx@luammd.net', CAST(N'1996-11-21T15:24:29.050' AS DateTime), CAST(N'1986-04-05T00:56:34.620' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f56619d6-9822-b2c3-49f5-a5f1a7330e22', N'Susana', N'Coffey', N'Adnipicator  ', N'Web', N'cong38@xyngzt.wtoutv.net', CAST(N'1990-11-03T00:51:05.730' AS DateTime), CAST(N'1959-04-24T08:38:10.680' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'da189b90-61e2-a231-965e-a5f4971d2705', N'Sergio', N'Proctor', N'Truvenin WorldWide ', N'Technical Sales', N'hkkb197@rzuai.hsorxe.net', CAST(N'1983-03-04T16:13:10.730' AS DateTime), CAST(N'1987-07-12T12:44:44.020' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0c8ebcc7-79f4-62b5-e348-a618599b5a19', N'Mandi', N'Wiggins', N'Qwinipistor Direct Company', N'Web', N'avvuay.oguurbrfsi@fzeztet.wzeiji.net', CAST(N'2007-11-03T11:33:37.310' AS DateTime), CAST(N'2006-03-14T15:48:26.080' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b78de124-a67a-a73a-cb9b-a62859a9f064', N'Jaime', N'Stokes', NULL, N'Service', N'nbpuk@lvcirr.rwmuoe.org', CAST(N'1958-04-03T19:43:39.850' AS DateTime), CAST(N'1998-10-12T14:26:58.170' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'94211eb1-79e8-9a82-ba1d-a6a10c2c5641', N'Fred', N'Blackwell', N'Zeepebefower  ', N'Technical', N'knuq649@tkakub.net', CAST(N'1990-05-21T21:01:28.620' AS DateTime), CAST(N'1991-09-22T19:02:59.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bd9afe0d-6597-0ff6-bb8b-a6d0fc17c9aa', N'Danny', N'Montgomery', NULL, N'Accounting', N'xjcngrkt.oqftoq@kbcftu.org', CAST(N'1977-05-19T11:26:15.760' AS DateTime), CAST(N'1994-10-20T16:23:22.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7c56023a-3fe7-bdc1-8f5a-a6e5a9c828ba', N'Morgan', N'Weber', N'Hapnipazz  Corp.', N'Technical Sales', N'tydmy.lirh@ltwjvp.com', CAST(N'1995-08-19T13:21:18.230' AS DateTime), CAST(N'1961-05-12T21:25:18.440' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'936e8632-3643-fdd6-81d9-a70906a585f8', N'Manuel', N'Vazquez', N'Vardudax  ', N'Prepaid Customer', N'kghyrmh775@suiuzj.com', CAST(N'2006-03-31T21:42:59.780' AS DateTime), CAST(N'1985-02-04T22:21:09.980' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'422821e9-e2fa-2858-41c0-a70ef072ae23', N'Joan', N'Reese', NULL, N'Prepaid Customer', N'pbwwnuc088@hqyv.nakkpv.org', CAST(N'1986-06-11T00:21:44.290' AS DateTime), CAST(N'1959-09-08T06:17:47.860' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7dfe2f2a-75fd-0df1-7843-a7a94282ab00', N'Chad', N'Richmond', N'Parglibiman  ', N'Corporate Customer', N'ycrpupio.dvclzkoaod@jbfgrnexi.-sgymr.org', CAST(N'1969-06-04T20:25:30.420' AS DateTime), CAST(N'1968-02-20T01:14:55.160' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7d3d13ca-101d-7f2b-d3b6-a7fb0c0b4b2e', N'Andrew', N'Reeves', N'Truvenewor WorldWide ', N'Web', N'yxcrm787@kklr.-fkzlx.org', CAST(N'2007-11-09T11:45:36.590' AS DateTime), CAST(N'1980-01-03T02:28:54.570' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1f4b7525-a370-565c-7e36-a804dcb77eb9', N'Erica', N'Raymond', NULL, N'Technical', N'gyooo494@oxvkj.mdxbci.com', CAST(N'2008-07-04T09:31:07.820' AS DateTime), CAST(N'1953-05-24T04:54:01.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f962ca02-7f68-c467-c1da-a8051185294b', N'Edith', N'West', N'Barpebin  ', N'Prepaid Customer', N'gsamfl.epulzjrxxz@cgmygt.net', CAST(N'2016-07-25T13:50:40.960' AS DateTime), CAST(N'1969-01-04T05:38:16.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0f2d6652-6892-db86-cb4b-a80ef25ad2db', N'Tracey', N'Mosley', NULL, N'Prepaid Customer', N'chavqrvm0@gkphbd.org', CAST(N'1975-07-17T14:32:03.140' AS DateTime), CAST(N'1964-09-01T10:17:01.890' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a595244a-ed3d-3d1f-e19b-a8288da63bb1', N'Denise', N'Keith', N'Frorobin WorldWide ', N'Technical', N'rnrbp.yqwuqv@tohaqtjaz.qpykoh.net', CAST(N'1956-04-26T21:58:22.850' AS DateTime), CAST(N'1971-03-27T20:58:06.230' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8b8767a8-dd65-2f5a-cc47-a836b58c5d0e', N'Marcie', N'Berger', NULL, N'Service', N'oojzj.inhgqqnr@-cwixz.com', CAST(N'1960-07-29T22:14:46.330' AS DateTime), CAST(N'2013-06-02T05:46:06.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'24e620b9-1b9f-b3df-b10e-a85ab59222d6', N'Julie', N'Herring', NULL, N'Service', N'xfwcly.rkgqilb@tzezta.com', CAST(N'1999-01-01T18:45:20.260' AS DateTime), CAST(N'1992-10-20T03:17:57.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0ec2f7c3-43b6-7002-4b85-a95549d2382f', N'Annie', N'Larson', NULL, N'Corporate Marketing', N'jjug38@rphvbe.zioroo.net', CAST(N'2008-09-03T04:10:54.670' AS DateTime), CAST(N'2016-11-02T08:46:02.560' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4774734b-48d5-713d-5d36-a963453164be', N'Anne', N'Saunders', N'Emkilower Holdings ', N'Marketing', N'mpjkbgjt77@aszooyld.yoeqye.org', CAST(N'1990-05-13T18:23:37.140' AS DateTime), CAST(N'1978-03-09T03:14:45.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0f7556ff-2051-d57c-1148-a9b2ce00a888', N'Dale', N'Underwood', NULL, N'Web', N'rhciy.ugvwhwd@irwdpr.org', CAST(N'1994-01-31T19:48:33.180' AS DateTime), CAST(N'2002-08-29T15:10:41.280' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b3e91ed9-f256-f544-77a3-aa0557f8af23', N'Roderick', N'Mack', N'Partinantor Holdings ', N'Service', N'ipdcx.xsmshygw@azzzet.net', CAST(N'1999-10-29T15:29:18.910' AS DateTime), CAST(N'2012-02-03T08:04:23.500' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'282452aa-edba-3df3-2034-aa055aa57c3a', N'Chastity', N'Greene', NULL, N'Accounting', N'qoltyvzk86@vcjva.s-ilsj.org', CAST(N'2003-11-01T02:23:08.620' AS DateTime), CAST(N'2015-03-19T18:53:57.430' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'179db00c-33fe-1e15-f6c0-aa34d0c31d07', N'Diane', N'Chavez', N'Parcadistor  ', N'Service', N'lwnelay.cfhbzyi@ymwjgtus.xjyqlp.org', CAST(N'1999-06-25T18:41:05.810' AS DateTime), CAST(N'1991-09-03T20:05:40.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2ffd9d63-441b-49e7-832e-aad877657e0c', N'Linda', N'Landry', NULL, N'Prepaid Customer', N'loci.kjmzn@kbiojybit.uyfww-.net', CAST(N'2012-11-06T20:49:41.530' AS DateTime), CAST(N'2018-10-27T11:52:00.500' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a39f229d-b0b7-f608-e0e2-aae9b1d8dfeb', N'Penny', N'Riddle', NULL, N'Accounting', N'tjkyt6@magypc.net', CAST(N'1974-01-04T18:12:20.490' AS DateTime), CAST(N'2012-09-10T18:08:57.800' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'42eb918f-7cff-f324-e511-aae9c3d8b2e6', N'Caroline', N'Bernard', NULL, N'Service', N'puybvj670@ptgohp.org', CAST(N'1981-03-12T21:57:15.650' AS DateTime), CAST(N'1997-04-04T15:29:40.590' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4d5ca1f6-65d6-d10a-33f1-ab558d0b9de4', N'Terra', N'Reid', NULL, N'Service', N'vkfsyl.xgbhfixgy@rtsuxv.com', CAST(N'1976-06-11T14:43:12.020' AS DateTime), CAST(N'2002-09-07T23:01:57.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'74a002fd-ec01-437c-45e3-ab8a46daa2d3', N'Brendan', N'Stout', N'Thrubanadistor  Corp.', N'Web', N'oavfpmr425@ccwf.qikvtz.org', CAST(N'2018-12-07T23:32:28.990' AS DateTime), CAST(N'1961-03-24T19:02:29.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b8047e5b-9451-2b0e-57f1-abcecfb0000f', N'Marsha', N'Silva', NULL, N'Service', N'jklrocbs.zwjlkzns@otdlpp.gzgdvw.net', CAST(N'2014-07-21T07:40:29.920' AS DateTime), CAST(N'1980-12-05T08:52:57.140' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6b7d1687-01f7-4cf0-9e3c-abd4bd0d6963', N'Melisa', N'Travis', N'Emtanilover International Inc', N'Accounting', N'recdl.bjje@cbopbf.net', CAST(N'2017-11-26T16:35:06.810' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cffcdf67-800a-1141-bc12-ac40ff74f496', N'Earl', N'Richards', NULL, N'Web', N'tgzje731@pechic.net', CAST(N'2014-12-23T00:04:02.280' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'892b2a46-e0c6-c3bf-3ee3-ac68e17888ba', N'Alexandra', N'Riley', NULL, N'Web', N'cnjvlyth.vass@hcndjg.net', CAST(N'1968-07-13T22:36:28.300' AS DateTime), CAST(N'1966-09-27T23:15:33.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'63f3dc83-3e1e-baab-84dc-ac7b7878a68f', N'Paul', N'Navarro', NULL, N'Technical', N'rqbzq31@kbpxcfsk.ezgynt.org', CAST(N'1966-06-20T07:32:16.090' AS DateTime), CAST(N'1978-11-23T18:57:55.110' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ec7fb192-b9b2-917f-4af8-ad00518d1ae8', N'Marla', N'Norton', N'Emquestimin WorldWide ', N'Service', N'zixd.qbvnd@yxystp.org', CAST(N'1989-01-27T17:46:51.890' AS DateTime), CAST(N'1958-09-12T08:00:20.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f7a6b9bf-27d1-ab5a-955a-ae0fc47afb54', N'Luis', N'Shah', N'Varglibollower  Corp.', N'Prepaid Customer', N'wudj.eoyhecuyw@zhpshg.org', CAST(N'1954-05-25T18:16:00.980' AS DateTime), CAST(N'2010-10-19T10:02:25.950' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7251598e-19bc-6e97-e94c-ae5777cc37e8', N'Ernest', N'Gamble', N'Inpickaquover  ', N'Web', N'mordyiv01@hzczsf.net', CAST(N'1972-08-12T09:38:46.690' AS DateTime), CAST(N'1962-04-11T20:57:55.380' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a3246e16-f984-4059-377f-ae9c6a87f784', N'Bethany', N'Lambert', NULL, N'National Sales', N'hrtzmxo.ajojazxmg@vgurfx.net', CAST(N'1990-06-19T10:09:41.660' AS DateTime), CAST(N'1968-11-20T01:24:43.970' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a8709c3a-1f84-9caa-64c3-aee25151a5e0', N'Elijah', N'Ali', N'Endjubover WorldWide ', N'Technical', N'zmgd781@ztllge.net', CAST(N'1981-01-24T15:15:46.240' AS DateTime), CAST(N'2012-06-20T13:02:02.100' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c601740b-14f4-07e2-49ec-af1a0181da12', N'Darlene', N'Hodge', NULL, N'Sales', N'tcbnfjq1@lmwpqf.net', CAST(N'2012-01-17T08:30:07.720' AS DateTime), CAST(N'1966-06-05T08:40:45.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ff98305a-97e7-e940-107a-af644df6cb1f', N'Brady', N'Yang', N'Reglibazz  ', N'Technical', N'jzlpqiqc74@rtvuas.org', CAST(N'1964-06-05T05:10:32.700' AS DateTime), CAST(N'1994-01-29T18:52:40.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'74c0bef6-05eb-2a61-b1bb-afe6831aa520', N'Salvador', N'Pearson', N'Zeetanover  Corp.', N'Prepaid Customer', N'ycndoui@kdjpomz.igwien.org', CAST(N'1969-11-01T09:56:28.610' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'30f20823-885a-332f-86ef-b08b142f744c', N'Lee', N'Warren', N'Tupzapex Holdings Group', N'Technical', N'bcjo.ckzskoovon@fubrkp.net', CAST(N'1996-08-14T22:12:43.300' AS DateTime), CAST(N'1976-10-03T00:44:39.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd2b4d74f-356d-4310-3eb5-b0d04a8bb09e', N'Ruth', N'Osborn', NULL, N'Prepaid Customer', N'iewzjdhx.fwuu@dqascj.net', CAST(N'1977-03-27T12:00:09.740' AS DateTime), CAST(N'1996-12-05T18:30:07.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'37b789ae-8092-b07c-44b3-b0ea90b75b8e', N'Mike', N'Bullock', N'Emdimonan  Inc', N'Prepaid Customer', N'cjryk.ckfchu@xymluf.net', CAST(N'1993-03-28T17:03:52.790' AS DateTime), CAST(N'1972-05-28T09:13:20.080' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e7aad64f-04ed-c896-5891-b0ed24e22a6a', N'Floyd', N'Gates', N'Frosipollan Direct ', N'Web', N'meaigpdq.tnlj@gwvbyx.net', CAST(N'1978-11-26T22:15:43.600' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'074055ed-73bc-f0e2-61d3-b0ed3e0e490f', N'Allen', N'Craig', N'Truglibar Holdings ', N'Prepaid Customer', N'yozzs@gu-zxn.net', CAST(N'1974-03-03T04:09:51.660' AS DateTime), CAST(N'1995-03-26T15:22:06.110' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f5076efb-20e9-9060-f690-b12282ed061d', N'Aimee', N'Villanueva', N'Kliquestackax  ', N'Accounting', N'uewoy.wwtzwk@rthdhp.net', CAST(N'1994-06-26T22:34:45.420' AS DateTime), CAST(N'1956-02-07T20:05:13.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f78925d7-6761-fb9f-2e8a-b127a0de24f8', N'Mark', N'Gentry', N'Lomquestegor WorldWide ', N'Web', N'tzcyd.scurentyv@bbbxoavie.exteey.com', CAST(N'1963-05-19T02:53:41.790' AS DateTime), CAST(N'1978-02-22T16:17:16.910' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'288b18e6-939e-a7ab-6cd6-b13e8001a20b', N'Antoine', N'Parks', NULL, N'Consumer Customer', N'gfyhk.twfugsu@msujsy.net', CAST(N'1987-11-17T19:15:13.480' AS DateTime), CAST(N'1964-05-03T17:35:45.100' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'dec74ba5-8d46-acdd-adff-b1a16c98c27f', N'Ginger', N'Gilbert', NULL, N'Accounting', N'otrrdsfr7@ddke-j.org', CAST(N'1991-09-28T04:03:37.660' AS DateTime), CAST(N'2015-07-08T17:10:20.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'696fbaa9-368c-683f-292a-b2692ca6e851', N'Adrienne', N'Quinn', N'Endtumonor Direct ', N'Web', N'bfjj@psqby-.org', CAST(N'1968-10-19T14:24:10.730' AS DateTime), CAST(N'1972-10-09T01:04:19.380' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'45f30b95-3dae-8a82-c4d4-b31df3253834', N'Tania', N'Walton', N'Surpebepicator WorldWide ', N'Accounting', N'imlq.opjxtud@qwcjen.org', CAST(N'2017-10-23T06:10:35.450' AS DateTime), CAST(N'1956-05-21T01:24:35.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'03051c03-0abc-59de-3c2d-b320c82c84de', N'Isaac', N'Travis', N'Upmunepax  Group', N'Prepaid Customer', N'djbfq.pwjkwxt@jbn-hr.net', CAST(N'1963-05-14T14:29:20.520' AS DateTime), CAST(N'1970-05-02T18:19:50.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7c78408b-9c6e-7467-46c7-b35aa3f3981b', N'Mathew', N'Cook', NULL, N'Sales', N'ghryzzos99@ccimvg.net', CAST(N'1995-12-25T21:43:36.900' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'575c2c5c-a12a-5c22-71a3-b3735b3e6bfb', N'Sylvia', N'Newton', NULL, N'Prepaid Customer', N'jtczbf.lafkxcllyc@fzsmkk.net', CAST(N'1984-09-04T08:01:52.900' AS DateTime), CAST(N'2005-08-10T15:01:42.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'77ecf47f-b8e2-4e3d-5b0c-b4088e6208a5', N'Ann', N'Rubio', N'Partanaman Direct ', N'Technical', N'jzomryd.umuhd@stwjzo.jeburw.com', CAST(N'2012-02-10T09:51:44.020' AS DateTime), CAST(N'2001-12-21T13:31:25.870' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5f95f536-21ef-5961-b255-b41994289f94', N'Cristina', N'Ferguson', NULL, N'Service', N'domaix685@sbitdruaa.iejjze.com', CAST(N'1982-07-05T22:28:06.440' AS DateTime), CAST(N'2006-12-14T16:16:27.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4d0900b8-cbd4-2f00-846c-b49866314b7b', N'Scot', N'Stark', N'Rapsipower  Company', N'Technical', N'mcoyzpt.ydin@jlmcyr.com', CAST(N'1982-02-13T02:36:05.670' AS DateTime), CAST(N'1982-03-27T09:11:33.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9c1634b8-4007-9d44-6cce-b4da71bda2df', N'Lawanda', N'Tucker', N'Inmunepor International Group', N'Service', N'glpsu3@rpukkm.com', CAST(N'1998-12-26T20:36:49.430' AS DateTime), CAST(N'1990-08-06T05:17:53.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b1f504b6-e4f3-6289-a2b1-b59dce95c9b1', N'Brett', N'Willis', N'Adbanax  ', N'Business Sales', N'skpx@czer-k.com', CAST(N'1969-04-27T16:03:04.850' AS DateTime), CAST(N'2009-02-26T02:22:56.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'95287c22-6e3e-204b-d430-b5b47ebb222e', N'Jeannette', N'Lynn', N'Cipbanegor WorldWide ', N'Web', N'ittzvxi.ihnjs@alz-k-.net', CAST(N'2008-08-20T03:14:06.830' AS DateTime), CAST(N'2016-09-04T07:45:23.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6674dc3f-b735-80d6-adf4-b6098abdec53', N'Joel', N'Hooper', NULL, N'Technical', N'zmlabry.hzxmthctx@oxoogur.uvgcrr.com', CAST(N'1988-03-14T00:16:24.630' AS DateTime), CAST(N'2017-06-04T08:01:30.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c097bf97-94b2-b905-1c6b-b61cfceba287', N'Darcy', N'Combs', NULL, N'Accounting', N'udahnbja41@tnpeoxwbi.zkgjer.org', CAST(N'1996-07-09T14:44:46.680' AS DateTime), CAST(N'2006-02-17T18:21:18.690' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'125a0388-366c-9eb4-1dea-b61dc70389a3', N'Sean', N'Shah', NULL, N'Accessory Sales', N'tfkkmazb.gbdjdn@urllwywg.rcisai.org', CAST(N'1960-03-20T14:04:49.740' AS DateTime), CAST(N'2003-07-11T04:46:22.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fc1fb38a-c437-319a-2404-b6278d6e6d11', N'Betty', N'Velasquez', NULL, N'Prepaid Customer', N'uarq.tpuwatzmdo@wklbat.org', CAST(N'1999-09-25T07:11:38.690' AS DateTime), CAST(N'1956-10-07T20:33:11.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fe5cb64b-b0c8-5883-0700-b67d8ff937cf', N'Jeff', N'Marshall', NULL, NULL, N'wdxnq33@fklbc.t-pcfw.org', CAST(N'1968-03-03T12:43:28.840' AS DateTime), CAST(N'1987-03-25T13:40:25.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5bef4028-a705-7fbd-e160-b69fd2734bed', N'Sidney', N'Ritter', N'Unjubazz Direct Corp.', N'Prepaid Customer', N'fppihftd.jfjmatpyh@yseweeaw.jxpjvx.com', CAST(N'1987-12-18T10:28:24.740' AS DateTime), CAST(N'1972-10-16T11:31:32.210' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3b2e8885-2970-34c7-bc40-b748606b9010', N'Kirsten', N'Mc Neil', NULL, N'Marketing', N'kaqrjwv.hutitv@dzycbd.net', CAST(N'1961-09-22T11:23:27.160' AS DateTime), CAST(N'1973-06-21T10:03:47.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'334605ec-212d-2f1d-810c-b757db4f5ec4', N'Michele', N'Swanson', N'Emhupor Holdings ', N'Accounting', N'ixlczvho.qqfgzz@efyr-x.org', CAST(N'1971-06-19T05:51:16.760' AS DateTime), CAST(N'1976-12-23T05:10:30.510' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cf3054c5-e3c8-d1c0-c7da-b777e94d9dc0', N'Jenny', N'Hancock', N'Uptinplantor Direct ', N'Service', N'eyjtp40@snhofkits.bcof-x.net', CAST(N'1958-03-21T08:25:50.830' AS DateTime), CAST(N'1959-04-08T16:10:21.920' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0800878d-1714-4a12-e5f8-b7d90375cdba', N'Sandra', N'Luna', NULL, N'Marketing', N'jfoynrrz.rrvwvwnjx@anxqen.net', CAST(N'1979-07-08T16:17:06.450' AS DateTime), CAST(N'2007-09-09T12:19:22.590' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5e82f887-d8f8-2c3f-0b40-b7e72d446bad', N'Victor', N'Medina', N'Undimex Direct Corp.', N'Sales', N'spgyzdeb53@vlbwis.org', CAST(N'1982-06-06T12:49:03.560' AS DateTime), CAST(N'1990-06-03T04:53:49.980' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bacf3825-ce30-d5b3-92c4-b7fed4b4f475', N'Elizabeth', N'Gutierrez', N'Supnipentor  ', N'Technical', N'cfybdz29@tbnjhn.org', CAST(N'1964-11-18T17:08:41.910' AS DateTime), CAST(N'1963-07-29T00:04:39.160' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'36517c9d-07a1-974c-a8af-b84e9b7a069c', N'Helen', N'Rodriguez', N'Unpebin WorldWide ', N'Web', N'wmgxbok20@spfdn.lelkwz.org', CAST(N'1969-04-08T05:10:10.660' AS DateTime), CAST(N'2003-01-19T03:47:22.860' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'04f7a104-dc81-f667-8061-b879a9e0ced4', N'Nicole', N'Krueger', NULL, N'Web', N'wlne283@iwa-bw.com', CAST(N'1960-05-25T20:13:23.900' AS DateTime), CAST(N'1999-04-27T12:41:06.570' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0caa0190-56c1-0ecf-73be-b896102e0ae2', N'Erin', N'Hinton', NULL, N'Business Sales', N'izcmjzfa.qyvrcf@epxuvv.org', CAST(N'1992-06-13T18:12:37.410' AS DateTime), CAST(N'1964-10-19T02:15:13.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'11cfe3d2-427a-beb9-fa0e-b8b3cc148409', N'Gerard', N'Griffin', NULL, N'Technical', N'wwxwe.uuaofhg@-ntgri.net', CAST(N'1968-12-04T19:25:31.830' AS DateTime), CAST(N'2014-11-18T07:54:12.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0a626034-b069-7c62-1016-b9db40bdf875', N'Danielle', N'Pruitt', N'Doppickimistor  ', N'Business Marketing', N'bykrm.arbhjalp@ficbih.com', CAST(N'1987-03-21T11:52:06.870' AS DateTime), CAST(N'1978-08-30T09:05:15.100' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7933f18e-9203-1013-6ae7-b9de8b4d71e7', N'Arturo', N'Vega', N'Tupquestedax  ', N'Prepaid Customer', NULL, CAST(N'1996-06-19T20:14:32.400' AS DateTime), CAST(N'1978-08-15T08:27:02.280' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd7d0b95a-a44a-2609-d36a-ba2c8b1ca09d', N'Tanisha', N'Huerta', NULL, N'Accounting', N'fltj.fsqpqewxf@isynha.d-lbvt.net', CAST(N'2013-12-02T11:30:52.580' AS DateTime), CAST(N'1955-03-01T05:16:58.390' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3d53e3dc-fc0e-cdb8-7dc0-ba97391f6976', N'Becky', N'Villa', N'Surtanistor Holdings ', N'Prepaid Customer', N'hzrlzrj.nlntlmb@qmaivxe.lazyml.com', CAST(N'1973-06-14T01:37:28.390' AS DateTime), CAST(N'1960-07-25T18:31:02.950' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cd7588f7-8e93-d68f-8f9e-baa9003c858b', N'Levi', N'Yates', N'Raprobedazz  ', N'Customer', N'znllun.tfsezf@xppjos.net', CAST(N'2008-02-15T07:06:13.480' AS DateTime), CAST(N'1963-08-27T06:07:18.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2dfbc1e9-ba23-015b-593f-bb058eadade4', N'Roderick', N'Robertson', N'Hapwerpinax Direct ', N'Accounting', N'rmueviuh.akrbgwuggr@vdjjxp.net', CAST(N'1954-06-06T23:06:13.450' AS DateTime), CAST(N'2004-04-18T09:44:52.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'60796318-a0b8-2bb2-0315-bb65cc77bab4', N'Ramon', N'Washington', N'Revenantor International Inc', N'Web', N'poodtjhu@rhcywgiz.-umbzk.com', CAST(N'1963-11-03T20:45:38.960' AS DateTime), CAST(N'2014-09-14T11:33:05.900' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c74c8daa-3f83-daa7-e359-bb89dce9ec50', N'Audrey', N'Blevins', N'Barjubefower  ', N'Service', N'thykmq231@xzqynt.net', CAST(N'1965-07-17T01:55:29.000' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'656e44cb-d249-7fc9-5f35-bba4321f5ecc', N'Carmen', N'Bass', N'Truquestor  ', N'International Sales', N'khfqp61@sxphv-.net', CAST(N'1964-07-08T11:57:33.890' AS DateTime), CAST(N'1997-11-26T11:16:43.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'19bc6981-b56d-8072-d6f0-bbffc74c5d86', N'Guillermo', N'Howe', N'Lomdudonentor  ', N'International Sales', N'xrqu.gcque@ajjbb.eqbmrb.net', CAST(N'1964-10-28T03:15:44.350' AS DateTime), CAST(N'1999-04-28T19:19:03.800' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b10b94f0-7bcf-45df-f1c0-bc14b274f5f6', N'Rick', N'Massey', NULL, N'Accounting', N'yghxu.yjrzgftgwk@ohnk.mfpnx-.com', CAST(N'1963-09-02T10:23:11.950' AS DateTime), CAST(N'2013-12-14T06:03:50.540' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1b2b8af2-897a-57af-cae0-bc1a01fff642', N'Teresa', N'Branch', N'Klihupaquex International Group', N'Technical', N'rteu63@tfeskf.com', CAST(N'1966-03-09T13:03:49.400' AS DateTime), CAST(N'1967-10-24T04:13:30.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c40bf9aa-974f-374c-dde1-bc6cf1080bc7', N'Corey', N'Bonilla', NULL, N'Technical', N'mzvaerec.qcjtiliveo@glswvpnyr.kdkfmf.net', CAST(N'1991-12-10T23:04:21.020' AS DateTime), CAST(N'2000-04-05T19:31:15.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b98b5776-6d66-d14f-18e8-bcb7549cfe12', N'Mathew', N'Powers', NULL, N'Service', N'gmmjfaal@a-tspu.org', CAST(N'1961-02-08T01:35:27.260' AS DateTime), CAST(N'2003-12-06T04:29:58.530' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3319eb7f-267e-aa54-3053-bd5f11162cdd', N'Mark', N'Sheppard', N'Adzapanover Holdings ', N'Accessory Sales', N'kevrdm990@qjbrld.net', CAST(N'1955-10-24T18:49:11.690' AS DateTime), CAST(N'1959-05-16T19:34:25.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'33386916-4c80-19a2-8d65-bd788a78849a', N'Allison', N'Lester', N'Endtumedistor International ', N'Service', N'fuzp005@mdx-xv.com', CAST(N'2014-07-17T21:10:14.870' AS DateTime), CAST(N'1970-11-25T16:50:59.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8f157a23-af8a-ba68-9273-bdd79572f3af', N'Rene', N'Holloway', NULL, N'Prepaid Customer', N'dras@vifamfufw.rkvrve.net', CAST(N'2016-04-10T09:59:10.760' AS DateTime), CAST(N'1973-06-07T14:23:15.600' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6d557292-40a7-e107-2527-bdffe7528afb', N'April', N'Wang', NULL, N'Web', N'orwreiol102@bier.iwbcks.net', CAST(N'1965-05-09T08:39:20.750' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'721166a1-9c32-0b14-14d2-be617e0276c9', N'Amanda', N'Gordon', NULL, N'Technical', N'xudwwa.jueank@sxguyw.com', CAST(N'1967-08-07T18:05:38.240' AS DateTime), CAST(N'1954-12-16T14:58:32.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'00af1dd3-313f-d954-fd1a-be7d7ba9d546', N'Neil', N'Barrett', NULL, N'Accounting', N'mqcrm.vprf@xqvxann.vxpbtj.org', CAST(N'1968-03-20T14:09:36.740' AS DateTime), CAST(N'1981-04-05T15:48:43.440' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'48304c02-9fe5-ec7a-1b2b-bff3d3ed7350', N'Gary', N'Melendez', NULL, N'Web', N'kpnynomr923@ryrjgrhr.gvdjwn.org', CAST(N'2005-03-22T20:29:36.880' AS DateTime), CAST(N'1982-11-24T12:53:40.460' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd6054e37-94b5-f0d1-8102-c06117eba401', N'Julian', N'Brooks', NULL, N'Web', N'mdnlu.eqqjeeqldh@buxjq.fjpvac.com', CAST(N'1975-12-29T15:33:43.820' AS DateTime), CAST(N'1989-12-09T11:58:47.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3189e744-f728-0307-59a4-c09afb494d07', N'Margarita', N'Morse', NULL, N'Accounting', N'kcdnxx.ghijgsm@smpvrxicq.lrynbd.com', CAST(N'1976-07-09T07:36:40.200' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a4f80cbb-38e2-c2f8-9276-c0b4307ac791', N'Maureen', N'Duncan', N'Lomtumopan  ', N'Prepaid Customer', N'jnvsuapk.uxgngfd@hbhqri.com', CAST(N'2009-10-16T17:33:55.670' AS DateTime), CAST(N'2002-08-26T14:47:16.170' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bfb1c6c7-3eac-3ba0-0842-c15393817101', N'Cody', N'Greer', NULL, N'Prepaid Customer', N'pswwx78@fcukjh.zewogr.com', CAST(N'2011-12-02T16:44:00.780' AS DateTime), CAST(N'1970-02-28T03:08:39.150' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'70ac03aa-d90e-c8a5-3099-c198ebd6b795', N'Ana', N'Grant', N'Uptumistor Holdings Group', N'Web', N'ylwr@ydhgm.mvwz-s.net', CAST(N'1993-04-11T18:11:37.200' AS DateTime), CAST(N'2000-11-07T13:04:46.000' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8e4a2e23-0613-9c15-62d0-c2222bc013db', N'Heather', N'Vargas', N'Partumackor Direct ', N'Service', N'rvzhswyl7@xyzqcl.org', CAST(N'1983-07-12T12:30:43.220' AS DateTime), CAST(N'1997-02-19T03:45:02.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fbde3bd9-359b-6c05-8bcc-c2e121d4dac2', N'Karrie', N'Carpenter', NULL, N'Accounting', N'zewjzbz75@vlcvbyrq.cw-nzn.org', CAST(N'1994-08-09T03:08:27.320' AS DateTime), CAST(N'2005-08-29T23:02:25.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8fa323fd-895f-cf19-c73d-c3405f74bdce', N'Joan', N'Bonilla', N'Rapvenistor  Group', N'Prepaid Customer', N'pjslfks.emtpaky@gdpxcg.bsndvj.com', CAST(N'1987-02-06T11:22:56.400' AS DateTime), CAST(N'1955-03-22T17:09:10.430' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'144545ef-3967-de48-30d5-c35c5b55ab14', N'Danielle', N'Yu', NULL, N'Accounting', N'vhkf0@z-ertw.com', CAST(N'1975-12-21T09:38:09.510' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cdee2a01-36d0-9535-b19b-c3c663b9411d', N'Ryan', N'Goodman', NULL, N'Technical', N'hrdiydaa@sigsntrtx.udqpto.org', CAST(N'2017-10-06T17:21:09.660' AS DateTime), CAST(N'2000-07-04T01:47:18.910' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'77388786-ad7c-edc6-2cc7-c3d8809cee0a', N'Troy', N'Vega', N'Monhupistor  ', N'Prepaid Customer', N'ajus.itpi@yhbo.-hslnv.net', CAST(N'1979-01-20T04:17:45.630' AS DateTime), CAST(N'2008-06-27T22:55:50.350' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'dc86f1cb-618e-8c3a-f091-c43d848d0e79', N'Laura', N'Baldwin', N'Inmunar WorldWide ', N'Technical', N'oebznuzw.svmr@vrdliw.com', CAST(N'1965-09-11T09:10:14.790' AS DateTime), CAST(N'1955-05-26T05:17:09.930' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6765726a-51e7-9428-7d98-c4550edd4e12', N'Lindsay', N'Hebert', NULL, N'Web', N'gaxtsotl82@kqinlw.org', CAST(N'1965-04-08T00:36:56.860' AS DateTime), CAST(N'1962-01-31T04:38:46.170' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'19b27868-e233-2fc2-e3c1-c48a3a6490c1', N'Trina', N'Medina', N'Repickantor WorldWide ', N'Service', N'schzapgp.wvkjiy@yysn.hpwhit.net', CAST(N'1958-01-02T16:18:50.980' AS DateTime), CAST(N'2017-12-03T12:35:17.550' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6f8a8be9-36c6-87e5-bcfb-c4bbbc493fe9', N'Karl', N'Marshall', N'Monmunin International ', N'Accounting', N'uladba@rddrwtew.rrjrfz.org', CAST(N'1978-03-05T20:10:39.220' AS DateTime), CAST(N'1964-10-03T08:44:02.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ba84b397-8cea-cefc-7cb7-c4cc1f841d2c', N'Evan', N'Reynolds', N'Grodimaquazz  ', N'Service', N'zghswt@yqfstkz.bzofdg.org', CAST(N'1966-11-06T16:03:43.730' AS DateTime), CAST(N'1981-02-18T16:27:51.590' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'071f71d1-4688-92b4-13c0-c4eb3a0c31b6', N'Kerrie', N'Ho', NULL, N'Service', N'ucgj964@jxfjlxpzp.kgl-uy.net', CAST(N'2013-08-02T08:50:19.460' AS DateTime), CAST(N'1965-06-05T18:43:23.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'89905766-8077-7bc1-f628-c51730101cbb', N'Otis', N'Holt', NULL, N'Technical', N'mplshmjk297@foly.gt-our.net', CAST(N'1964-07-18T20:41:47.840' AS DateTime), CAST(N'2006-07-05T00:24:27.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7d43326e-1224-0588-9d49-c519e274b83e', N'Joanna', N'Galvan', N'Zeetumommower  ', N'Prepaid Customer', N'hroschm.dwfb@zrdlvafl.-obpon.org', CAST(N'1956-07-15T22:58:59.900' AS DateTime), CAST(N'1977-05-17T09:11:44.390' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c6f80a74-294d-b48d-8882-c57b19701edc', N'Nicolas', N'Spence', N'Barrobor International ', N'Customer', N'ovzbthyo.xkvejl@izojee.net', CAST(N'1954-08-23T15:15:49.440' AS DateTime), CAST(N'2006-08-07T14:17:21.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'df78f358-a58f-119a-0cd6-c5a9a3dac1ca', N'Steve', N'Acosta', NULL, N'Technical', N'uoptfsj0@lgqtxkyz.ntczyk.net', CAST(N'1959-07-06T00:49:02.910' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'66a2ae7c-d3f9-80ee-081d-c5f419e71aec', N'Latisha', N'Cross', N'Tupdudefantor Direct ', N'Web', N'isex4@hbjwcs.org', CAST(N'2000-09-10T18:50:36.150' AS DateTime), CAST(N'1994-11-26T08:23:01.260' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'86790d5c-d326-d196-a067-c640e2256781', N'Elijah', N'Pena', N'Endnipedor Direct ', N'Technical', N'cady.xfiuaeibi@bqwjhp.org', CAST(N'2010-12-13T11:38:58.950' AS DateTime), CAST(N'1958-12-20T11:35:48.280' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ef2aa710-8ca3-c1e7-8884-c665bc90c657', N'Andrea', N'Merritt', NULL, N'Accounting', N'qxrlgib841@ivpip.dkrs-p.com', CAST(N'1991-11-22T07:13:53.680' AS DateTime), CAST(N'1999-12-07T10:03:34.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b8ae99fe-7445-3c14-5b59-c691e4fcd855', N'Frankie', N'Rivera', NULL, N'Accounting', N'fefv09@aduzjn.net', CAST(N'1994-11-13T06:35:40.100' AS DateTime), CAST(N'1970-08-08T01:53:44.750' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5f205b6e-f28a-8512-f924-c6d25a44e0fe', N'Caleb', N'Vance', N'Qwijubicator  Inc', N'Technical', N'oqqmohi.jyph@gg-rcl.org', CAST(N'1969-06-16T05:48:48.690' AS DateTime), CAST(N'1985-07-18T06:01:33.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cc92b8f3-ac35-3645-d442-c6f6acbf7dbc', N'Meredith', N'Hubbard', N'Tupkilopover  Inc', N'Consumer Customer', N'jdpk.rrjzaxk@dfmpya.com', CAST(N'1965-01-26T14:50:07.810' AS DateTime), CAST(N'1959-04-07T13:47:27.480' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3d77703e-bb9c-5296-234f-c72288bc0c93', N'Marvin', N'Mc Neil', NULL, N'Web', N'ottccz02@nsdpthrmy.dedbik.com', CAST(N'1972-11-28T00:53:54.710' AS DateTime), CAST(N'1981-02-14T22:45:40.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'09082743-8883-4b1c-3d1b-c7a167a3b5fe', N'Jami', N'May', NULL, N'Accessory Customer', N'mtfcw977@vqgcj.-cevcb.com', CAST(N'1983-07-22T05:15:54.860' AS DateTime), CAST(N'1996-02-23T01:19:09.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8dc486cd-ab14-26d2-17a1-c8525018b191', N'Lisa', N'Huffman', NULL, N'Marketing', N'hwbamm63@rnmjcoz.dnpwwf.com', CAST(N'2010-08-28T20:26:04.070' AS DateTime), CAST(N'1971-05-24T07:59:58.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0ca8b4b6-a9d8-3d68-5d4c-c8c1652d4ba1', N'Glenda', N'Nolan', N'Endquestplin International ', N'Web', N'toqxy7@opehfo.com', CAST(N'2002-02-21T06:18:58.560' AS DateTime), CAST(N'1967-09-04T05:22:16.350' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5924b798-b3c8-9371-e6ce-c93d457bbb87', N'Shawn', N'Murillo', N'Cipcadadazz WorldWide ', N'Marketing', N'vytdsoq59@ipxypm.org', CAST(N'2016-07-11T16:25:14.480' AS DateTime), CAST(N'1961-07-02T20:31:31.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'782747ba-5050-0b1e-9151-c93f51e2918d', N'Alisa', N'Marks', NULL, N'Prepaid Customer', N'gcbnrcp.cwgawkboos@bcxr.jlswei.org', CAST(N'2016-02-03T01:31:58.190' AS DateTime), CAST(N'1957-11-04T11:25:01.220' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'15aed802-880e-f8d3-362a-c9d0b1b42b80', N'Nick', N'Allen', N'Windimexentor Holdings ', N'Accounting', N'umyjccru.tzwcdxx@dyfrdh.com', CAST(N'1994-05-02T02:49:11.770' AS DateTime), CAST(N'1984-10-02T13:14:36.760' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'70b5f74f-d3be-9a84-b66d-c9e3ede68368', N'Betty', N'Jacobs', NULL, N'Technical', N'rsgry.wermyncyup@nb-vsy.org', CAST(N'2016-04-13T12:28:55.010' AS DateTime), CAST(N'1967-12-29T22:50:34.240' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9a310bf8-44d1-f18d-7f02-ca15b80d8d91', N'Summer', N'Torres', NULL, N'Technical', N'opvtkn.otoypq@jdsjtj.com', CAST(N'1985-09-09T03:56:13.390' AS DateTime), CAST(N'1993-06-11T23:00:48.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6a134942-7954-a383-ab8d-ca2e5245c2eb', N'Alan', N'Blevins', N'Tiptinplentor  ', N'Service', N'hshtq@jbnocz.org', CAST(N'1963-08-29T18:34:46.970' AS DateTime), CAST(N'1977-06-02T13:03:22.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'af89fbfc-6e17-ffa3-55d6-ca382e50b014', N'Stanley', N'Levine', NULL, N'Accounting', N'rzlecx79@qrvikz.mfdouh.com', CAST(N'1965-05-07T11:50:14.750' AS DateTime), CAST(N'1972-01-26T19:04:08.760' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'98a4b91e-0853-7199-726f-ca97077bea40', N'Nikki', N'Petersen', NULL, N'Technical', N'nllowus.ftqdgj@myntphr.nbcm-r.com', CAST(N'1974-03-15T23:52:13.330' AS DateTime), CAST(N'1990-08-08T19:20:16.530' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2db30fca-91ea-c818-ef80-cab7248c7fd3', N'Jamal', N'Turner', N'Tiptinplover Holdings Company', N'Sales', N'dtudnz@o-peee.com', CAST(N'1962-08-21T22:27:46.300' AS DateTime), CAST(N'1978-11-21T03:07:07.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bf1bb397-5f54-6b82-75ec-cb2e4a40d4e6', N'Raymond', N'Vazquez', NULL, N'Service', N'zmeiicu.abiykfp@etptn-.com', CAST(N'2012-06-10T04:39:18.980' AS DateTime), CAST(N'2013-07-19T10:04:53.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'33477c89-fc0c-db5f-9b6a-cb311a2286c1', N'Charlene', N'Rocha', N'Trudimover WorldWide ', N'Technical', N'syqwo9@qwimav.bapysl.net', CAST(N'1962-12-31T19:36:01.650' AS DateTime), CAST(N'1996-05-16T12:49:52.230' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'be6e7f49-7ccb-44ba-0b2d-cb8edf80f0e6', N'Wendy', N'Park', N'Cipsapar WorldWide ', N'Accounting', N'rzqgg810@slplhvb.k-qrlq.net', CAST(N'1967-12-22T11:56:08.230' AS DateTime), CAST(N'1980-09-24T05:01:19.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'97cf5855-8196-d94d-d4a8-cbfb765832c5', N'Ruby', N'Andrade', N'Indimor Direct ', N'Accounting', N'hgkscc.jugjnkw@mqwnck.com', CAST(N'1988-08-10T08:48:12.420' AS DateTime), CAST(N'2012-11-10T17:56:03.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3c0bf96a-6564-77b5-4124-cc0c6a881f1b', N'Mathew', N'Whitehead', NULL, N'Prepaid Customer', N'uuqz.qwmwmwoi@cicvxvssw.unagg-.com', CAST(N'1994-01-26T08:56:29.300' AS DateTime), CAST(N'1986-07-20T05:01:17.160' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'861e8674-ef6d-f694-208d-cc2bf12cc956', N'Franklin', N'Pollard', N'Insipeficator Direct ', N'Web', N'uzjagvsc74@edcpgk.-efg-i.net', CAST(N'1967-05-02T06:44:25.240' AS DateTime), CAST(N'1986-11-25T23:32:02.750' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bef564bd-5112-8457-9209-cc31c0d68166', N'Amie', N'Murphy', NULL, N'Service', N'xjfxiqea.ilvukpue@rk-jqu.com', CAST(N'1953-01-04T19:01:23.650' AS DateTime), CAST(N'2001-02-11T04:23:27.060' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b21d630f-9405-80d0-2037-cc7b0dbda8f8', N'Tracy', N'Melendez', N'Lomhupantor International ', N'Prepaid Customer', N'tivc.qzheucfo@jvypgc.org', CAST(N'2004-12-03T01:53:43.150' AS DateTime), CAST(N'2008-10-14T21:38:47.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5a7f0cf6-92c9-8205-38da-ccc76b669a04', N'Marci', N'House', N'Surerover Holdings Inc', N'Web', N'ipudjohm8@wuhujg.org', CAST(N'1977-06-03T02:55:11.200' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'653a1a52-73f1-69df-3ea2-ccd596ded2da', N'Christina', N'Hunter', N'Truglibedantor International Group', N'Technical', N'vgjsy05@rsgbbm.oxbfpx.com', CAST(N'1956-10-12T14:58:23.990' AS DateTime), CAST(N'2003-12-12T10:13:46.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8e377c8e-87fe-3a5c-23ef-cd28f95f95ab', N'Darin', N'Dixon', N'Cipsapover  Corp.', N'Web', N'vebyki6@ylfqetwe.yunbhc.org', CAST(N'1984-12-27T17:49:18.740' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bc1a21e9-d328-297c-4881-cd297382eb8b', N'Russell', N'Gibbs', NULL, N'Service', N'ohqknhjl@rzkwwf.net', CAST(N'1990-07-08T13:35:19.980' AS DateTime), CAST(N'2018-08-28T10:49:50.900' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'636e2c7a-1500-4042-39c1-cd5e4486822e', N'Alejandro', N'Wilkins', N'Supdimower International ', N'Accounting', N'pehw.erwozqe@oedrc-.com', CAST(N'2004-01-10T14:31:24.290' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'87ffbe87-3ce2-af34-ee4e-ce5399c59411', N'Alfredo', N'Sampson', NULL, N'Accounting', N'ddhwz742@chdulc.com', CAST(N'1969-08-07T19:38:20.280' AS DateTime), CAST(N'2013-07-27T20:49:07.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a5ea58a4-ca03-64cc-c64e-cedd4bbbdf9c', N'Desiree', N'Wiggins', NULL, N'Technical', N'eqjez@hgfvu.vptwtc.com', CAST(N'2016-06-03T06:51:01.090' AS DateTime), CAST(N'1969-09-17T18:29:13.710' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7aa8e319-f636-8964-8c77-cefab1972c4e', N'Jeannette', N'Frey', N'Redudin WorldWide Group', N'Prepaid Customer', N'uasx.rsvumfvq@-guxhs.com', CAST(N'1992-11-05T13:42:57.220' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0d7dd9df-2567-2cef-1cac-cf2ff180d259', N'Julius', N'Chen', N'Tipsipor Direct ', N'Accounting', N'ofpwrla41@-ggleo.org', CAST(N'1999-12-02T19:12:24.790' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cb536775-3c60-f0e8-ad70-cf8a3b55139a', N'Kari', N'Moyer', N'Adsapar  ', N'Prepaid Customer', N'syyvmd@miqbqg.vapkih.org', CAST(N'2015-12-26T10:24:12.570' AS DateTime), CAST(N'2003-01-05T23:34:03.260' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'29b9897b-5d05-c5a5-c81e-cf9d76e2983a', N'Virgil', N'Brooks', N'Injubegistor Holdings Inc', N'Accounting', N'ruqaz.sfgsyte@tdzeuq.net', CAST(N'1995-07-17T12:55:20.540' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b652b338-111f-a52c-de9f-cfc12469a39b', N'Lorenzo', N'Stokes', NULL, N'Prepaid Customer', N'spsdcz.szgvswag@emglqi.yruzkc.org', CAST(N'2018-05-11T01:09:20.510' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0530ad34-fd3e-4207-1996-cfea67079482', N'Norman', N'Carter', N'Unwerpopin International Company', N'Technical', N'byxjs@znuersl.kacrc-.net', CAST(N'1961-06-24T17:48:47.660' AS DateTime), CAST(N'1989-05-15T22:51:54.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8ff5dc1f-eba4-fd64-270a-d0059391b1a5', N'Jerome', N'Flynn', NULL, NULL, N'wmbrnicj32@slsbba.net', CAST(N'2002-03-29T16:09:48.380' AS DateTime), CAST(N'2012-04-11T06:43:37.210' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6fd7186b-cd15-cbd5-55ce-d00e730a4796', N'Belinda', N'Austin', NULL, N'Prepaid Customer', N'fjjjr.frjnla@psuukrwpz.twyngj.org', CAST(N'1956-06-26T20:09:04.880' AS DateTime), CAST(N'1983-08-21T06:07:45.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1909f012-2923-9324-bc5a-d07b2f8b38df', N'Melanie', N'Weiss', N'Surtanadover WorldWide ', N'Marketing', N'jtpgqgne39@ilrs.iagiuh.net', CAST(N'2004-09-04T22:51:33.550' AS DateTime), CAST(N'1990-07-10T07:19:02.150' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8da6bb36-5683-ec54-e768-d094c03bec95', N'Gwendolyn', N'Russell', NULL, N'Accounting', N'uzntbb.dnxv@qmzh.gnzoh-.net', CAST(N'2014-07-07T17:01:17.920' AS DateTime), CAST(N'2000-01-25T15:39:17.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6863c090-dabd-c27b-1f1e-d0c514a6c045', N'Ronald', N'Frank', N'Lomwerpan  ', N'Accounting', N'wfowq80@tofxsb.com', CAST(N'2016-11-30T16:40:10.070' AS DateTime), CAST(N'1999-11-11T22:03:47.950' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6821d0d4-1a2d-4e5d-b4e2-d0cd8f3d3efa', N'Owen', N'Shepherd', N'Rapvenicator WorldWide ', N'Prepaid Customer', N'znmydjv238@jwlyem.nvimza.net', CAST(N'2014-01-18T13:35:10.550' AS DateTime), CAST(N'2009-01-26T02:17:45.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'61dabec3-0a49-23bc-8f3e-d0d24dca8421', N'Desiree', N'Hurst', N'Parkilistor WorldWide ', N'Prepaid Customer', N'hnkbor.kvapz@fpztdkwcw.qpmzlv.net', CAST(N'1974-05-06T02:42:10.180' AS DateTime), CAST(N'2018-02-25T10:39:40.760' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'002378c0-fbd5-412e-e133-d15a350a22c4', N'Bobbi', N'Hayes', NULL, N'Web', N'xqlttk.urorlbm@-nplfb.com', CAST(N'1991-06-19T16:42:02.510' AS DateTime), CAST(N'1964-02-17T08:44:03.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cbf26c18-edcd-04d8-2c3e-d1abaa9edf83', N'Joy', N'Lynn', NULL, N'Accounting', N'ztxw.awejzs@wjnzuk.net', CAST(N'1970-06-28T18:01:51.720' AS DateTime), CAST(N'1954-12-20T21:28:18.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b1a74251-4cd2-46d9-bb29-d1bb2237a12b', N'Noah', N'Orr', N'Adkilinover  Inc', N'Prepaid Customer', N'lzfkujz.zyfnvd@txii.qfgi-f.org', CAST(N'1999-12-02T00:40:10.530' AS DateTime), CAST(N'2004-04-08T01:00:57.260' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'08e1094f-d225-4c1c-74e2-d1d9fd9d7939', N'Leslie', N'Hardy', N'Wintinentor Holdings Inc', N'Web', N'tletul.hwuvvxg@hmqtot.org', CAST(N'1958-10-18T19:27:33.920' AS DateTime), CAST(N'2005-04-15T21:15:25.020' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5d21f040-c17b-deb7-520b-d20b75e8d32f', N'Christie', N'Ford', N'Truhupegistor Holdings Corp.', N'Service', N'ldzyblwk470@ahuhn.xwpmrq.org', CAST(N'1999-01-18T03:16:07.000' AS DateTime), CAST(N'1958-07-11T15:19:46.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e02050af-78f0-8792-06b7-d23d146c6b75', N'Rachael', N'Andrade', NULL, N'Customer', N'vkxukxv.jzbx@edslnm.org', CAST(N'2018-06-17T15:32:03.940' AS DateTime), CAST(N'1965-04-04T10:42:33.950' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'071bed5f-bd58-5d72-3a7d-d24ede146b93', N'Constance', N'Joseph', N'Admunicator Direct ', N'Technical', N'lqcf.bpfihfwv@vx-g-f.net', CAST(N'2008-03-05T14:18:41.280' AS DateTime), CAST(N'1966-10-01T16:47:25.150' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ee2bbc35-f462-355c-b1ad-d2d70e258073', N'Gilbert', N'Parker', NULL, N'Service', N'dfsw.zyeih@oinagj.net', CAST(N'1984-03-30T21:54:28.400' AS DateTime), CAST(N'1969-05-24T02:27:15.630' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'344ffe29-bbc6-ba9b-049d-d3219949802c', N'Marvin', N'Brock', NULL, N'Web', N'yqziij51@tbqn.mkzneq.org', CAST(N'1989-09-05T23:56:12.670' AS DateTime), CAST(N'1977-07-01T18:34:22.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd2776a00-d552-c5f5-225a-d3e57b728159', N'Rebekah', N'Estes', N'Qwizapower Holdings ', N'Web', N'ahkbrh.nizrfii@pbowe.pnq-gm.org', CAST(N'1990-01-29T06:19:12.830' AS DateTime), CAST(N'1988-03-14T11:44:39.550' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4024c379-555f-6031-2b15-d47f31e13355', N'Angelo', N'Schmidt', NULL, N'Prepaid Customer', N'ahcdfjyo17@-stpeh.net', CAST(N'2017-03-20T09:53:27.040' AS DateTime), CAST(N'1969-07-20T04:24:21.820' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a11ea171-c7ac-e00d-c5c7-d50dcf49b420', N'Roxanne', N'Salazar', N'Lomhupaquicator International Group', N'Service', N'nasgdy@wdtirhw.ueyaxp.net', CAST(N'2012-07-26T09:32:11.900' AS DateTime), CAST(N'1986-03-29T16:06:32.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'69bdc257-1eea-aca8-a521-d5451348d490', N'Emily', N'Serrano', NULL, N'Accounting', N'npbxa12@xn-syx.com', CAST(N'1977-04-23T01:57:31.470' AS DateTime), CAST(N'1994-06-01T19:33:42.520' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'dfacd165-a69a-3bb4-4c8e-d55ead95fd98', N'Kerri', N'Owen', N'Zeetumex  ', N'Web', N'ihcznai.bylhd@xstael.net', CAST(N'1954-01-20T22:30:04.870' AS DateTime), CAST(N'2006-06-21T04:08:00.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'50596efb-1105-f424-6e5e-d63a1d5cdab4', N'Andrea', N'Chang', NULL, N'Prepaid Customer', N'jviuc.rzxvrg@lhop.evkaof.org', CAST(N'2013-07-18T00:48:43.510' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bd80c0d5-2936-bcc0-765a-d656fc76d307', N'Antoine', N'Schultz', NULL, N'Web', N'pmwwb72@ryxgvm.org', CAST(N'1963-10-03T19:58:33.280' AS DateTime), CAST(N'2012-06-01T02:55:32.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b9be59b3-4bb6-cf13-194f-d69c0407762c', N'Larry', N'Cortez', NULL, N'Service', N'ffoqcvw.ywptecpqzj@pzlfzabx.ffhcg-.net', CAST(N'1969-06-27T13:27:50.660' AS DateTime), CAST(N'2002-11-10T05:54:42.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3df78c6c-56fe-2486-923d-d6dc72e8cd75', N'Alisa', N'Browning', NULL, N'Prepaid Customer', N'hbnnlmd8@owewks.com', CAST(N'1984-07-20T07:22:35.480' AS DateTime), CAST(N'1975-09-13T17:28:30.770' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'981bc6c3-40d2-e40b-e20c-d7425e15a43a', N'Nicolas', N'Cordova', NULL, N'Consumer Marketing', N'xmnq.hvjkxymnr@mcdtupv.huozca.net', CAST(N'2010-09-19T18:47:47.670' AS DateTime), CAST(N'1993-04-28T13:32:07.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'202483a3-0e9a-5e84-2ed0-d7bb1b82d3f5', N'Marcie', N'Newman', N'Fropickazz WorldWide ', N'Technical', N'jkrmkbm.wmvor@aoagf.risvqr.com', CAST(N'1983-05-01T23:49:59.890' AS DateTime), CAST(N'1986-01-01T20:55:24.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd915bc8c-364f-a338-1d8a-d7c16ef7c834', N'Gregory', N'Reeves', NULL, N'Prepaid Customer', N'nacuxlsd065@rezadk.com', CAST(N'1981-05-17T23:46:14.510' AS DateTime), CAST(N'1985-10-21T06:27:29.620' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b6abbd46-3e65-7588-08c1-d80ae139cd80', N'Anitra', N'Jacobson', N'Wintumplan International ', N'Service', N'djshdik.vkmssop@w-vawr.org', CAST(N'1984-04-16T06:31:28.520' AS DateTime), CAST(N'2001-10-29T16:33:49.580' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7f75075d-c1a5-779e-5750-d8554384d775', N'Allen', N'Hurley', NULL, N'Service', N'akxwmdh27@uvwgrg.net', CAST(N'1973-06-07T22:03:38.970' AS DateTime), CAST(N'1961-05-14T02:04:35.260' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'cfbac6fe-9446-0590-39c0-d894b7dddb00', N'Jeannie', N'Gomez', N'Reerower  ', N'Service', N'medb.uapuwbnjv@pppdtb.org', CAST(N'1953-02-18T06:51:35.810' AS DateTime), CAST(N'1994-12-29T19:25:36.280' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9e632021-e1b0-f255-4fb6-d8bdecc0eda5', N'Brett', N'Riggs', N'Dopglibopin WorldWide Group', N'Accounting', N'iazxsm.oqrcni@zwbt.yezkvo.net', CAST(N'1994-04-22T14:19:53.360' AS DateTime), CAST(N'2000-06-06T13:15:43.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'896878f3-2c7b-cce3-387f-d8fdcc06b5c3', N'Gary', N'Finley', NULL, N'Service', N'vqngnnfk3@yfaxaz.org', CAST(N'1982-12-24T11:21:20.200' AS DateTime), CAST(N'2013-09-13T02:26:03.610' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6a793f2c-127d-dbfc-9875-d95327aeeb73', N'Sonya', N'Levy', N'Dopcadan WorldWide ', N'Technical', N'abqltrp.mahr@gdnhnw.com', CAST(N'1962-03-24T23:48:43.460' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'67fa10ae-9c6d-28ab-ca67-d9cbf16a7d58', N'Derrick', N'Andrade', N'Empebanower  ', N'Prepaid Customer', N'zhlqcp0@riiisll.s-znhc.net', CAST(N'1954-08-05T12:18:08.210' AS DateTime), CAST(N'1983-03-03T03:23:52.870' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'635a889f-3f68-e52c-cfcf-da573b1bd9e9', N'Alice', N'Dunn', NULL, N'International Sales', N'wviar@unjtrx.net', CAST(N'1970-09-13T12:32:07.780' AS DateTime), CAST(N'1993-07-01T17:42:50.440' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5b72ecdf-d295-fe12-fd23-dab283590ddd', N'Danielle', N'Boyer', N'Enddudinan Direct ', N'Business Customer', N'weezkz93@ifhmaa.com', CAST(N'2008-08-16T13:42:39.130' AS DateTime), CAST(N'1959-10-28T03:53:33.080' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ad9db893-4388-1845-5cc1-dab2d6600f95', N'Allan', N'Ferrell', N'Emmunar  Inc', N'Web', N'jpeserx906@qvkco.zapvlq.net', CAST(N'1987-07-23T01:08:00.970' AS DateTime), CAST(N'2010-04-09T10:14:39.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'168ac464-d503-88b8-8492-db391d28c6d5', N'Kareem', N'Benton', N'Montinedin  ', N'Service', N'hohdiw467@wqcnye.com', CAST(N'1957-05-18T21:03:38.590' AS DateTime), CAST(N'1981-10-13T19:52:49.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6304109a-dddd-49f7-4d1c-db40ee1ce5b5', N'Albert', N'Obrien', NULL, N'Service', N'ifnwlatp.hfyxz@kccb-w.org', CAST(N'2003-11-26T20:47:40.230' AS DateTime), CAST(N'1966-04-14T03:05:50.920' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8ebbe0d1-5ad1-0023-5e8b-db5fb1582b0e', N'Mitchell', N'Velasquez', NULL, N'Technical Marketing', N'ewpisxb.skhzhk@yjgazb.net', CAST(N'2000-01-28T03:54:47.000' AS DateTime), CAST(N'1961-07-28T22:28:56.040' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0e884b8c-93a6-2beb-2d30-db7347381e77', N'Kimberly', N'Wheeler', NULL, N'Web', N'hksiji.zcpjzbumd@uvzzlr.net', CAST(N'1962-09-20T13:27:40.100' AS DateTime), CAST(N'1995-05-05T22:34:25.990' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9b1e4850-65ab-bb70-791e-db74f80bb49b', N'Latisha', N'Cherry', NULL, N'Technical', N'jpvjr.itrfapygo@gqohpd.net', CAST(N'2015-04-26T08:32:13.850' AS DateTime), CAST(N'1973-08-19T18:53:11.860' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9b870adb-4abb-2276-75cd-dc589f7ae762', N'Tonia', N'Hogan', NULL, N'Corporate Marketing', N'eajjxq.sklwz@vucif.gpttza.org', CAST(N'1975-04-30T13:04:57.090' AS DateTime), CAST(N'2009-06-16T10:09:18.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'eac65501-21f5-f831-fb07-dcfead50d1d9', N'Sandy', N'Mc Gee', NULL, N'Technical', N'dhkckfxk1@-ljicj.com', CAST(N'2010-07-02T14:50:15.250' AS DateTime), CAST(N'1963-11-14T11:22:34.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c4247216-f843-b362-ec6b-dd42ee22281f', N'Frances', N'Gregory', NULL, N'Prepaid Customer', N'vjeax.lhkwj@abxmwu.com', CAST(N'1966-02-23T15:37:47.040' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'05821fc0-8082-0eb6-ac61-dd53c985d48a', N'Lisa', N'Bentley', N'Barpebackor  ', N'Prepaid Customer', N'sprdk.dchooppqkl@rqyhtp.net', CAST(N'1954-11-20T13:32:55.310' AS DateTime), CAST(N'1984-12-10T12:33:33.580' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'09be0827-264f-e2e8-3a04-dddd9e0e492d', N'Zachary', N'Beasley', NULL, N'Technical', N'ofjxrtjh.dlnru@gtrtofdmy.agknzr.net', CAST(N'1962-11-18T22:01:34.930' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'21fc5cc9-b5db-2fd5-870c-de8b19c15690', N'Elaine', N'Garza', N'Zeemunupar Direct ', N'Web', N'ntsygi296@rombbe.com', CAST(N'1955-09-29T16:44:18.920' AS DateTime), CAST(N'1984-01-04T05:03:58.290' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0dacfdba-38ea-33bb-6def-de99227bbeef', N'Bobbi', N'Haney', N'Parrobin WorldWide ', NULL, N'mvwnutrh72@oyzvnntgh.mjnyy-.net', CAST(N'2001-06-09T06:55:51.290' AS DateTime), CAST(N'2017-07-31T15:06:48.890' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e761bbbd-9f1b-0975-5289-de9cefa45e9a', N'Manuel', N'Cherry', N'Cipbanentor Holdings Group', N'Web', N'rvfnoeic@eadvcf.org', CAST(N'1976-08-19T03:46:32.290' AS DateTime), CAST(N'1986-02-06T01:41:50.970' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'930d7095-2a19-1b7c-4a59-ded0df47f73a', N'Sam', N'Austin', N'Trudimor International Group', N'Service', N'ajind.zyyguye@zhdngmxxj.jlynes.org', CAST(N'1975-09-11T19:50:58.220' AS DateTime), CAST(N'1999-09-11T18:28:56.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c62f0a66-2f74-2019-b20b-def8171ce234', N'Erik', N'Petty', N'Lomquestefar  Corp.', N'Web', N'kivygu370@movggu.org', CAST(N'1983-05-10T06:42:56.090' AS DateTime), CAST(N'2001-08-12T18:16:28.250' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fa49d6cc-e645-94dd-0f51-df324763e66e', N'Dana', N'Sweeney', N'Rapdudupistor International ', N'Accounting', N'xojy167@wwef.l-bsbl.net', CAST(N'2018-11-10T05:37:53.920' AS DateTime), CAST(N'1984-12-12T05:00:38.800' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b36ec6ff-be65-6803-f627-df67e39aefe8', N'Kirsten', N'Brennan', N'Thrubanedgower  ', N'Web', N'wwpvsduw397@mzuuvy.com', CAST(N'1967-09-28T08:12:14.660' AS DateTime), CAST(N'2014-04-09T11:20:22.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'90a59def-5aca-6754-f4e5-df6fcd29851b', N'Kristy', N'Estrada', N'Retinicator WorldWide Company', N'Web', N'tzyrczsq.hkalcgs@uwlvzu.org', CAST(N'1972-05-02T12:32:52.280' AS DateTime), CAST(N'1988-03-13T07:10:27.260' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c9f1d106-906f-dbe3-55b1-df7f019b13e3', N'Christi', N'Berger', N'Surglibplistor  ', N'Service', N'xbdh.syiotwev@wxpkre.org', CAST(N'1963-12-28T10:42:17.800' AS DateTime), CAST(N'1994-03-26T03:09:39.290' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ea4490c4-3f26-f98d-82da-dfdee1474e11', N'Bret', N'Ross', N'Remunentor Direct ', N'Accounting', N'yesoowrx702@rytwwclta.uqpujk.org', CAST(N'1977-01-18T07:04:12.440' AS DateTime), CAST(N'1986-04-20T20:31:09.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bfae84a0-2ed2-a0a8-3508-e06dbce49859', N'Latonya', N'Lee', N'Endjubepar WorldWide ', N'Prepaid Customer', N'phbcjt@ltwfiny.mfrrti.com', CAST(N'1977-07-19T23:34:12.520' AS DateTime), CAST(N'1955-06-16T20:25:16.180' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'417e671b-f0c7-4626-b9fd-e086c4ec3e1c', N'Alissa', N'Myers', N'Barweran WorldWide Company', N'Sales', N'qxtdf973@ovvknd.org', CAST(N'1985-07-21T01:37:37.290' AS DateTime), CAST(N'1993-10-23T06:59:38.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2ee15dff-aff7-c47c-ad62-e0bed3a02b17', N'Jessica', N'Greene', NULL, N'Accessory Marketing', N'owgxhxm7@tktzut.net', CAST(N'1960-11-30T07:28:17.980' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'0c2f13ff-bfc7-e90a-70ef-e0ee00c7ce61', N'Kristie', N'Cline', N'Hapdudexantor  Company', N'Accounting', N'ordbz.cueo@ecbjkvq.znrnca.com', CAST(N'2007-07-10T18:20:22.590' AS DateTime), CAST(N'2005-11-19T14:36:19.760' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5aff0ac2-87a7-b11a-fcbb-e0f36f3373a6', N'Dennis', N'Frank', N'Renipedin  ', N'Prepaid Customer', N'mjkcq.dhaqzjt@dzdvf.ccvjlp.com', CAST(N'1989-11-26T18:06:29.440' AS DateTime), CAST(N'2013-09-19T00:17:07.040' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'17228aa5-31db-a1a8-df86-e0ff2c33070f', N'Randi', N'Zamora', N'Inpickamax  Company', N'Accounting', N'itlumk.lgxtrdkcu@vhomqa.org', CAST(N'1969-01-31T04:07:03.880' AS DateTime), CAST(N'1958-07-31T14:34:39.870' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'50000b81-9fee-3179-fc04-e113e6d22d36', N'Xavier', N'Mosley', N'Trufropinar International ', N'Web', N'zukm.rfqe@ahiuro.com', CAST(N'1978-10-15T07:06:59.690' AS DateTime), CAST(N'1960-02-23T00:13:13.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'814cc60a-4ca9-879f-1e33-e1a70d68cb73', N'Shelly', N'Hendricks', NULL, N'Accounting', N'gysteqi923@qxxqti.yui-xm.net', CAST(N'1984-06-15T11:22:08.470' AS DateTime), CAST(N'1995-05-30T03:52:51.120' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b31a758e-e0c6-7886-5394-e1d4060f3514', N'Billy', N'Dunlap', N'Qwisapex  ', N'Technical', N'brwopfg@mzzhdx.net', CAST(N'1974-07-04T18:18:20.580' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8803a231-7eac-d3ed-a79d-e1e72d8580e3', N'Jeanette', N'Massey', NULL, N'Service', N'snlwnsmp.ahinlbitps@oqvpiv.org', CAST(N'2015-03-21T21:47:46.440' AS DateTime), CAST(N'1987-12-16T19:48:03.240' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'09f4e4d3-ffe6-d54d-0d8c-e22a540f49b7', N'Melissa', N'Lindsey', N'Varvenistor WorldWide ', N'Technical', N'oklrlpy.pophgnpti@yvvt.jphzcu.com', CAST(N'2013-11-30T11:56:22.410' AS DateTime), CAST(N'1992-12-09T18:45:43.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f88538e5-70f0-f14b-2dbb-e24c31eb2cd6', N'Patrice', N'Shaw', N'Qwidimantor Direct Group', N'Accounting', N'xahv.boyvsgo@-ttmya.net', CAST(N'1986-01-17T06:59:48.730' AS DateTime), CAST(N'1964-03-26T07:50:39.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9a99c2cd-fcd3-bbd3-7893-e2bd95c4a0d9', N'Damion', N'Spencer', N'Intumeficator WorldWide Inc', N'Web', N'ioskse12@lznktw.com', CAST(N'1990-04-24T22:59:21.990' AS DateTime), CAST(N'2002-07-29T12:58:23.680' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'72a6a799-f38d-bda3-0499-e369647b8785', N'Kerrie', N'David', N'Dopdudin  ', N'Accounting', N'kfvkkjf98@ebpyda.org', CAST(N'1962-12-13T11:53:28.320' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'151d188c-6c29-5592-49cc-e3a5c83c5f21', N'Amanda', N'Bentley', NULL, N'Technical', N'xkdq19@deeuid.org', CAST(N'1965-06-21T01:54:55.810' AS DateTime), CAST(N'2018-03-31T18:25:05.210' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a3e7ad26-e36b-85ac-3a9c-e3c5eb0a4edb', N'Lorie', N'Hester', N'Hapglibopex  ', N'Service', N'mxyoqbo85@nutqqnd.zgjjfy.com', CAST(N'1990-01-15T06:15:15.330' AS DateTime), CAST(N'1987-03-19T09:57:01.220' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'eeb6b60a-42df-ef61-a2b5-e401ab694ae1', N'Elisa', N'Crane', NULL, N'Service', N'rbizkzge07@lyqouqt.wlwxeh.net', CAST(N'2003-09-09T09:28:00.600' AS DateTime), CAST(N'1978-07-20T05:56:14.620' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'eaa17312-9834-32b7-ecf2-e448a70d0825', N'Virginia', N'Lewis', N'Tuptanollover  Company', N'Accounting', N'dgqfr.pbamx@zdkfhg.org', CAST(N'1994-12-12T21:56:43.390' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c82c5122-d676-13ee-520d-e4f2d2e683b9', N'Angelica', N'Zimmerman', NULL, N'Service', N'hyuijm.cfmc@qctzqq.net', CAST(N'1988-08-27T00:33:46.330' AS DateTime), CAST(N'1991-01-20T15:13:41.780' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fe823627-391e-cac1-3379-e50b91727860', N'Jordan', N'Hall', NULL, N'Consumer Customer', N'zbesylay.fwseygl@mnpyy.fricel.org', CAST(N'1981-10-15T17:58:00.670' AS DateTime), CAST(N'1962-12-07T23:53:53.510' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a155709e-9aec-1e48-2059-e5fecbc361b1', N'Irma', N'Durham', NULL, N'Accounting', N'zxbp73@hrglo.nmwrcz.net', CAST(N'1961-04-02T14:01:27.080' AS DateTime), CAST(N'1980-06-13T19:56:15.990' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a15e4246-d3f0-5894-e18b-e6439ae103ed', N'Elijah', N'Briggs', NULL, N'Service', N'zlrzqhh4@oanvil.net', CAST(N'1972-11-27T15:00:35.540' AS DateTime), CAST(N'2003-04-11T21:50:33.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'da9d5a58-1dc3-10ab-b22c-e68bc624678c', N'Angelique', N'Faulkner', NULL, N'Technical', N'hlanr.dqrsnbgb@nvrqmn.net', CAST(N'1999-05-21T20:14:11.270' AS DateTime), CAST(N'1995-10-12T04:33:47.200' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7f2123cc-1f35-ae90-4aa9-e69d575a2acf', N'Tyrone', N'Booker', N'Tipsapor Direct Corp.', N'Technical', N'xphwnibw.uypikjwlzt@kocm.oofd-h.com', CAST(N'1984-12-21T07:27:52.410' AS DateTime), CAST(N'1987-10-27T12:57:14.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2244c8ff-e3aa-596f-7fd1-e6ef32c38e5e', N'Kerri', N'Downs', N'Untumar Direct Group', N'Accessory Customer', N'wkncp548@xiaqudn.kmzsiy.com', CAST(N'2003-11-27T22:35:58.290' AS DateTime), CAST(N'2016-10-19T20:17:55.800' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8a010591-3667-0dc8-af28-e72ee190f280', N'Alejandro', N'Long', NULL, N'Prepaid Customer', N'jdiyiqf.jkdtndebgi@qbrlmq.ceifhs.org', CAST(N'1958-07-09T00:26:22.650' AS DateTime), CAST(N'1999-02-21T14:49:27.850' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6920d81b-bb6e-e813-585b-e82085df04e6', N'Katina', N'Mc Daniel', N'Hapdiminan Holdings Inc', N'Prepaid Customer', N'syra.ekmgptrgmd@udghnz.com', CAST(N'1992-01-09T12:02:46.620' AS DateTime), CAST(N'2004-06-18T04:47:42.600' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'04842085-eb7d-6a6f-a0a7-e8c79160dc8e', N'Esmeralda', N'Lozano', N'Trufropedex  Corp.', N'Web', N'vqts.yfrcjzuqw@potrgswb.hqqr-s.net', CAST(N'1959-05-19T20:48:31.930' AS DateTime), CAST(N'1990-12-20T07:06:07.540' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'92aa84a7-72e3-c736-ff11-e8ccbc35ad91', N'Nina', N'Kemp', N'Endvenepor Direct Corp.', N'Technical', N'jjpgfn0@towktigr.lijlxl.net', CAST(N'1955-12-05T03:41:36.960' AS DateTime), CAST(N'1986-06-29T21:35:28.020' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b57924c4-8263-93e2-30d4-e8dda337e44e', N'Norman', N'Collier', NULL, N'Web', N'gtgx.qhiwjbz@mrupcjcqm.laug--.com', CAST(N'1965-10-12T15:46:09.180' AS DateTime), CAST(N'2008-05-16T11:49:57.000' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'eea36fa9-f6c1-c4b0-29bf-e9070eefa94c', N'Ian', N'Walters', N'Kliquestplan Direct ', N'Service', N'deueraxu.tgeajrfd@rfqbpkjv.kcfgmq.org', CAST(N'1965-06-14T05:55:20.960' AS DateTime), CAST(N'1998-09-27T05:51:22.700' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c67dfcd6-5bab-90f0-362d-e924399e4e56', N'Adrienne', N'Nguyen', N'Rapsapex Direct ', N'Prepaid Customer', N'ssoqlh@hpbviq.net', CAST(N'1969-12-11T05:15:24.520' AS DateTime), CAST(N'1969-12-15T01:03:14.470' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'22e7fa42-f06a-81cd-0825-e93ae8af4e8e', N'Jack', N'Howe', N'Surkilepentor  Group', N'Technical', N'ocfuoqj.sgltkrstwe@ufxakz.zkot-p.org', CAST(N'1989-10-09T05:13:23.610' AS DateTime), CAST(N'1992-01-17T13:59:54.150' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'351b3d1b-8f9b-da3e-c825-e9594b1c8485', N'Rosa', N'Compton', NULL, N'Service', N'mdypxjbz.fzujqne@mhnal.dkpqwd.net', CAST(N'1970-10-06T12:34:43.330' AS DateTime), CAST(N'2006-11-13T21:18:15.060' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bac69715-30ff-ac65-5347-e97f13ddd801', N'Vanessa', N'Collier', N'Endsipilar Direct ', N'Accounting', N'kaudnhhu.usraht@vcodvt.net', CAST(N'2009-07-10T11:43:52.060' AS DateTime), CAST(N'1966-05-23T19:11:34.580' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'70982e04-d4ec-f7c8-4f07-e98307dbe822', N'Spencer', N'Clayton', N'Thrudiminover Holdings ', N'Corporate Marketing', N'sikh@wk-ujv.com', CAST(N'1988-11-23T04:56:22.460' AS DateTime), CAST(N'2008-03-03T16:58:32.260' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f3b50ba6-84ed-8c6e-1270-e99e78ecf575', N'Amelia', N'Daniels', N'Emeramar Holdings Company', N'Web', N'feqpkffp.tpne@dgfmac.com', CAST(N'1997-01-28T09:14:06.500' AS DateTime), CAST(N'1978-04-20T17:58:21.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd4486f0e-44f1-2be0-f710-e9ac60921a82', N'Thomas', N'Fitzgerald', N'Thruhupicator  ', N'Corporate Sales', N'ljriy.chvfrq@-nuenq.com', CAST(N'1966-12-10T20:13:06.540' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3a20d9e8-1def-8028-d9a9-ea01fedf1722', N'Jerry', N'James', N'Rapwerpin International ', N'Prepaid Customer', N'vyhcu.laedm@lvm-dd.org', CAST(N'2008-03-31T09:25:06.660' AS DateTime), CAST(N'2016-03-16T03:17:21.090' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9d060f79-aa8b-1ac8-4d39-ea6ae307b172', N'Marcus', N'Pruitt', NULL, N'Technical', N'djts5@afopgk.org', CAST(N'1983-08-01T10:50:40.580' AS DateTime), CAST(N'1994-01-26T01:00:04.840' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'01ef76ea-77e0-9f65-3a4b-eb20b554cd61', N'Gabriela', N'Reed', N'Dopmunover  ', N'Technical', N'dqxal.fryt@cbkvwq.realxv.com', CAST(N'1988-10-02T07:07:04.960' AS DateTime), CAST(N'1999-04-13T01:46:23.160' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'25eca171-6cdd-a8f3-53f4-eb2ce66e7b3a', N'Abigail', N'Edwards', NULL, N'Accounting', N'qqvvchq.zahtku@inpjbx.org', CAST(N'1990-08-01T10:11:51.570' AS DateTime), CAST(N'1954-03-14T09:32:46.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5c6ea494-22da-97ee-8638-eb46f05d5ae9', N'Sherri', N'Beck', N'Lomzapinin  ', N'Consumer Customer', N'ltdutv.prxqvquiq@bacwxaml.saadch.com', CAST(N'1975-04-20T07:27:45.900' AS DateTime), CAST(N'1988-02-23T17:47:22.730' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ff615e84-d554-bd9c-21d8-eb8b0f28535d', N'Rafael', N'Briggs', N'Cipkilan  ', N'Accounting', N'tgjhzsj.baoyx@ynconfx.ezfklg.com', CAST(N'1975-05-22T10:39:59.860' AS DateTime), CAST(N'1959-11-18T14:07:58.680' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'81d751a4-0d22-2b80-8c87-ebbd8890c3db', N'Walter', N'Hughes', N'Barrobexan  ', N'Web', N'nqhz@tmgdz.qwjtia.com', CAST(N'1985-07-25T02:49:43.770' AS DateTime), CAST(N'1981-01-05T23:10:36.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'848236e8-81d0-79a8-3f95-ebe09374f5d9', N'Tera', N'Tucker', NULL, N'Technical Marketing', N'hhfvghft.hvtzrt@eyikeney.mb-bdi.org', CAST(N'1978-12-19T20:34:57.150' AS DateTime), CAST(N'2002-06-04T10:12:08.810' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'172b78de-4b53-c847-8547-ebe193a31622', N'Brock', N'Moon', N'Adtanaquex WorldWide Group', N'Prepaid Customer', N'sdmcg82@jofzxi.zxkgvd.com', CAST(N'1975-09-18T13:41:36.820' AS DateTime), CAST(N'1982-10-23T19:42:13.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7d718b4b-594b-883d-30bb-ec61f1f8b338', N'Kristie', N'Serrano', N'Addudepazz Holdings ', N'Web', N'sonhiada.snkow@avove.uxnghz.org', CAST(N'1974-05-29T02:12:41.610' AS DateTime), CAST(N'1956-02-17T20:17:41.680' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5b7937a7-3420-4faf-0477-ec751bbd2ed1', N'Thomas', N'Velasquez', N'Hapdudefower Holdings ', N'Service', N'yyomdpdc516@nhokuk.com', CAST(N'1953-09-27T06:06:59.510' AS DateTime), CAST(N'2002-07-21T16:25:36.550' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'dd4f8672-cbc3-e94b-0c13-ed6f19b936a4', N'Kari', N'Cole', NULL, N'Marketing', N'fpsdct@w-spaw.net', CAST(N'1981-04-23T01:52:47.280' AS DateTime), CAST(N'2008-02-13T06:07:11.160' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5ad0251f-5735-a29c-90b1-ed841134cf8f', N'Julie', N'Rios', N'Lommunower Holdings Group', N'Service', N'urbbr5@qhevgkj.yqgjpq.org', CAST(N'1977-06-10T15:24:57.200' AS DateTime), CAST(N'1965-01-21T20:19:37.210' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ac0d5096-14c3-5e12-f28b-ede6bd394d36', N'Justin', N'Cannon', NULL, N'Prepaid Customer', N'hvbqhn.ajyxlm@qmodfg.com', CAST(N'1961-08-20T22:55:39.530' AS DateTime), CAST(N'1971-09-09T23:21:18.950' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e6e1291a-bc74-1c08-a4c8-ee0b8b884104', N'Rodolfo', N'Collier', N'Tuptanistor International ', N'Technical', N'oxvvevnz.bprdciwyo@frjrig.net', CAST(N'2016-04-22T14:30:10.110' AS DateTime), CAST(N'1961-04-01T18:46:05.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a257bbc9-d541-e656-a90b-ee252b075f3f', N'Brent', N'Koch', N'Emdimamicator  Corp.', N'Technical', N'vurs5@--bihw.org', CAST(N'1978-01-12T18:28:15.950' AS DateTime), CAST(N'1959-12-27T05:52:17.060' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e8a7bb82-487a-d68e-fe9d-ee2e031d97b3', N'Yvette', N'Maddox', NULL, N'Accessory Marketing', N'tkzn4@mqfqyt.com', CAST(N'1958-02-14T17:32:19.730' AS DateTime), CAST(N'1982-08-04T05:02:24.280' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3409a2b2-48c9-5535-4129-ee5a5c3117bc', N'Luis', N'Sosa', NULL, N'Technical', N'fhweczjz.nokj@wbfrpe.net', CAST(N'1965-04-11T10:27:59.120' AS DateTime), CAST(N'2001-03-19T04:30:50.270' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ee32b566-9c01-c46d-de67-ee88921bd40a', N'Ericka', N'Hayden', NULL, N'Prepaid Customer', N'txxzhj2@esrmac.net', CAST(N'1961-01-30T21:43:47.580' AS DateTime), CAST(N'2004-03-27T12:21:14.710' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9e7faeb6-8c63-5ca8-4948-eed4d49afeb1', N'Derick', N'Burns', N'Hapfropantor Direct ', N'Service', N'aemfrj.tizf@anrmswxn.rnnsbj.net', CAST(N'1996-07-23T02:05:48.690' AS DateTime), CAST(N'2013-06-28T06:20:47.920' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'379e6f27-879b-ecf3-ac57-ef10dee1fa7f', N'Todd', N'Stevenson', NULL, N'Web', N'hnurcgu6@raveyd.gfeoms.org', CAST(N'2015-06-20T05:02:41.380' AS DateTime), CAST(N'1963-11-11T12:43:47.880' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'047dc926-9ceb-03e7-e981-f032f0c6acfd', N'Gordon', N'Mccoy', N'Empickover  Group', N'Accounting', N'qlbjz7@zoitwg.org', CAST(N'1995-08-12T15:15:44.610' AS DateTime), CAST(N'2010-04-27T15:17:49.430' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1fd858ed-aadc-8821-b6a8-f04d2ae43181', N'Jane', N'Durham', NULL, N'Prepaid Customer', N'mextuo.hwnikhpmek@ihotxqsm.oiwpxh.net', CAST(N'1974-03-07T01:07:30.260' AS DateTime), CAST(N'1989-04-06T15:32:07.640' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a1753d40-362d-eee8-1339-f0952e1c1f91', N'Amie', N'Madden', NULL, N'Accounting', N'lrbo.icgqfh@tunv.xfvpoc.net', CAST(N'1966-12-05T11:28:26.790' AS DateTime), CAST(N'1962-01-24T12:03:24.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1de1cb13-289e-b3d7-8c88-f1918a7d14f9', N'Dale', N'Archer', NULL, N'Web', N'rtobqeks40@oivocc.ucswrz.net', CAST(N'1962-10-15T05:03:49.790' AS DateTime), CAST(N'1964-03-11T23:53:04.840' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bfc01f47-da94-95f2-58a0-f1b5c182cf3f', N'Raymond', N'Wilkins', NULL, N'Accounting', N'tdwcsv13@zrehqtm.yk-qsm.com', CAST(N'1961-03-31T16:02:26.610' AS DateTime), CAST(N'2008-10-31T18:00:11.390' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3f958b87-ee2b-1ebc-736b-f1bd2cc70e18', N'Felipe', N'Roy', NULL, N'Prepaid Customer', N'lvfgxefc.quhgqhct@qkrxdx.lipvvo.org', CAST(N'1997-01-28T05:19:37.330' AS DateTime), CAST(N'1959-02-14T02:10:50.720' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'8e0089a3-0bd5-ea81-6f5e-f201ab99c5a1', N'Irene', N'Velazquez', NULL, N'Web', N'xlwzl1@zwanrg.net', CAST(N'1963-01-06T07:28:49.140' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'5bbd9fad-142b-9585-a41b-f24a660e82e9', N'Jeanne', N'Gordon', N'Qwiquestantor Holdings ', N'Accounting', N'rfik4@imxlku.org', CAST(N'1999-12-24T01:41:25.280' AS DateTime), CAST(N'1997-07-22T22:54:29.340' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a9afef7f-c380-b1d5-b3af-f26ada1ce633', N'Chasity', N'Stein', N'Endquestimar Holdings Company', N'Web', N'vnex901@dbcfxp.df-aoz.com', CAST(N'2003-03-11T08:38:08.150' AS DateTime), CAST(N'2009-11-16T04:37:23.220' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'67d4c44c-1ffe-11de-890f-f2750c6a056d', N'Darius', N'Hays', NULL, N'Service', N'miezfb@wwykqr.-vbxsy.net', CAST(N'1984-11-27T18:43:33.130' AS DateTime), CAST(N'1990-04-15T15:18:18.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'543177ea-f055-fd29-2635-f2850ee60f71', N'Drew', N'Rivera', N'Hapdudefantor Direct Company', N'Web', N'fxlvphk.bzslyeyvk@macu.oizrfq.org', CAST(N'1956-09-21T19:44:15.410' AS DateTime), CAST(N'1969-12-20T06:03:01.130' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ae515959-b9a4-e134-ae80-f30307ae568c', N'Cody', N'Powers', N'Resipex  ', N'Accounting', N'zaquzv.fvirt@hqylpp.net', CAST(N'1979-10-01T11:50:23.080' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c60e0e0a-3ae7-600e-a211-f33ae5ec7022', N'Mandy', N'Cline', N'Monzapicator Holdings ', N'Business Customer', N'tshokf@arkgtn.net', CAST(N'1963-06-28T23:49:13.620' AS DateTime), CAST(N'1975-11-27T03:22:00.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ee430c1b-18e2-22d5-8ad6-f3b9e60a0465', N'Valerie', N'Hood', N'Growerax Direct ', NULL, N'sljjvyx00@xiewgvbok.ivcgmc.net', CAST(N'1958-03-10T08:11:02.420' AS DateTime), CAST(N'1986-03-02T11:12:34.020' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'c783b971-f06e-a283-111f-f409ca8561b4', N'Micah', N'Harrison', NULL, N'Service', N'zwfhrckn7@pzafpjg.litclb.com', CAST(N'1996-11-23T16:45:09.960' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4e33972d-d9eb-5415-e8a6-f43ee6c64d87', N'Sabrina', N'Brown', N'Adsapegar  ', N'Service', N'vpzdb16@prspogvgh.oxwqux.org', CAST(N'2009-08-25T11:04:53.620' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd2b1cef6-10ce-303d-31f1-f4733084bdf3', N'Terrance', N'Bentley', NULL, N'Web', N'wnnatzix8@jafrl.wnmehk.org', CAST(N'1972-11-16T19:55:19.180' AS DateTime), CAST(N'1976-10-20T21:46:51.380' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2a5ac717-3e6b-42ae-2305-f4c7edfad7d4', N'Sheri', N'Boyle', N'Tiptinantor International ', N'Accounting', N'pdvp.bkrgqhkfaq@egefnq.net', CAST(N'1957-02-08T22:12:02.380' AS DateTime), CAST(N'1991-10-17T17:27:25.120' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ff3f3678-5888-505a-6b08-f54fb1dbd17c', N'Josephine', N'Mooney', NULL, N'Service', N'npqv@rrujdyr.ftcswx.org', CAST(N'1991-02-27T12:25:59.470' AS DateTime), CAST(N'1978-07-08T01:16:42.230' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd60fab43-03e9-f668-05bf-f5cb5ac554bb', N'Bryon', N'Grimes', N'Lomglibefover  ', N'Service', N'gkfrau82@zhphgu.cnjwes.org', CAST(N'1963-01-06T01:46:43.870' AS DateTime), CAST(N'1960-07-20T04:49:18.880' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'bfe181d8-475e-61eb-63d0-f5cfe5dd0082', N'Belinda', N'Fowler', N'Parpebex International Inc', N'Accounting', N'gdjqwd.assns@qjmqzdz.uroafj.net', CAST(N'1967-02-17T02:48:31.140' AS DateTime), CAST(N'1965-08-01T02:41:33.150' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'028534bb-6854-ede9-001d-f5d5d18e33b8', N'Ross', N'Mc Daniel', N'Endtinan Direct ', N'Service', N'trwjwebm424@adylh.sfgkqm.org', CAST(N'1979-01-05T10:16:17.040' AS DateTime), CAST(N'1971-04-14T19:36:17.000' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6c2e1e75-59b9-8ff3-48b4-f5fd63854781', N'Stefanie', N'Perez', N'Klitinicin WorldWide ', N'Technical', N'obap0@yrnhim.net', CAST(N'2014-07-13T11:20:46.410' AS DateTime), CAST(N'2000-01-22T14:16:01.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a4074fb6-b8a0-0bd6-2b9c-f62fc9a05a0d', N'Tasha', N'Richardson', NULL, N'Corporate Sales', N'gbdhw@popzpj.net', CAST(N'1998-12-15T18:08:27.400' AS DateTime), CAST(N'2005-08-01T16:14:19.230' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'374cab85-f97c-41f4-4a12-f685105d8adc', N'Julius', N'Turner', NULL, N'Web', N'qoayar14@dgffid.com', CAST(N'1981-11-26T08:02:17.650' AS DateTime), CAST(N'2005-11-04T21:00:59.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e54c0cc8-9a41-9a1d-c1d3-f6897d0bd842', N'Roy', N'Costa', NULL, N'Accounting', N'kwmxq.gmrjtvptu@grvxsra.bzc-x-.net', CAST(N'1977-07-15T04:22:25.960' AS DateTime), CAST(N'1974-09-01T07:54:54.050' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'81845987-0e1d-b2ec-8bab-f71113e42ce9', N'Greg', N'Mcgrath', N'Fropickicator International ', N'Web', N'rjpo00@nnripp.org', CAST(N'1957-12-26T00:26:04.040' AS DateTime), CAST(N'1961-10-03T15:27:35.370' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'fdaa855b-7e5a-8451-a747-f796c5526bf9', N'Lesley', N'Lawson', N'Lommunplicator International Group', N'Prepaid Customer', N'xejxhu.wbdmw@cjygmuuti.pmnebv.net', CAST(N'1975-02-18T10:16:05.650' AS DateTime), CAST(N'1967-11-13T11:24:55.670' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'567666f0-b9c2-a662-49ba-f85f3cec32a7', N'Ray', N'Nixon', N'Wintinex International Group', N'Sales', N'zizgjx.pasjbhelq@tyzeoh.org', CAST(N'1968-10-08T20:51:04.280' AS DateTime), CAST(N'2017-05-22T11:55:41.310' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'236356cf-1baf-d81f-8e64-f873cb6ef016', N'Tamara', N'Farrell', N'Tipglibopor WorldWide Inc', N'Accounting', N'mcak.redcotf@scc-mj.org', CAST(N'1986-03-07T06:35:08.410' AS DateTime), CAST(N'1958-07-11T07:35:57.120' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'79e47169-5a21-b8bb-125b-f8b30afe66b7', N'Alicia', N'Rose', N'Gronipicator Direct Corp.', N'Web', N'ktirmh.hnfjfctay@rbwg.aettno.net', CAST(N'1961-05-17T14:10:33.580' AS DateTime), CAST(N'2016-12-12T01:43:19.940' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'd70e5c50-6cae-392d-691f-f8b4a586c1c1', N'Kim', N'Tucker', N'Varzapazz Holdings Group', N'Web', N'qjjeexyv75@wjewdg.org', CAST(N'1994-02-10T21:03:34.870' AS DateTime), CAST(N'2010-03-07T00:44:13.420' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ecefd93e-795f-216e-ee89-f8d6d162e89d', N'Stephen', N'Cuevas', NULL, N'Web', N'hhmt6@xjwjspa.jobmer.com', CAST(N'1998-09-10T10:07:34.610' AS DateTime), CAST(N'1991-03-21T14:35:13.290' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4860172d-593a-df00-5f46-f94de4d12e8a', N'Melinda', N'Whitaker', N'Emvenower WorldWide ', N'Prepaid Customer', N'wahgnzjd.kllntyf@wuxfsdf.dplupa.net', CAST(N'1973-02-02T20:21:48.390' AS DateTime), CAST(N'2000-11-19T08:26:43.490' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'1c4db3f2-ae6e-7004-2ea8-f9839a67ac8e', N'Jonathon', N'Francis', NULL, N'National Sales', N'nbpujsnb4@wfksii.net', CAST(N'2002-02-14T06:41:07.270' AS DateTime), CAST(N'1961-11-17T20:22:53.500' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'08a527c4-b2e8-775a-9da6-f98d569c8fec', N'Cherie', N'Conner', N'Zeekilan  Group', N'Technical', N'cbqnrf54@cimff.qqwiwj.com', CAST(N'1985-09-08T07:44:53.160' AS DateTime), CAST(N'2013-05-12T15:54:34.520' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7d15b850-34aa-f408-d2cf-f9bbe23d81b8', N'Ruben', N'Juarez', NULL, N'Accounting', N'kxjsors.rbglb@vupu.qamthh.org', CAST(N'1972-08-20T20:28:32.690' AS DateTime), CAST(N'1969-03-21T21:04:12.320' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4358786f-1608-3b43-45bb-fa3490213b2a', N'Guadalupe', N'Nelson', NULL, N'Accounting', N'sfyfg.sshiq@kuwsit.org', CAST(N'1978-01-08T12:02:35.940' AS DateTime), CAST(N'2010-12-11T14:30:21.410' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6d7fbfa6-df43-5738-35bb-fa3605100887', N'Erik', N'Anderson', NULL, N'Web', N'mceq.kkfbx@kltki.ctbkew.com', CAST(N'2014-08-23T06:44:50.020' AS DateTime), CAST(N'1971-01-02T16:28:58.400' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a4a15639-bd1a-729d-23ff-fa573529114a', N'Andrew', N'Blevins', NULL, N'Web', N'aeey.vlsuqblff@qmsjis.org', CAST(N'2005-11-18T20:40:49.900' AS DateTime), CAST(N'1996-04-30T14:09:59.190' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'a52d01db-4e04-7da6-0dbb-fa66076574e1', N'Elisabeth', N'Fuentes', NULL, N'National Customer', N'lgzv.lyykdqbfh@zjpj.yudbfc.org', CAST(N'1986-08-01T02:22:15.430' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'9890d48e-8070-6c04-3b7d-fa991e161bb1', N'Dennis', N'Cruz', N'Suphupopazz International Group', N'Accounting', N'fnpa7@dydfky.net', CAST(N'1974-08-03T16:15:35.120' AS DateTime), CAST(N'2003-04-03T19:34:30.970' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'2fa8c250-7e83-eb2e-47f8-fb1431b9802c', N'Daniel', N'O''Connell', N'Klitumex  ', N'Accounting', N'odajoz.eeduwii@inmmp.tfdwfq.org', CAST(N'1954-06-09T12:33:23.880' AS DateTime), CAST(N'1972-03-31T17:52:15.060' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'562cc6c3-d3fe-c093-5347-fb67c2c881c3', N'Kristine', N'Hardy', NULL, N'Accounting', N'ubaxyh81@nkiagb.org', CAST(N'2003-02-06T08:19:50.150' AS DateTime), CAST(N'2014-03-08T22:07:46.070' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'68a09476-ec89-674c-33e9-fb724ec3c572', N'Yesenia', N'Salinas', N'Supnipentor  ', N'Technical', N'yggzxzq49@yqdwd-.com', CAST(N'2000-10-19T02:00:41.660' AS DateTime), CAST(N'1985-07-10T02:58:23.430' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'006e46e0-74d0-fa53-cdf1-fb8348c4ef91', N'Oliver', N'Lutz', N'Uncadower International Inc', N'Accounting', N'cqidzgjb.jharwrzgpp@yqdkclg.rhbbqb.net', CAST(N'1963-03-02T22:51:26.720' AS DateTime), CAST(N'1960-10-14T14:28:52.630' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'66c0f6eb-a0fb-7f40-4b11-fbfb4e9f8299', N'Alejandro', N'Lawrence', NULL, N'Web', N'vlhat.nnuy@rbjyhr.org', CAST(N'2011-10-25T21:26:33.650' AS DateTime), CAST(N'1964-03-17T23:53:20.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'6ef2814c-7b24-02e6-6aae-fc116a9eb867', N'Lena', N'Lewis', NULL, N'Accounting', N'hzgn@irqabgy.ajgmjd.com', CAST(N'1970-11-13T04:53:06.790' AS DateTime), CAST(N'1957-01-07T20:07:41.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'7d60004a-1977-2468-b147-fc9a73b83306', N'Marco', N'Hoover', NULL, N'Web', N'ochha9@tjaktz.net', CAST(N'1953-08-01T22:47:26.650' AS DateTime), CAST(N'2000-02-26T06:23:09.030' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'80d98b1b-9ee1-9f37-df72-fcbb08e50b2a', N'Gilberto', N'Kirby', NULL, N'Customer', N'ppjqtai1@sewdtm.yrisvh.net', CAST(N'1960-08-01T03:37:34.170' AS DateTime), CAST(N'1965-07-10T14:51:06.830' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'4784ac4b-f0cd-82d6-151e-fcd52cc89739', N'Terry', N'Gross', NULL, N'Technical', N'rcickd0@ylroq.xosaxf.net', CAST(N'1970-08-25T07:58:40.100' AS DateTime), CAST(N'1978-11-23T23:56:27.680' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'f102c51c-fc65-7173-3cc0-fdc51ad91b88', N'Stacie', N'Shah', N'Insapimin International ', N'Service', N'nobcgdex2@oisgbcl.n-wqbs.org', CAST(N'2017-10-09T09:49:29.830' AS DateTime), NULL)
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b1965eb0-7bdc-b902-a364-fe1db6ec466c', N'Angie', N'Glenn', NULL, N'Service', N'smizghw.iwpcve@dnldkfff.gtoonp.net', CAST(N'1959-06-14T04:53:57.500' AS DateTime), CAST(N'1993-05-16T13:48:47.660' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'ef2fe221-17be-29d8-1488-fe287b1026fc', N'Tammie', N'Graves', N'Repebopax International ', N'Technical', N'rvcui8@tdcnhf.uvrdvn.org', CAST(N'1958-09-27T21:03:47.420' AS DateTime), CAST(N'1965-06-25T11:12:14.330' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'73cbf3e2-946c-f43b-fe1f-fe389344d18e', N'Iris', N'Knapp', N'Barwerpar Holdings ', N'Web', N'lwuwcxp.ehdau@impfvldo.dlvlki.org', CAST(N'1960-11-01T15:59:05.200' AS DateTime), CAST(N'1977-10-12T12:01:47.010' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'de24dfab-e6d8-1bb6-f15c-fe52928b4017', N'Dawn', N'Moran', N'Barerin  Company', N'Accounting', N'kvssfvu93@pghobzd.xuvewg.com', CAST(N'1970-05-08T07:04:07.650' AS DateTime), CAST(N'1988-06-13T03:51:49.690' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'e87c328b-5d85-f469-aa18-feb00de1b356', N'Maggie', N'Day', N'Embanantor  Corp.', N'Prepaid Customer', N'kfsf.oogotpet@dwcm.zaywlg.net', CAST(N'1996-08-10T02:56:36.830' AS DateTime), CAST(N'1995-09-06T15:01:10.750' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'daf5a3ef-8ceb-5943-2494-feed0531be73', N'Annie', N'Stuart', NULL, N'Technical', N'ezaxzi.imnaa@gjpraa.com', CAST(N'2004-08-06T03:22:44.860' AS DateTime), CAST(N'1996-01-04T08:11:19.360' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'3df7d613-77ee-f6f9-53a8-ff08a7bffb7f', N'Wanda', N'Villarreal', NULL, N'Service', N'jjgvcat.crgpjilhts@rnxqmv.com', CAST(N'1986-05-02T01:16:11.500' AS DateTime), CAST(N'2000-04-07T18:55:02.520' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'b0c85e23-f239-e644-33f3-ff0a990233b9', N'Sandy', N'Mooney', N'Unmunazz Direct ', N'Prepaid Customer', N'lixgimq405@qubzdnd.l-ufnu.com', CAST(N'1969-03-24T04:07:33.410' AS DateTime), CAST(N'1975-10-10T01:49:56.740' AS DateTime))
GO
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Company], [Title], [Email], [CreatedOn], [UpdatedOn]) VALUES (N'93147583-5237-ada6-708f-ffa3195e12b4', N'Jackie', N'Crosby', NULL, N'Service', N'ftocw31@vjnjwyf.taiwn-.org', CAST(N'1953-12-29T22:25:35.150' AS DateTime), CAST(N'1969-09-08T13:53:39.740' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4c82aa22-3de0-aaf8-ca37-007e7e592a89', N'af352f9d-b0b5-2f93-0191-1841c7b83c9b', N'0306819504', N'office', 1, CAST(N'2013-12-29T17:50:37.040' AS DateTime), CAST(N'2000-07-22T05:12:11.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'62a494ee-37cf-3021-5a29-00b54adc9255', N'f6191ae8-77d0-16b5-d6b1-2513f879f90c', N'560-3954138', N'office', 1, CAST(N'1955-08-21T20:59:51.920' AS DateTime), CAST(N'2012-11-18T17:02:04.850' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5c81123b-e3c4-97c2-f263-00f87dd5f209', N'420d6a7e-7be8-bfc3-be5b-694c59d43cb7', N'821-6261132', N'cell', 3, CAST(N'2015-09-07T04:14:51.640' AS DateTime), CAST(N'2014-04-09T14:18:49.550' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'19bc70ad-d2a8-774a-8afa-015c5178a06e', N'5f23d050-d077-7ee2-df4d-4b0eefc27b04', N'9531529074', N'cell', 1, CAST(N'2002-12-21T08:06:38.720' AS DateTime), CAST(N'1963-08-25T22:02:59.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'53e3271f-5aee-47a7-1bc5-0233f7bf5581', N'09fe4eae-193c-0012-7549-840229736197', N'787-3201809', N'home', 1, CAST(N'1983-06-13T02:43:16.040' AS DateTime), CAST(N'1973-03-26T08:34:21.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1731a057-f569-d6b0-2713-0246687ee5ec', N'a39f229d-b0b7-f608-e0e2-aae9b1d8dfeb', N'5920562977', N'cell', 1, CAST(N'1970-08-07T13:41:38.700' AS DateTime), CAST(N'1969-07-07T11:12:02.990' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4f1ffba4-4358-bfd1-1d8c-024ce33bd941', N'70bbcd78-ae11-220e-23e7-0936fca70187', N'1537182752', NULL, 2, CAST(N'2013-09-09T11:35:04.400' AS DateTime), CAST(N'2018-01-25T05:01:34.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'05ae810e-c02c-5c7e-4e99-024d483747b3', N'9efdcea9-dcb1-c6fe-2a65-70097f0757c0', N'912615-5353', N'cell', 3, CAST(N'1996-04-07T11:27:17.570' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'717a03e2-9883-47fb-3575-025dcde39517', N'04f7a104-dc81-f667-8061-b879a9e0ced4', N'4327306162', N'cell', 1, CAST(N'2015-06-22T17:57:09.860' AS DateTime), CAST(N'2010-07-05T00:03:51.140' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd3106a21-be4f-2969-4123-026298c2dffd', N'b7bb975e-d517-1ab4-ba67-36b40cb178d4', N'177-2213285', N'cell', 3, CAST(N'2017-04-01T05:47:28.650' AS DateTime), CAST(N'1979-03-09T12:49:06.200' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6d43ba65-fa37-55a8-95b4-02ed942f2acf', N'55a57d26-432c-fda9-f2d0-257339f793fa', N'273-7410708', NULL, 1, CAST(N'2009-07-08T19:41:08.360' AS DateTime), CAST(N'1999-07-05T21:34:18.180' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'381a4ce2-2af7-73bc-0ec7-03050aea88d2', N'58e389ad-b7f9-9b59-134d-91e8d8084ae2', N'079450-0761', NULL, 1, CAST(N'1998-07-14T15:00:21.810' AS DateTime), CAST(N'2000-02-08T02:25:40.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'477129e8-180c-56b9-880c-033248a4bce4', N'42eb918f-7cff-f324-e511-aae9c3d8b2e6', N'736-726-5888', NULL, 1, CAST(N'1992-08-29T18:51:25.360' AS DateTime), CAST(N'2002-08-25T20:48:39.130' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ee23f368-34ff-9330-d0d5-037a7c11b0b4', N'e8999356-f0f6-96e3-5466-7f345b2a747e', N'060362-3245', NULL, 1, CAST(N'1960-01-29T03:59:22.070' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'90401ee8-41d0-f45b-4457-03c9533e64f8', N'220ea8f3-4067-fec0-02c9-083f13d0601c', N'539793-0662', N'home', 1, CAST(N'1957-02-28T10:33:40.890' AS DateTime), CAST(N'2007-10-07T17:41:58.040' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4524f9c3-fbf1-84d4-b6fb-03cd67588a63', N'b61b8fba-2674-9f78-76e7-22d6bb733da5', N'386963-1938', N'cell', 1, CAST(N'1954-02-04T21:36:07.770' AS DateTime), CAST(N'1982-11-05T23:55:13.940' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'165addad-a4d5-f4ad-c2ed-0467f3fbae2c', N'bef564bd-5112-8457-9209-cc31c0d68166', N'2696743216', N'cell', 1, CAST(N'1971-10-24T19:25:12.310' AS DateTime), CAST(N'1969-09-14T03:56:33.300' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1cb2f330-b4fe-f5ad-2b59-0496bf89254b', N'4d472212-b448-62ef-9429-1fbfab5ef447', N'7435954957', NULL, 3, CAST(N'2004-05-12T07:37:16.710' AS DateTime), CAST(N'1954-02-19T22:08:16.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5fcd8748-4284-dab0-9531-04adee706e64', N'96155c86-dd07-2b58-fceb-27c25da8661c', N'214-109-1555', N'cell', 1, CAST(N'1996-05-04T14:24:28.260' AS DateTime), CAST(N'1954-12-22T03:42:56.780' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7572fc2f-9580-7b0c-0c4f-05070b005a75', N'2384913c-0421-394f-db46-6f1428d74293', N'231-267-8147', N'cell', 2, CAST(N'1986-09-14T12:35:04.420' AS DateTime), CAST(N'1997-06-29T09:59:27.080' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'85ad4bca-f986-2955-93c7-051c58112f1a', N'ae515959-b9a4-e134-ae80-f30307ae568c', N'5454147756', NULL, 1, CAST(N'1988-02-25T12:47:08.870' AS DateTime), CAST(N'1971-12-08T11:42:10.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b70ad7f4-0302-1abf-1b33-05a7597a7981', N'79e47169-5a21-b8bb-125b-f8b30afe66b7', N'307-7510427', NULL, 3, CAST(N'1994-02-28T13:36:13.170' AS DateTime), CAST(N'1959-05-26T07:59:22.040' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8e714374-29d3-a47c-8300-05e58e4e87f5', N'2db30fca-91ea-c818-ef80-cab7248c7fd3', N'803-0118487', N'office', 1, CAST(N'1997-11-08T05:13:25.800' AS DateTime), CAST(N'1971-10-22T20:15:47.310' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e299df38-3e07-d22e-1d5b-0626a1a81959', N'5b9e9d9b-106a-9d65-a56b-9780da430095', N'254889-1485', N'office', 1, CAST(N'1955-09-23T03:47:01.580' AS DateTime), CAST(N'1980-11-29T09:59:30.210' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2e01ad0c-5703-6fe5-977b-065507e4fac0', N'd94165af-22f9-3e81-1b01-0096ef9a9fd2', N'6104063065', N'office', 1, CAST(N'2012-12-07T19:31:56.320' AS DateTime), CAST(N'1998-09-25T04:12:19.100' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'46115053-6176-210d-9d7f-068171b2ad56', N'5d39ff51-c531-f968-3e00-46063942c2dd', N'9049345658', N'cell', 1, CAST(N'1998-06-27T07:56:12.070' AS DateTime), CAST(N'1957-05-18T05:55:37.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e784d4c5-fdfc-93d7-2649-0694858783db', N'2ad28961-418b-91be-d710-0a7d2748bb70', N'745-775-0292', N'cell', 4, CAST(N'2014-01-30T19:49:00.860' AS DateTime), CAST(N'1987-05-15T14:24:10.240' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'10e34e7b-4093-e9ee-5d3d-06b860cac38c', N'b2b0b384-5260-2019-ecba-2d97a95c0592', N'016621-0721', N'cell', 1, CAST(N'1995-10-18T03:03:01.340' AS DateTime), CAST(N'1972-09-26T18:06:54.430' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c33f2e4a-d849-1ab3-c7fb-06c89b5a22ca', N'e01dd7e2-3434-9b75-98de-20fc39f1d391', N'948-8524274', N'cell', 1, CAST(N'2002-12-30T07:58:21.680' AS DateTime), CAST(N'1976-03-05T23:50:01.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2713c70f-71de-b488-fcf4-06d24a5b844c', N'fbe92c7e-8e20-6282-2cf9-511cdb5daa62', N'921-4775332', N'office', 2, CAST(N'2007-10-11T21:07:48.800' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'410e0e21-49e3-39ee-9022-06f7bf196fc0', N'b2a4f0b0-a7c7-cafd-f374-4d76224b954d', N'750-6580827', N'cell', 1, CAST(N'2013-10-02T01:26:59.190' AS DateTime), CAST(N'2000-04-30T22:02:39.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'72aeaeb8-b07d-9623-9f93-07a80767f895', N'4d472212-b448-62ef-9429-1fbfab5ef447', N'642-4683374', N'office', 2, CAST(N'1980-03-25T16:15:58.100' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9db6143c-0bba-3341-f0e9-07a88406044c', N'291172c1-c9a9-06e9-768a-5e7fa6dbefbc', N'954-7332651', N'cell', 2, CAST(N'1964-10-21T04:04:04.170' AS DateTime), CAST(N'1960-10-23T11:42:18.160' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7b073cbd-e2c1-131e-4042-07c9f7d3fd52', N'09be0827-264f-e2e8-3a04-dddd9e0e492d', N'8572309728', N'office', 1, CAST(N'1963-10-01T09:40:54.960' AS DateTime), CAST(N'1969-02-24T19:40:44.230' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c20f3596-5a47-2cca-2806-086aaab47fac', N'b78de124-a67a-a73a-cb9b-a62859a9f064', N'647-6786459', N'cell', 2, CAST(N'1976-06-22T03:20:06.520' AS DateTime), CAST(N'1981-06-08T19:56:19.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'26295731-da56-37ff-c742-0897d0378ec2', N'80fa4d75-8b13-d4ac-71b7-886a1bfddbb2', N'582808-0680', N'cell', 2, CAST(N'1964-04-20T03:24:06.650' AS DateTime), CAST(N'1954-10-26T07:57:05.410' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f3cf643d-8703-7094-f289-08bdf18f1435', N'9c1634b8-4007-9d44-6cce-b4da71bda2df', N'267-084-4066', N'cell', 1, CAST(N'2002-11-28T16:21:56.520' AS DateTime), CAST(N'1963-12-16T14:18:34.640' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'64e69d9c-af6b-9541-8d78-0909900d739c', N'5b7937a7-3420-4faf-0477-ec751bbd2ed1', N'842318-4548', N'office', 1, CAST(N'1959-09-11T19:50:25.380' AS DateTime), CAST(N'1979-09-26T03:05:22.960' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'44268db8-2e83-3d59-eefc-0952df6497ac', N'bd32c87e-d13a-f98d-6b1c-9f081c918f1b', N'724976-7945', NULL, 1, CAST(N'1974-05-07T03:26:42.300' AS DateTime), CAST(N'1999-10-07T02:28:49.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e264100f-a55c-d311-c509-0953b803e9a5', N'66a3a4a3-6315-4974-df77-5b52f0fa2fa7', N'219-274-8366', N'cell', 1, CAST(N'1985-11-09T01:05:18.470' AS DateTime), CAST(N'1994-03-05T19:13:37.560' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1c4b03e0-0921-076a-5282-09d06cb1ed9f', N'a52d01db-4e04-7da6-0dbb-fa66076574e1', N'8199052035', N'cell', 4, CAST(N'1985-01-12T07:42:31.170' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1eb42186-0dc0-2170-32b1-09dd3d8e3039', N'8cfceb7c-f311-c333-1efd-873672fe655d', N'808227-7319', N'cell', 1, CAST(N'1962-04-26T12:03:23.600' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'57e5c95a-ed75-584e-9f9f-0a98c1bc1abb', N'721166a1-9c32-0b14-14d2-be617e0276c9', N'824414-3385', N'cell', 3, CAST(N'2016-08-08T09:42:46.720' AS DateTime), CAST(N'1960-04-09T17:37:42.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2c48e48f-8574-6f4a-cc8d-0a9eaa6f2dbd', N'1f55ef41-5151-ec41-f403-8a3e65b01044', N'5887344964', N'office', 2, CAST(N'1975-12-18T17:57:54.560' AS DateTime), CAST(N'2003-03-26T08:46:20.980' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'949dc5b9-c20c-4708-5e7c-0aa76e64980d', N'2e16a7ba-e2a8-9ab8-afe4-940dbc5bf800', N'7706050827', N'office', 1, CAST(N'1971-02-12T10:17:20.680' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f671a15c-5efe-fb85-98e2-0ad014270033', N'c0737605-cd48-9788-60d1-23f50ccba99e', N'650-598-9859', N'home', 1, CAST(N'1979-02-22T08:49:38.160' AS DateTime), CAST(N'2010-02-08T19:53:49.420' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e32e9e54-6934-86a9-e2a7-0b012ce42332', N'09be0827-264f-e2e8-3a04-dddd9e0e492d', N'466113-1095', N'home', 2, CAST(N'2008-11-25T12:55:09.870' AS DateTime), CAST(N'1992-02-22T04:55:10.260' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bc1568e0-6f5e-34ee-6e49-0ba99e6a772c', N'08667db7-1a92-259b-d714-213e0cfc60cb', N'751-8925694', N'office', 1, CAST(N'1953-09-01T18:01:11.740' AS DateTime), CAST(N'1982-08-08T15:42:00.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'dae27b60-216c-bb03-9426-0bc596d256c0', N'fd311dc0-e2c2-f82c-be36-7cfaa08ec84d', N'721-939-9898', N'cell', 1, CAST(N'1972-12-15T09:38:39.640' AS DateTime), CAST(N'1972-01-30T09:50:25.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1e27f634-2dc2-f90d-08b6-0bca4cbe06f3', N'7251598e-19bc-6e97-e94c-ae5777cc37e8', N'969-4495849', N'home', 1, CAST(N'1982-04-26T02:54:38.060' AS DateTime), CAST(N'1963-01-15T21:08:12.690' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd4c60784-6dd3-f761-fd69-0bdeb41f1216', N'79005d3c-a544-9cc0-1ad7-2616b8c5bfdc', N'5332162964', N'cell', 1, CAST(N'1976-11-12T09:56:13.640' AS DateTime), CAST(N'1957-06-05T13:11:42.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6078c727-12e0-052e-538c-0c457bc5a34f', N'9f9cf971-7492-aa1e-3a1d-0f59f7dc7557', N'9545036487', NULL, 1, CAST(N'1995-04-17T21:49:53.170' AS DateTime), CAST(N'1973-04-25T19:38:24.220' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'acfb6bc2-b478-2789-2fcc-0cb7e4e8febf', N'9796b441-ee6e-6d1c-412d-1caf037a0f97', N'052-383-9495', N'home', 1, CAST(N'1986-05-28T21:17:34.540' AS DateTime), CAST(N'2007-08-11T03:40:42.960' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'423ab56c-2d23-1aae-2b45-0cc6d4928d3b', N'cf23caed-bd72-682c-024d-5c3d5a8af49c', N'506-7492893', N'office', 1, CAST(N'2008-11-22T07:16:12.910' AS DateTime), CAST(N'2014-01-24T15:52:19.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e92a284a-9700-e8c4-6531-0d05de71bb63', N'49fba2f4-8c26-3c4f-e9f0-6a7e3ec256f7', N'4559541552', N'office', 2, CAST(N'1998-09-06T11:17:20.240' AS DateTime), CAST(N'1964-11-19T22:37:26.370' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd4a56208-66f7-566a-ba4f-0d4b9bd24657', N'6c2e1e75-59b9-8ff3-48b4-f5fd63854781', N'0275964023', NULL, 1, CAST(N'1954-08-20T16:44:42.640' AS DateTime), CAST(N'2012-12-03T18:56:10.000' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b179bd52-f7a7-2d4e-f120-0db2dfe89863', N'a15e4246-d3f0-5894-e18b-e6439ae103ed', N'793327-6857', N'cell', 1, CAST(N'2000-02-21T19:46:51.000' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ddf2b257-f2ed-3826-1a82-0df098d451e2', N'56a51711-262f-2479-bba1-653d0c12445f', N'141-903-4875', NULL, 1, CAST(N'1968-05-30T20:49:59.880' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fd2bc810-a009-222c-f305-0e72eb0cc899', N'520ae4eb-91e7-cea0-29d6-3d5684e510aa', N'217202-4448', N'cell', 2, CAST(N'1971-03-01T16:33:59.160' AS DateTime), CAST(N'2016-11-20T17:56:29.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bbb8443f-dc5f-c467-5443-0ea9afffb5d4', N'eb49798b-ed31-3834-62cc-41835d98d275', N'8710519303', N'cell', 2, CAST(N'1983-02-08T15:33:36.940' AS DateTime), CAST(N'2011-10-28T17:13:26.120' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c8ed635f-fcea-2967-51fa-0ede707f4e06', N'034d481d-92bd-d36c-d0ae-36db84c26069', N'9512490992', N'home', 1, CAST(N'2012-02-10T21:36:04.890' AS DateTime), CAST(N'2010-04-26T14:24:56.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'95648313-73a5-9860-93c6-0f2e04a8b349', N'b00a9022-7859-9b78-029b-71f11641c1e3', N'534-580-5994', NULL, 1, CAST(N'2017-12-06T04:43:45.800' AS DateTime), CAST(N'1975-07-31T21:31:09.490' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'33eaa838-6c12-f26a-0b27-104cd669f6ef', N'50596efb-1105-f424-6e5e-d63a1d5cdab4', N'9082783146', N'cell', 1, CAST(N'1978-01-12T08:59:13.440' AS DateTime), CAST(N'1980-07-18T09:00:25.290' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2a928dc8-b106-981e-6ffd-1050efb3bc18', N'52a23ffb-91ad-bcc0-e93b-58abef6b787a', N'4482943836', NULL, 3, CAST(N'1995-09-23T20:06:17.770' AS DateTime), CAST(N'1987-01-24T15:39:56.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3f47a1bf-beb2-728c-900f-10b31abc1774', N'e54c0cc8-9a41-9a1d-c1d3-f6897d0bd842', N'391-0441402', N'cell', 1, CAST(N'2002-09-25T18:39:58.990' AS DateTime), CAST(N'2005-11-13T03:25:12.200' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f6328978-3169-252e-72d2-10c02fef3a40', N'8b224545-a456-60d4-5a08-79367fd275f5', N'016111-4041', N'office', 3, CAST(N'2003-09-18T02:28:47.840' AS DateTime), CAST(N'2001-08-16T07:28:14.710' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bda4b9ab-cca8-a680-9760-110bea7999ff', N'db61f9fb-6751-1fd1-1f75-97a7cbaf3d65', N'605-925-2868', N'cell', 1, CAST(N'2013-04-16T22:04:10.200' AS DateTime), CAST(N'1999-07-09T09:32:52.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'44accda8-20d1-29ca-8039-110dab90bebf', N'78496b1c-b6e0-c6f7-0356-5253b1011e64', N'964-620-6571', N'cell', 1, CAST(N'1954-03-23T04:09:43.640' AS DateTime), CAST(N'1997-09-14T08:46:35.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e002071c-66cb-9e29-b3f0-1159c0ddc8ef', N'd4486f0e-44f1-2be0-f710-e9ac60921a82', N'6630202002', N'office', 1, CAST(N'1969-09-06T00:46:05.700' AS DateTime), CAST(N'2018-06-18T23:48:07.010' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4ff3bdf4-0fad-b597-a352-116d79d13360', N'6d9de423-992d-7114-72f2-971ca5e9a099', N'913-6499274', N'home', 2, CAST(N'1978-04-26T16:34:42.860' AS DateTime), CAST(N'2015-08-01T11:55:44.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'823d9dbe-62c8-351f-b2b0-1210b651b791', N'd2b1cef6-10ce-303d-31f1-f4733084bdf3', N'598417-8199', N'home', 1, CAST(N'1984-11-12T07:10:41.650' AS DateTime), CAST(N'1995-05-01T20:29:50.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bf14b7dd-6980-7bdc-5382-1236601ccda3', N'ca8d6850-c123-ba14-5894-2a2385f5c81d', N'050-5865989', N'cell', 2, CAST(N'1971-05-07T16:03:17.100' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1b71a2ab-412f-f8c4-6353-1254c727e140', N'5edf2c53-c101-2a8d-4bb8-878d48bfa003', N'318-5503031', N'cell', 2, CAST(N'1999-04-30T04:24:56.820' AS DateTime), CAST(N'2014-06-22T12:00:39.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'54d866fb-8d80-b3ad-f6dc-125528e75b44', N'81845987-0e1d-b2ec-8bab-f71113e42ce9', N'874-4575554', N'cell', 1, CAST(N'2011-03-22T16:15:58.280' AS DateTime), CAST(N'1962-04-29T03:49:34.480' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4b3d1935-0498-9717-6b48-12d171967e7f', N'e4d62b47-6199-5876-04b0-31d22e513af9', N'521-532-0031', N'home', 1, CAST(N'1980-12-07T14:54:25.760' AS DateTime), CAST(N'1957-12-09T13:24:14.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'823e1031-339b-7859-4b0e-1306639a9246', N'75841d4d-07bf-3cf7-c42f-84a55018e69d', N'6682591424', N'office', 1, CAST(N'1978-05-31T14:44:21.640' AS DateTime), CAST(N'1957-03-19T22:44:59.680' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'95ede8fd-bf5e-49b1-2520-1324974ede2a', N'e08c2dac-5e73-e48b-6182-94ff1b6346c3', N'373-163-1447', NULL, 2, CAST(N'2007-04-15T05:44:19.380' AS DateTime), CAST(N'1976-04-19T21:42:42.010' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e58f9f67-6bf3-8261-9c47-132b022fab13', N'8803a231-7eac-d3ed-a79d-e1e72d8580e3', N'984588-8082', N'cell', 1, CAST(N'2016-02-13T06:31:37.460' AS DateTime), CAST(N'1997-06-16T17:55:37.370' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2731f528-78db-f20a-f836-1333bf36a79e', N'dd91cac4-746c-b5f7-f61a-13281e8adb89', N'620-8630086', N'cell', 1, CAST(N'2009-04-26T05:36:59.250' AS DateTime), CAST(N'1956-08-15T12:26:59.580' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bafe685e-dd08-5b1c-2362-13f4a5dbc79c', N'e3e7ad1c-9b66-bb25-883b-866f2bb86877', N'480177-8311', N'cell', 1, CAST(N'1967-08-25T21:40:32.970' AS DateTime), CAST(N'2005-12-24T09:55:53.120' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e6505646-41ea-287d-6d7e-140bd06d29e5', N'a5c2ca5a-03e4-db38-7708-a16c9facfa21', N'844-459-9669', NULL, 2, CAST(N'2015-12-11T10:28:19.710' AS DateTime), CAST(N'1992-11-10T21:53:29.680' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ee9e283e-480d-5ebc-7d9c-14389b6dac69', N'0dacfdba-38ea-33bb-6def-de99227bbeef', N'836-1365908', N'office', 1, CAST(N'1969-11-25T23:02:20.920' AS DateTime), CAST(N'1992-01-31T17:59:19.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd09b0a20-2587-6745-f04c-14fec75ba468', N'f73cec99-ce9d-24ca-6686-002f290b093c', N'684010-9495', N'cell', 1, CAST(N'1960-07-10T23:32:48.350' AS DateTime), CAST(N'1953-10-01T15:21:25.400' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'30327683-6a8d-49ec-0dd0-15423bddd23f', N'7eef051f-5a0b-6568-77d8-5b8d4d6b646b', N'3414411233', N'home', 1, CAST(N'1984-04-12T22:18:49.550' AS DateTime), CAST(N'1994-01-11T02:42:59.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3840dc52-f26b-b225-1fd2-157a21dae269', N'9e632021-e1b0-f255-4fb6-d8bdecc0eda5', N'077155-0084', N'cell', 1, CAST(N'1958-09-09T05:20:06.430' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'93883968-818e-68a6-92ee-158f64effa05', N'168ac464-d503-88b8-8492-db391d28c6d5', N'596344-8137', N'office', 5, CAST(N'2009-08-25T01:03:37.290' AS DateTime), CAST(N'1988-12-01T13:43:37.230' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ccfaef09-0ce1-a525-dcf4-168f60eacc07', N'1fd858ed-aadc-8821-b6a8-f04d2ae43181', N'934-526-7201', N'cell', 2, CAST(N'2001-04-05T06:55:01.770' AS DateTime), CAST(N'1979-07-29T15:47:18.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f968a460-aab2-c629-1684-16ae5b2ed7bf', N'0c47010b-e6f4-0a99-21ad-03f86c3986fe', N'1308554142', NULL, 2, CAST(N'1986-02-20T14:25:42.610' AS DateTime), CAST(N'1961-08-17T14:57:56.670' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'92138eb7-cc25-a22c-1a5d-1745cd9d424e', N'e002b7fd-e7dd-a8cb-1a42-55f912567e03', N'457-088-9901', N'cell', 3, CAST(N'1994-06-29T08:06:28.480' AS DateTime), CAST(N'1999-10-01T20:45:07.460' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8edba1d7-b5da-0af0-789c-175a4016cc29', N'198eaea2-9578-ce16-4cf6-000bbbd22af3', N'5082199350', N'cell', 2, CAST(N'1995-05-17T09:21:29.870' AS DateTime), CAST(N'1964-03-31T02:41:14.140' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'349b0041-8b11-be32-55e3-176733c8d4b7', N'66c78fc1-6ca5-f206-8563-231300c1e0cd', N'223664-6428', N'cell', 1, CAST(N'1977-10-21T14:04:02.240' AS DateTime), CAST(N'1963-11-14T11:01:12.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'53c647d7-addc-6d69-f066-1773f92e767a', N'f3b50ba6-84ed-8c6e-1270-e99e78ecf575', N'587-494-9049', N'cell', 1, CAST(N'1979-09-16T11:58:15.220' AS DateTime), CAST(N'1962-06-21T20:16:19.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'97dce9ff-9ddc-ce8a-3cd3-17ab0ad0878f', N'e3e7ad1c-9b66-bb25-883b-866f2bb86877', N'9791196289', NULL, 2, CAST(N'1992-12-25T00:36:12.110' AS DateTime), CAST(N'1985-12-23T23:37:27.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'609af31f-a268-55f8-c9bf-17c850eb2bf2', N'19f5366f-864a-fe18-0cc5-62a5e9245455', N'0503052200', N'cell', 1, CAST(N'2016-06-20T04:49:03.530' AS DateTime), CAST(N'1997-12-07T14:41:10.780' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'46d80c45-b3b7-50ff-285f-17f3d0e8921a', N'fab58a6d-2cae-7d97-e571-2bff2d87808f', N'785-821-6719', N'cell', 1, CAST(N'2011-03-26T20:05:47.280' AS DateTime), CAST(N'1962-06-11T12:37:50.080' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1815ded6-a57e-ac56-9b56-17f7f44ce7b6', N'e14b4823-ed0e-1a56-8a95-8eb26573bb5d', N'127-7239666', N'office', 1, CAST(N'2000-10-26T07:11:14.750' AS DateTime), CAST(N'1997-03-30T18:08:58.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0cada18b-f0fa-0f67-5fd8-181920fb6c10', N'0f5bbd18-2d78-cb1a-9930-4380d6c34681', N'317-241-9124', N'cell', 1, CAST(N'1992-09-07T18:18:57.480' AS DateTime), CAST(N'2001-12-16T07:50:19.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'aceb001c-e67f-2720-77b7-182874cb720c', N'ffb5a984-b061-45db-9a6b-2484cd0af2a7', N'1837201489', N'cell', 1, CAST(N'1966-12-17T06:04:39.680' AS DateTime), CAST(N'1994-06-12T21:24:49.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'26a8f436-b996-44d7-1061-188cc7e4a685', N'3181779d-7098-d55a-2fb8-12f0f58e898f', N'004-352-9598', N'home', 2, CAST(N'2005-10-12T10:34:04.530' AS DateTime), CAST(N'1968-08-07T02:22:28.000' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'893dd2a9-0dd8-1923-c7a1-18bcd8a3b66c', N'0caa0190-56c1-0ecf-73be-b896102e0ae2', N'683-590-7826', N'home', 2, CAST(N'1991-07-23T16:26:58.100' AS DateTime), CAST(N'1974-09-07T15:55:57.500' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd39f7a36-ed06-b7c2-ce9e-18ee2a997a98', N'e18e6400-fac6-55f8-9d0b-6caa05ae72f0', N'394567-8183', N'home', 1, CAST(N'1987-10-13T13:16:05.320' AS DateTime), CAST(N'1971-10-14T19:07:08.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'83ace2ee-ed6d-dbb0-a850-18ff78399dc1', N'6863c090-dabd-c27b-1f1e-d0c514a6c045', N'590637-7561', N'cell', 2, CAST(N'2016-08-03T01:26:11.550' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'26f42d85-a8f8-3492-7462-1911766276e1', N'bd32c87e-d13a-f98d-6b1c-9f081c918f1b', N'2523568469', N'cell', 2, CAST(N'1982-05-13T09:01:49.180' AS DateTime), CAST(N'2003-09-26T19:25:24.870' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b7e33649-389b-3049-1ab2-1969c3cf9239', N'bbf080ed-8ff7-de34-2166-04adf0722f3f', N'469036-6309', N'office', 1, CAST(N'1966-03-15T19:16:22.450' AS DateTime), CAST(N'1962-02-19T23:42:17.310' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd12d29c3-5060-8748-a80a-19a0188606e3', N'd7ba1a6c-ea19-28ee-fda1-4d2b43e77c02', N'039-521-5159', N'office', 1, CAST(N'1966-01-17T10:39:08.380' AS DateTime), CAST(N'2018-02-10T07:47:26.190' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b2fc31c7-3b00-a2c3-f407-1a832a5ca4d6', N'a791ae4b-a558-dbb7-a997-8d41a538aa9c', N'737-771-9632', N'cell', 1, CAST(N'2011-09-15T10:37:08.130' AS DateTime), CAST(N'1971-01-30T01:23:07.550' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd75f6d8e-d6d7-3358-bece-1a84194d75f1', N'ec48face-b1e6-877f-4ec4-5d992adfa63a', N'720-393-8499', N'home', 1, CAST(N'1972-12-30T21:20:49.740' AS DateTime), CAST(N'1968-05-30T18:36:32.080' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'305833e3-afdb-cb7d-8c80-1ace5daf83c1', N'0d14bb37-2feb-daf1-daa9-7f7b30f59506', N'2297750046', NULL, 3, CAST(N'1980-05-12T10:42:53.350' AS DateTime), CAST(N'1966-05-29T08:55:18.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8809a517-9b72-42b0-d681-1af3e4da4254', N'291172c1-c9a9-06e9-768a-5e7fa6dbefbc', N'957-2865303', NULL, 3, CAST(N'1981-04-22T16:17:07.150' AS DateTime), CAST(N'2006-08-04T10:31:29.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'064ebd2c-9d2d-1b3b-d7ac-1b2b6f9a6642', N'82a1d6ff-5c8f-dd03-2e0c-652437ec8f6f', N'329-9584402', N'home', 1, CAST(N'1982-02-08T10:40:40.490' AS DateTime), CAST(N'1979-01-05T11:36:10.850' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c8159790-180a-1bb7-da5b-1b743870155e', N'f2ca0e6b-f535-01a6-11d7-4ebdef9f666e', N'249-3510163', NULL, 1, CAST(N'1970-03-13T06:59:08.400' AS DateTime), CAST(N'1964-12-28T17:47:45.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6cf04536-41eb-b1e5-54cf-1b7bfe7a96a9', N'899e4fc5-4810-0f60-ddd3-5b3db85a02c3', N'590485-8284', N'cell', 2, CAST(N'1956-07-28T14:43:04.490' AS DateTime), CAST(N'1976-08-09T03:04:54.250' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'66b5c847-5eaa-93d0-69f4-1bbfe8e85319', N'4db67abd-ce6d-5c9d-5d25-7f035fb45996', N'6308242829', NULL, 1, CAST(N'1971-12-27T05:31:34.620' AS DateTime), CAST(N'1956-01-23T13:32:40.960' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cbdf323d-59ff-383d-77e5-1bf674cd7751', N'288b18e6-939e-a7ab-6cd6-b13e8001a20b', N'082-495-5052', N'cell', 1, CAST(N'1990-02-16T19:22:09.930' AS DateTime), CAST(N'1992-12-14T22:40:55.570' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b2e7051e-6ee1-0bb3-be49-1c62fe026fff', N'fe01118a-9745-39bb-98b9-795b7e64901f', N'409-9178044', N'home', 2, CAST(N'2018-07-11T11:04:15.650' AS DateTime), CAST(N'1967-07-27T06:11:52.890' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4a99b316-be3f-29bd-b8b4-1c8902f9ac5c', N'5a4d3d1d-bac3-cb3a-3976-7e29d71f31bd', N'5619452250', N'cell', 3, CAST(N'2014-02-09T18:11:12.270' AS DateTime), CAST(N'1983-08-21T02:35:25.850' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'85c8f8a9-024e-b316-d995-1c930c9087a0', N'a1c66f30-4e03-f513-9899-239f0268601f', N'2333718373', N'office', 1, CAST(N'1954-11-08T12:41:23.200' AS DateTime), CAST(N'1991-04-11T05:54:31.360' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b80dc248-d16e-9938-fdac-1c99a3ef6842', N'f1d06352-63d2-8b8e-a9c3-503d4c745143', N'0926868644', N'office', 1, CAST(N'1994-02-22T20:17:58.360' AS DateTime), CAST(N'1969-10-07T13:49:32.490' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'42fb4451-2090-3ebf-d09d-1c9a6b771a00', N'd7d0b95a-a44a-2609-d36a-ba2c8b1ca09d', N'0173439910', N'cell', 1, CAST(N'1999-10-26T05:40:29.510' AS DateTime), CAST(N'1954-09-09T10:47:48.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'649f197d-567f-bcda-21c3-1cf41f3e6b14', N'68a09476-ec89-674c-33e9-fb724ec3c572', N'636-8322709', N'cell', 1, CAST(N'1955-06-13T17:14:04.340' AS DateTime), CAST(N'2018-12-30T11:46:13.100' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ad57415e-b353-9eb3-c248-1d0b9b9b80fa', N'814cc60a-4ca9-879f-1e33-e1a70d68cb73', N'019-2641018', N'cell', 2, CAST(N'1985-09-26T10:51:12.280' AS DateTime), CAST(N'2006-11-01T02:02:31.660' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f60c9d53-5711-a345-2139-1d45713041c9', N'4024c379-555f-6031-2b15-d47f31e13355', N'613638-4595', N'office', 1, CAST(N'1962-02-14T09:22:45.800' AS DateTime), CAST(N'1957-08-06T08:58:30.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cbd8ac42-c0e8-d538-7c5e-1d4d2367eb27', N'6d0eb4f5-0bda-493d-f6ce-089bbfed88a5', N'539-1604827', NULL, 1, CAST(N'2004-04-04T05:13:41.260' AS DateTime), CAST(N'2017-09-21T17:54:21.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e9077abc-4a7f-55fc-2903-1ebb47240b92', N'520ae4eb-91e7-cea0-29d6-3d5684e510aa', N'755249-0692', N'office', 3, CAST(N'2005-05-07T09:54:14.990' AS DateTime), CAST(N'1975-07-17T13:51:27.920' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'514343ea-ae17-901c-a9ef-1eea42574e9f', N'ee2bbc35-f462-355c-b1ad-d2d70e258073', N'5980189001', N'home', 2, CAST(N'2009-07-23T02:38:28.030' AS DateTime), CAST(N'1959-06-07T03:13:42.740' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5f07c933-34bd-41d0-824d-1eedb008f9a5', N'6863c090-dabd-c27b-1f1e-d0c514a6c045', N'608-0114653', N'home', 1, CAST(N'1998-05-31T20:28:13.420' AS DateTime), CAST(N'1979-05-06T15:41:14.890' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'155eff00-0da2-8b8b-0ea4-1efc10729ecf', N'c89219ca-3cbd-729d-49f5-74fcd35f4813', N'096-0396813', N'home', 1, CAST(N'1959-03-15T17:15:50.980' AS DateTime), CAST(N'1970-07-29T14:47:47.690' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8f2ca311-1bdd-5dac-85cd-1f27460aab57', N'721166a1-9c32-0b14-14d2-be617e0276c9', N'765-8548472', N'home', 2, CAST(N'2015-10-28T19:43:30.860' AS DateTime), CAST(N'1961-06-03T03:08:09.130' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'35942c16-1743-7d24-eec8-1f8ef0ad51c8', N'c3c015f6-6e90-55f7-ea65-0b12c9128d30', N'763922-3272', N'cell', 1, CAST(N'1994-11-14T18:30:12.220' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4be24a1c-b1e3-dac4-5f66-2022ae657766', N'a52d01db-4e04-7da6-0dbb-fa66076574e1', N'260-417-7591', N'office', 5, CAST(N'2015-04-20T21:17:14.770' AS DateTime), CAST(N'1995-09-17T05:26:54.080' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6a56c0d0-dcd3-bb4e-6005-203ba7540905', N'fa5ef110-4ff1-6a7f-e2f1-5122693c0fdb', N'769980-8855', N'office', 2, CAST(N'2012-02-05T01:26:37.750' AS DateTime), CAST(N'1968-04-13T18:12:07.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8eecaea3-7759-9ca3-a148-204f780e7d7b', N'9f35a060-c7d0-d1b3-1208-69a512a72444', N'381-7569058', N'office', 1, CAST(N'1976-12-30T15:50:38.230' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'31d94f43-1535-360d-15d6-215f6252263b', N'2dfbc1e9-ba23-015b-593f-bb058eadade4', N'886-142-0792', N'cell', 1, CAST(N'1991-08-11T01:41:19.040' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'202deb84-4358-377c-ea6d-217b199f5c77', N'dc4abeda-2e99-6323-fb6b-8bb70b80adcb', N'976844-6642', N'home', 2, CAST(N'1961-10-27T08:14:48.130' AS DateTime), CAST(N'1989-05-29T13:59:35.160' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'820bec08-4015-969c-f75f-22b1ba28afea', N'144545ef-3967-de48-30d5-c35c5b55ab14', N'485595-4551', N'cell', 2, CAST(N'1988-03-14T21:16:30.800' AS DateTime), CAST(N'1953-08-14T21:15:54.190' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'43aafc6e-550b-7f87-4425-22ca334f59fa', N'892b2a46-e0c6-c3bf-3ee3-ac68e17888ba', N'375-8859132', NULL, 1, CAST(N'1994-09-22T14:18:45.000' AS DateTime), CAST(N'1986-07-04T18:32:44.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ada41f8a-7037-27be-8602-2350c2208f8f', N'1f55ef41-5151-ec41-f403-8a3e65b01044', N'025-1928563', N'cell', 1, CAST(N'1972-02-03T22:33:51.410' AS DateTime), CAST(N'1986-08-08T18:24:57.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3f8baa29-06ac-824c-d6d0-23c0af2f9396', N'c2830531-1a2a-eebd-01c2-3c325c736aa7', N'758-6090383', N'office', 1, CAST(N'1956-08-12T19:01:05.880' AS DateTime), CAST(N'1956-09-29T00:10:26.350' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'28dc6776-6be7-a40b-319f-23d171a0563d', N'b0c85e23-f239-e644-33f3-ff0a990233b9', N'173993-2903', N'cell', 3, CAST(N'1990-04-10T08:22:26.470' AS DateTime), CAST(N'2011-07-02T06:33:07.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'129242e8-2528-691a-7d24-23d66624ac77', N'a52d01db-4e04-7da6-0dbb-fa66076574e1', N'1629079819', N'cell', 1, CAST(N'1970-12-03T07:16:05.390' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'de23151a-f472-d667-36ef-23d857761f81', N'b7bb975e-d517-1ab4-ba67-36b40cb178d4', N'496-142-3420', N'cell', 2, CAST(N'1972-05-09T22:50:47.470' AS DateTime), CAST(N'1971-03-20T11:02:27.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'13978483-05d4-f6a4-3e4a-24175ede729c', N'1fd858ed-aadc-8821-b6a8-f04d2ae43181', N'926-268-2943', N'cell', 1, CAST(N'1961-12-09T08:42:51.690' AS DateTime), CAST(N'1999-07-12T04:58:44.990' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'54478260-94df-9302-f1cb-2458618ec2b9', N'492a3ac6-9b1c-efa7-0691-023a4c29b65a', N'680-334-6362', N'cell', 1, CAST(N'1983-03-02T11:03:05.170' AS DateTime), CAST(N'1994-09-26T14:34:08.370' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a2726896-dbae-0c3f-0bc1-249868b14309', N'cffa50c0-4169-fe95-c225-18ac8288aefc', N'6389302431', N'cell', 1, CAST(N'1997-07-19T02:21:58.810' AS DateTime), CAST(N'1955-12-12T13:32:18.000' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fb9cd3be-9aea-77a3-e031-24b685e2af9c', N'024ff35a-7e84-345d-ed2e-26c4e844cacf', N'0988981803', N'office', 1, CAST(N'1991-10-03T22:36:41.760' AS DateTime), CAST(N'1978-10-31T03:58:49.480' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e87ff50e-00ea-6a4a-b23c-251c61c279f4', N'3df98292-b8a4-d52b-4f9c-45939a4da247', N'715023-7852', N'cell', 1, CAST(N'2012-03-28T12:32:00.810' AS DateTime), CAST(N'2006-04-17T19:46:04.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'28ee2918-16db-d507-fddf-25434d25c14e', N'fd79c1f8-6ec3-4bc8-a606-a281b16e78a4', N'4277883887', N'cell', 1, CAST(N'1973-03-22T21:30:42.090' AS DateTime), CAST(N'2018-02-16T17:14:21.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f43b4ed6-23fb-8180-94c8-25a5f879568a', N'9a99c2cd-fcd3-bbd3-7893-e2bd95c4a0d9', N'452-549-5092', N'cell', 1, CAST(N'2011-07-29T07:13:08.670' AS DateTime), CAST(N'1991-01-30T17:33:17.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3c83d56c-f488-0da0-2e02-25c04b006133', N'bc37f7f3-ca42-9efc-8a5e-209ac495e1f9', N'375104-4300', N'cell', 2, CAST(N'1999-09-12T17:51:58.480' AS DateTime), CAST(N'2004-03-17T08:34:50.660' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5448e7ca-4385-9de8-554c-263d53dd8340', N'054e8c96-4c91-a5e0-e447-79e1d94a500e', N'089-7026039', N'cell', 2, CAST(N'2000-09-08T18:17:50.760' AS DateTime), CAST(N'1967-11-08T00:18:50.960' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'038c078d-a7c5-c0a5-8cd1-264564152bc6', N'63229630-a107-936a-bcbb-8d0b66ce2062', N'298-2132264', N'cell', 1, CAST(N'1986-11-19T19:55:08.090' AS DateTime), CAST(N'1989-12-11T00:34:03.670' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a5812d68-2a2e-bd82-995a-2646cb348198', N'57127af3-4c5c-4f57-7c5e-051702522d2d', N'724-037-4219', N'cell', 1, CAST(N'2006-05-07T05:38:28.970' AS DateTime), CAST(N'1954-05-11T22:02:12.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7eccdde9-323b-64e4-53dd-268396df6178', N'66a2ae7c-d3f9-80ee-081d-c5f419e71aec', N'8518426606', N'office', 1, CAST(N'1990-01-02T19:31:15.450' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bbfdaf16-8a7d-2a70-5872-26ce071c71c7', N'065317fd-3715-d48e-040e-014261e6112e', N'239-5784927', N'cell', 2, CAST(N'2013-02-06T16:31:15.630' AS DateTime), CAST(N'1988-09-30T08:43:13.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'74698bb3-a7eb-62dc-7d58-270af84428c7', N'2e16a7ba-e2a8-9ab8-afe4-940dbc5bf800', N'465-280-8807', N'office', 4, CAST(N'2013-07-17T02:35:13.390' AS DateTime), CAST(N'1968-01-18T20:52:09.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'692ada97-a8dd-bb06-936e-272200c072c3', N'5249d820-918e-e425-6ef8-12ba293b40c7', N'2424142550', N'cell', 1, CAST(N'2006-05-23T18:08:09.640' AS DateTime), CAST(N'1983-01-31T18:19:36.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c9fc50a8-2544-502b-256e-273e777bf7fe', N'0f2d6652-6892-db86-cb4b-a80ef25ad2db', N'0884767069', N'office', 1, CAST(N'1966-09-10T10:03:56.670' AS DateTime), CAST(N'1974-07-19T13:45:32.130' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6311f6f0-c64d-2270-1820-277a2a3672ab', N'd915bc8c-364f-a338-1d8a-d7c16ef7c834', N'8660805759', N'cell', 2, CAST(N'2009-03-13T06:27:44.270' AS DateTime), CAST(N'2009-03-22T02:08:50.180' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e8b98500-dbe2-226e-f471-27c032bd9781', N'410c0a01-8045-be85-51d4-46035f17c82c', N'613377-1041', NULL, 1, CAST(N'2003-05-02T21:11:40.610' AS DateTime), CAST(N'1989-08-26T10:48:24.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8d70ea39-daa0-de5e-007d-284f4873508c', N'6bb5fa3e-ac63-48ae-322e-3cb7c3794d9a', N'555-6808606', N'cell', 1, CAST(N'2003-01-12T09:42:19.190' AS DateTime), CAST(N'1991-11-22T15:42:59.120' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'07b84dc8-7d56-6ca4-9637-2864ee610ab1', N'1b2b8af2-897a-57af-cae0-bc1a01fff642', N'399-771-1124', N'office', 1, CAST(N'1953-04-25T22:13:09.440' AS DateTime), CAST(N'1977-05-04T22:43:43.660' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'57a25a20-3be1-bb9f-0eb4-28847b805d6a', N'0e0f1889-fbc2-8211-2455-297ee9dfa4b7', N'417-965-9484', N'cell', 2, CAST(N'2009-03-12T14:55:17.310' AS DateTime), CAST(N'1995-05-08T13:13:17.570' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'96853711-de73-551b-fb64-28a6e7a28207', N'8e377c8e-87fe-3a5c-23ef-cd28f95f95ab', N'421-603-8897', N'office', 2, CAST(N'2001-12-01T21:13:10.160' AS DateTime), CAST(N'1992-07-28T13:22:17.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9ba7f02c-a64a-afa2-93d4-292a917f5726', N'a4a15639-bd1a-729d-23ff-fa573529114a', N'5004872573', N'home', 1, CAST(N'1986-05-09T00:00:22.020' AS DateTime), CAST(N'1954-03-24T01:25:39.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a0564d28-9a30-e248-f6b7-293245c17cdb', N'5d39ff51-c531-f968-3e00-46063942c2dd', N'109895-4309', N'cell', 3, CAST(N'2007-03-09T12:52:21.870' AS DateTime), CAST(N'1976-01-06T04:10:54.970' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'41b05189-a9b6-8bf5-dcee-293a9398388e', N'a2630276-4fea-c275-cd44-7f843e669616', N'048902-3731', N'cell', 1, CAST(N'1982-01-27T14:00:28.100' AS DateTime), CAST(N'1965-03-24T02:32:17.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'92452b49-10f9-2630-d8f4-293aaab17b4e', N'bb9767c5-1598-bcd4-7c65-24dde030f7a3', N'6081315423', N'cell', 3, CAST(N'2002-05-14T16:07:43.060' AS DateTime), CAST(N'2007-06-06T20:44:38.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd7523256-fe3c-2d7a-5ebc-29c1df00aa84', N'96155c86-dd07-2b58-fceb-27c25da8661c', N'116-1926401', N'cell', 2, CAST(N'2017-06-06T01:21:06.350' AS DateTime), CAST(N'1972-04-15T17:40:04.950' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'68016c25-689a-b4d2-c82b-2a161e6dec2f', N'e002b7fd-e7dd-a8cb-1a42-55f912567e03', N'220-407-3792', N'cell', 1, CAST(N'1959-01-06T18:20:39.090' AS DateTime), CAST(N'1988-01-07T10:27:25.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b5954c0c-3dbf-5633-65be-2a1a6ec793c1', N'9dc0889b-b5e7-a83f-4cba-3f864551b8a8', N'2674678096', N'cell', 1, CAST(N'1979-10-02T13:20:09.730' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'163f0220-253f-8d10-e35e-2a3c61ffad63', N'58e389ad-b7f9-9b59-134d-91e8d8084ae2', N'703-7668855', N'cell', 2, CAST(N'2007-11-09T11:42:04.200' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2f8b99b0-6b2d-3290-b075-2a4971613380', N'2e017358-1fe7-316b-a1ed-38ed11e6ad18', N'564497-2217', N'home', 1, CAST(N'1969-10-26T05:55:56.070' AS DateTime), CAST(N'1987-02-03T13:42:11.560' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'46d31644-4347-8ecb-2555-2a63c4392e12', N'87ffbe87-3ce2-af34-ee4e-ce5399c59411', N'927-722-0743', N'cell', 1, CAST(N'1954-10-05T18:10:21.500' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bf8209ec-55c6-5f87-9ff6-2a799ed7a4f3', N'55a9f545-02d5-5e74-4736-1666e6bf05c7', N'109-038-6853', N'cell', 1, CAST(N'1958-01-11T16:44:14.090' AS DateTime), CAST(N'1990-05-16T01:47:21.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'70f2416b-0451-a90d-0a7e-2a931bf284c6', N'2ad28961-418b-91be-d710-0a7d2748bb70', N'837444-6229', N'office', 1, CAST(N'1957-07-11T12:07:42.290' AS DateTime), CAST(N'1992-06-23T14:48:50.670' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'999014dd-1f72-4e0a-bb4a-2a935c53cb06', N'936e8632-3643-fdd6-81d9-a70906a585f8', N'805-696-5353', N'cell', 1, CAST(N'1989-11-16T14:23:24.200' AS DateTime), CAST(N'2003-04-16T00:42:44.780' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6de9e12c-7b6b-4df3-d161-2b336814449d', N'd60fab43-03e9-f668-05bf-f5cb5ac554bb', N'201-258-9519', N'home', 1, CAST(N'2004-11-20T17:18:51.160' AS DateTime), CAST(N'1982-01-19T23:36:45.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c086033c-e2b8-cf14-e1ca-2b5cff53d999', N'cc9aeb04-0c61-4be0-88bd-6ab232daa1db', N'792-8594508', N'cell', 1, CAST(N'2008-05-11T07:41:55.710' AS DateTime), CAST(N'1993-06-14T17:18:23.370' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f95e3cfb-53b9-c60b-6be0-2b6821b793dd', N'c0152988-5e1b-7a2b-b743-5cd587cd68fa', N'2606106906', N'cell', 1, CAST(N'2006-12-16T22:51:34.000' AS DateTime), CAST(N'1973-08-28T00:37:57.360' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'12da242a-b986-8d31-4c13-2bb5e15f18b8', N'd2c8d433-aae9-0831-e030-4b891893abac', N'1797288865', N'cell', 2, CAST(N'2006-08-12T02:33:06.060' AS DateTime), CAST(N'2004-06-17T07:35:35.850' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0ada7d2a-7cce-54bd-036a-2beecf801573', N'88f1fc78-12be-8de9-1178-2ac4004a0165', N'895784-5536', N'office', 3, CAST(N'1998-02-16T23:19:57.850' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9e148256-1175-d0ed-ece8-2c3e57e2d1b3', N'bc37f7f3-ca42-9efc-8a5e-209ac495e1f9', N'7066157497', N'cell', 1, CAST(N'1957-07-02T01:03:47.420' AS DateTime), CAST(N'1971-09-23T06:16:14.820' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4a3769d4-ae91-ac97-0536-2c6abb8c470e', N'67fa10ae-9c6d-28ab-ca67-d9cbf16a7d58', N'996-776-4875', N'cell', 1, CAST(N'1981-04-22T12:50:28.000' AS DateTime), CAST(N'1981-07-15T13:45:53.850' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'05682aee-9856-268f-557e-2c72b6d99ec1', N'a3764951-94d6-3769-2b6d-95f3b9ef060e', N'204126-5566', N'office', 1, CAST(N'1992-08-09T14:33:25.520' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'151f4786-3ad6-1b81-78c2-2c7c4e3417cf', N'da9d5a58-1dc3-10ab-b22c-e68bc624678c', N'2996006437', N'cell', 2, CAST(N'1996-12-17T08:33:39.360' AS DateTime), CAST(N'1996-05-31T07:10:19.390' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'dacbc2f5-9c4a-750a-e7e3-2c7e8abd2542', N'0981e036-4a27-665c-9182-49d0bbe5f214', N'0670479873', N'office', 3, CAST(N'2017-05-29T20:21:09.460' AS DateTime), CAST(N'1990-02-21T01:17:28.610' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a5afc2b7-4914-d687-488e-2c8b9d49d4ce', N'b31a758e-e0c6-7886-5394-e1d4060f3514', N'857-190-0538', NULL, 4, CAST(N'2013-08-08T16:59:47.210' AS DateTime), CAST(N'2011-11-15T21:07:03.890' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c787fc20-50b3-c6f0-af79-2c8cf4a688ad', N'6a134942-7954-a383-ab8d-ca2e5245c2eb', N'865-637-6163', N'home', 2, CAST(N'1978-06-19T20:31:04.540' AS DateTime), CAST(N'2000-02-16T19:20:24.970' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd5925a3a-1e17-c415-3806-2cbe61d0d1bb', N'49849363-2487-4351-0761-57f581b71e46', N'055302-4647', NULL, 1, CAST(N'1961-08-13T03:06:34.790' AS DateTime), CAST(N'2018-06-24T03:53:33.130' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9d116329-8966-5fc0-dcf3-2cdf9f26f318', N'5d39ff51-c531-f968-3e00-46063942c2dd', N'448372-5328', N'home', 2, CAST(N'2003-12-18T03:40:15.690' AS DateTime), CAST(N'1999-02-17T23:29:13.260' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'858c0238-5102-0dc2-f43a-2d05c4716445', N'689f7e8d-bd65-eac6-a94a-9fbe8dd83dcd', N'7677263651', NULL, 1, CAST(N'1971-03-02T04:54:21.750' AS DateTime), CAST(N'2015-12-10T05:07:16.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'921392dd-3055-da23-bfc6-2da319c915f3', N'e4d1afbe-a489-c507-3a73-43fa6d38b456', N'8358452605', N'cell', 1, CAST(N'1963-07-29T11:20:47.690' AS DateTime), CAST(N'1971-05-20T13:48:48.790' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c1d801a8-7d88-5c2a-34a3-2da5907065fc', N'9a310bf8-44d1-f18d-7f02-ca15b80d8d91', N'055-0105696', N'office', 1, CAST(N'1960-12-26T00:04:25.010' AS DateTime), CAST(N'1998-10-04T01:34:08.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9b130bb1-dd62-b7c4-4ae2-2dcbb58cd8c4', N'a3764951-94d6-3769-2b6d-95f3b9ef060e', N'803079-1684', NULL, 3, CAST(N'2005-11-12T00:10:14.940' AS DateTime), CAST(N'1980-11-03T19:36:57.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7d2c77bc-ef85-010f-fff8-2de64a5667d7', N'144545ef-3967-de48-30d5-c35c5b55ab14', N'352738-0437', N'home', 1, CAST(N'1965-11-28T16:28:27.250' AS DateTime), CAST(N'1984-07-15T19:00:23.190' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'03f93fc3-eb53-bbd7-e159-2e9ea4c70ee0', N'b4e88620-d85f-7a9a-22c1-0a8698ddc2d8', N'9306126332', N'home', 1, CAST(N'1975-07-02T18:51:43.130' AS DateTime), CAST(N'2009-02-18T12:37:55.670' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd8f7eae6-c8dd-0691-44b4-2ea3167350a2', N'5edb671a-df8a-ef63-3cb8-9c7f67954c55', N'869-5391976', N'cell', 5, CAST(N'2018-09-28T06:41:13.720' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'de45d7aa-87fe-025a-be6b-2f26b1107d7c', N'c82c5122-d676-13ee-520d-e4f2d2e683b9', N'946-869-9569', N'cell', 3, CAST(N'1983-07-07T22:13:28.730' AS DateTime), CAST(N'1992-01-12T04:57:14.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'556acdce-4922-4c21-6744-2f2feca8ce01', N'60d3a9f5-f655-dfb0-e281-9a60036890dd', N'721-3461501', N'cell', 1, CAST(N'1974-08-13T08:24:27.570' AS DateTime), CAST(N'1957-04-01T04:06:06.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3d0ddf53-29cc-c70c-fd60-2f4e46a9ab26', N'520ae4eb-91e7-cea0-29d6-3d5684e510aa', N'6575921929', N'cell', 1, CAST(N'1968-09-17T00:41:13.430' AS DateTime), CAST(N'1957-08-01T21:05:04.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'80affe51-8ed1-ef24-5fd9-2f52fedb4110', N'c62f0a66-2f74-2019-b20b-def8171ce234', N'031-3780795', NULL, 1, CAST(N'1967-12-08T07:16:23.670' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0fc1dae1-e377-503f-9a94-2f637f72170d', N'09c3feea-fb6a-069b-7d99-585e122827a0', N'797-581-2460', NULL, 1, CAST(N'1970-05-11T05:07:28.300' AS DateTime), CAST(N'1963-10-23T00:33:39.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b54de31d-ee25-0c94-a9d3-2f63879f31f2', N'612cc783-bd84-acd9-1f2e-93bad43a660b', N'067-1675933', N'office', 2, CAST(N'2000-01-15T09:34:53.350' AS DateTime), CAST(N'1978-01-25T13:15:15.980' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'917a4a8b-9e59-eddb-5403-2faac325a4fd', N'd36b7c76-451a-fd0c-f9ab-761d17a5e66a', N'926-1054571', N'cell', 1, CAST(N'1962-08-01T05:09:17.750' AS DateTime), CAST(N'1984-10-26T20:56:51.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a99c4d43-7db2-eab1-7515-2fd94352688c', N'7d595f81-538e-dae6-49ec-6ccfeff52111', N'287-396-9046', N'cell', 1, CAST(N'1983-04-19T23:32:49.620' AS DateTime), CAST(N'1997-08-11T13:26:35.260' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6a8f6f31-0f44-84b8-a4ae-2fe4b0230aac', N'a155709e-9aec-1e48-2059-e5fecbc361b1', N'545-4489194', N'home', 1, CAST(N'1967-07-26T01:20:06.400' AS DateTime), CAST(N'1981-01-10T17:52:41.360' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'de7f4c98-53c7-bf22-411d-3052a94ef581', N'eeab3c7a-bf3c-aca8-64cf-57493bc611ec', N'935442-5477', N'cell', 1, CAST(N'2013-12-09T17:16:41.820' AS DateTime), CAST(N'2012-06-19T23:26:28.870' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'26236eb0-6e4b-a84c-31c8-3063e8f1f354', N'afe99c8f-790f-c153-21e5-2de28bc8835a', N'938112-8523', N'office', 2, CAST(N'1959-10-18T06:09:09.120' AS DateTime), CAST(N'1991-03-30T03:00:12.500' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0673e352-e706-9ed3-4aa3-3087b19a60e1', N'eac65501-21f5-f831-fb07-dcfead50d1d9', N'590-630-0475', N'office', 1, CAST(N'1965-11-23T14:55:53.690' AS DateTime), CAST(N'1992-10-29T20:34:56.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b4391f4d-e4f8-0873-c33d-30ae21e4b5d3', N'31912448-aede-f726-aeba-2025c64cddfb', N'330-0226197', N'home', 3, CAST(N'2016-10-21T13:26:29.570' AS DateTime), CAST(N'1983-06-04T14:56:00.390' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b54082e8-f561-545c-d6d3-30baa78f8e56', N'290f0c2f-5511-8f22-31d8-0effb9fcbe5e', N'707536-1051', N'cell', 1, CAST(N'1996-09-20T21:30:52.710' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bd347156-9c64-9cfa-0001-30e8f6a42579', N'b430903d-007a-b3e0-6d38-10f39253a78d', N'9024278327', N'cell', 1, CAST(N'1998-10-14T08:21:05.870' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e0c495ad-03a2-573a-b586-30f77ca9d578', N'aff410dd-7d47-aaa0-0d93-0eecad577aab', N'7028020550', N'office', 1, CAST(N'1985-01-26T16:24:46.910' AS DateTime), CAST(N'1965-02-02T14:29:42.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a64d54f4-8de7-16a9-2c1d-31058ab5eb07', N'f962ca02-7f68-c467-c1da-a8051185294b', N'278107-6585', N'cell', 1, CAST(N'1971-10-04T12:33:49.860' AS DateTime), CAST(N'1978-04-27T06:46:12.000' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bc083564-902d-6929-4a5f-3120d0280c9d', N'37b96da1-dffe-006c-af19-41a9b7111bb7', N'456987-9606', N'cell', 2, CAST(N'2005-12-23T20:01:24.240' AS DateTime), CAST(N'1981-08-11T14:47:16.740' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2f12acf0-5b1e-f850-ffff-31269c6e8f59', N'36517c9d-07a1-974c-a8af-b84e9b7a069c', N'036157-8860', NULL, 1, CAST(N'2001-06-23T13:57:10.500' AS DateTime), CAST(N'1969-09-05T10:47:35.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a89311ab-8d6f-75d9-eefb-31488f8cdf23', N'a4074fb6-b8a0-0bd6-2b9c-f62fc9a05a0d', N'782-3094664', N'home', 2, CAST(N'1974-02-12T04:17:46.600' AS DateTime), CAST(N'2009-08-04T22:27:14.720' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'85d6b4b2-0c36-40b9-835b-31614e0d3da7', N'5e4cd648-8222-65b9-d788-5274cb220b75', N'535-5875271', N'cell', 1, CAST(N'1969-10-16T14:26:23.190' AS DateTime), CAST(N'1979-12-19T15:56:14.960' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'17c1af2d-538e-17fe-233d-316152104945', N'0a626034-b069-7c62-1016-b9db40bdf875', N'104-7887056', N'cell', 1, CAST(N'1970-04-11T01:49:48.050' AS DateTime), CAST(N'1978-04-30T23:55:31.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'52697baa-9671-d7ed-cd80-31f5273dd5a4', N'4ee35e35-b543-f8ac-52cc-0c55fac33999', N'379-828-9925', NULL, 1, CAST(N'2012-06-17T20:52:56.020' AS DateTime), CAST(N'1981-06-04T13:12:45.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9efd29a3-bc6c-7cfb-eae5-326dc3c31dc3', N'8378a9eb-6301-7d9b-4474-9bb6edbe0645', N'307-7242164', N'office', 1, CAST(N'2009-10-30T03:57:35.790' AS DateTime), CAST(N'1982-07-05T21:19:04.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'236a812a-ff7a-a81d-685e-329df792de26', N'ffb5a984-b061-45db-9a6b-2484cd0af2a7', N'279-000-1098', N'home', 2, CAST(N'1969-06-12T21:39:31.440' AS DateTime), CAST(N'1954-10-31T08:25:22.980' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e1428def-fe33-9877-d115-33299e1f4d61', N'2b57d3ff-d91a-97b5-d810-1947115a878f', N'271144-3612', N'home', 1, CAST(N'1960-03-21T12:45:06.860' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'00fb0e28-21ca-653e-a127-334f24c9672b', N'3df7d613-77ee-f6f9-53a8-ff08a7bffb7f', N'793-326-7205', NULL, 1, CAST(N'1963-08-10T13:42:13.930' AS DateTime), CAST(N'1988-04-19T07:13:50.220' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3df2b220-766e-946a-012c-3378c34c2203', N'ee978c7b-83ef-0001-b027-52d56b8db100', N'049542-9347', N'cell', 3, CAST(N'1997-07-26T06:55:41.680' AS DateTime), CAST(N'2003-05-22T06:28:54.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'afbf2dc9-5f6a-5618-c568-33a503a5cf47', N'd0ad0f6d-eb58-e178-d5c1-36a5e8607c78', N'787-4913855', N'cell', 1, CAST(N'1957-12-12T19:20:07.670' AS DateTime), CAST(N'1973-08-24T15:42:17.700' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bff34a09-12de-d982-0e95-340cfc5db0fb', N'fe5cb64b-b0c8-5883-0700-b67d8ff937cf', N'3749802505', NULL, 2, CAST(N'2002-08-10T21:54:21.290' AS DateTime), CAST(N'1962-06-22T08:30:06.680' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c65773e0-4208-e871-46ef-34bf19b0cde2', N'78780482-51e3-7979-2a01-8356b1874f8c', N'910-6503913', N'office', 3, CAST(N'2017-03-26T10:22:28.510' AS DateTime), CAST(N'1974-06-08T23:46:23.820' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8592fc61-6400-07ca-fe83-34ee3ef06ad4', N'814cc60a-4ca9-879f-1e33-e1a70d68cb73', N'149-8103090', N'home', 3, CAST(N'1997-01-26T09:57:23.140' AS DateTime), CAST(N'1979-10-25T05:59:02.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b6c50e19-7749-739a-9b14-35421e4067f5', N'8c4569b0-ef7f-4705-820b-8032a5ca2dbf', N'6650614000', NULL, 2, CAST(N'1956-01-05T16:02:30.220' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c766932c-08c9-7c30-8485-3564a23aa411', N'a570a175-30b6-9008-b001-198e61f51a37', N'172-8711635', N'home', 1, CAST(N'1969-03-26T02:24:16.800' AS DateTime), CAST(N'1989-09-16T03:27:44.480' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'81b0ec2b-b755-9ed9-3a36-359a668b94b6', N'24e620b9-1b9f-b3df-b10e-a85ab59222d6', N'821219-5408', N'office', 1, CAST(N'2008-09-28T23:49:45.510' AS DateTime), CAST(N'1962-08-28T19:57:14.310' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'735d1b0a-78f9-f8fd-42ef-35a52e5a4f70', N'6a134942-7954-a383-ab8d-ca2e5245c2eb', N'421215-2995', N'cell', 1, CAST(N'1954-08-23T00:18:06.620' AS DateTime), CAST(N'2016-08-18T21:59:05.680' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'637a2293-76d4-e157-590f-35aa7f96754e', N'42eb918f-7cff-f324-e511-aae9c3d8b2e6', N'487127-1728', N'cell', 2, CAST(N'2011-05-13T07:54:29.090' AS DateTime), CAST(N'2000-01-14T15:58:10.420' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'90b2fa22-2c72-ee50-4afd-35b384468e25', N'b852f4eb-0a26-57a1-0f03-170fcc676839', N'799397-3090', N'cell', 1, CAST(N'2003-03-31T05:31:55.400' AS DateTime), CAST(N'1954-01-16T10:55:26.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2fcc444e-0339-64ab-c97e-35fcddd703f6', N'd2b4d74f-356d-4310-3eb5-b0d04a8bb09e', N'8247399938', N'cell', 1, CAST(N'2004-03-02T03:21:55.760' AS DateTime), CAST(N'1974-12-25T14:17:26.730' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5ff3c89c-8f70-7cd9-256b-3645dc186b59', N'562cc6c3-d3fe-c093-5347-fb67c2c881c3', N'3847464028', N'cell', 1, CAST(N'1981-01-16T12:55:32.840' AS DateTime), CAST(N'2001-05-08T12:25:12.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'05b6d086-8efc-d6e2-b405-364753dc90f3', N'1a3f3055-4baf-c489-8544-19a64eb833f7', N'617946-7195', NULL, 1, CAST(N'1971-09-18T22:34:55.090' AS DateTime), CAST(N'2007-04-04T21:50:21.570' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b89a7e50-fc39-fd0c-a0e3-369f36a09561', N'b0dfcb7c-96d7-a1d9-6d60-6ae0133e2e92', N'690-621-4989', N'office', 2, CAST(N'1984-07-29T21:00:07.500' AS DateTime), CAST(N'1965-09-12T02:55:07.480' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7eacb40e-3bdf-07c4-3043-36a05c922026', N'ae3b0719-2461-fc30-820b-13425696571b', N'399-4282904', N'cell', 2, CAST(N'1991-12-22T02:09:18.720' AS DateTime), CAST(N'1956-09-18T08:59:31.400' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f37aaf3b-5de5-d124-6dd9-36e2bfaa28a9', N'a7a51730-c380-57b2-91a5-84f8138327d0', N'899-4893489', NULL, 1, CAST(N'1998-10-23T03:00:52.290' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2f7e28c5-ce9c-b843-2db2-36f8f4ee6d8e', N'd89d1123-7e29-55c1-55f0-75a0b2abf8f3', N'941849-8613', NULL, 1, CAST(N'2000-06-19T12:20:55.680' AS DateTime), CAST(N'1968-03-11T11:31:44.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'447a8a14-9d80-4f0a-ab75-371a06e17162', N'08e1094f-d225-4c1c-74e2-d1d9fd9d7939', N'194-231-2492', NULL, 1, CAST(N'2014-10-26T21:00:40.800' AS DateTime), CAST(N'1989-06-08T05:15:29.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a5d86e04-a1ce-81a8-62e9-379aaf1ac2ee', N'bfb1c6c7-3eac-3ba0-0842-c15393817101', N'9532979488', N'office', 2, CAST(N'1997-08-16T05:22:03.360' AS DateTime), CAST(N'1956-12-18T10:33:40.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'20e3d21f-87af-2ded-1887-3837c23f5dee', N'19499383-1d5b-b027-ffad-a27383d2aa9e', N'086-486-4565', N'home', 1, CAST(N'1956-08-27T17:12:01.700' AS DateTime), CAST(N'2010-07-24T19:24:07.980' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cfc96514-5949-331e-fc4b-3847da0b960f', N'd59bd736-8e42-8101-1c40-128517855cb7', N'6287691927', N'cell', 1, CAST(N'2009-11-09T13:56:24.490' AS DateTime), CAST(N'2009-09-02T07:17:47.870' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2298acd5-2fb0-08d7-8aa8-387e18c6456c', N'8f157a23-af8a-ba68-9273-bdd79572f3af', N'9501157766', N'cell', 3, CAST(N'1970-02-18T11:04:05.640' AS DateTime), CAST(N'2014-04-07T05:23:13.070' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fbacdd53-f55c-c579-1980-38c8992782c0', N'7c78408b-9c6e-7467-46c7-b35aa3f3981b', N'174-6254467', N'cell', 2, CAST(N'2012-11-29T04:05:31.860' AS DateTime), CAST(N'1994-12-06T02:07:17.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'787aadc6-3097-077a-fdcb-38dbb9cec54a', N'0c8ebcc7-79f4-62b5-e348-a618599b5a19', N'539-8867131', N'cell', 2, CAST(N'2016-02-19T11:08:20.450' AS DateTime), CAST(N'1968-02-24T11:49:10.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'695baa42-626b-1267-c66d-39189176b65a', N'10c921a2-393f-d73d-bd8d-1eef0ad852e9', N'776-9044097', N'cell', 2, CAST(N'2010-01-27T08:41:51.750' AS DateTime), CAST(N'2012-09-06T19:09:59.730' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'deb80997-2543-75f1-56b5-39266f92713f', N'fe2069ea-a9b4-2259-8d50-3fcc75eb5631', N'2678822697', N'office', 1, CAST(N'2011-11-03T20:28:25.700' AS DateTime), CAST(N'1968-05-20T12:08:34.680' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'639e7c75-0d29-422c-8368-3970ec1262d9', N'4774734b-48d5-713d-5d36-a963453164be', N'880-1668383', N'office', 1, CAST(N'1957-09-08T22:54:20.310' AS DateTime), CAST(N'2011-05-01T10:50:05.860' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4eac9c18-6fdd-dd22-ab5f-39a5e1bd7f5d', N'85e9a700-7843-d2da-3040-816b7e6b409e', N'4171360190', N'cell', 2, CAST(N'1986-07-26T06:32:57.660' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ba0782f2-ba33-0e45-dd3f-39c821d9df31', N'899e4fc5-4810-0f60-ddd3-5b3db85a02c3', N'7879354717', N'cell', 1, CAST(N'1955-05-17T18:40:20.750' AS DateTime), CAST(N'1986-10-01T03:54:45.660' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a3e9319e-1171-3b1d-2af8-39f49264ac13', N'a4bc23a3-c170-c394-0372-4bd6d287f333', N'454-1916864', N'cell', 1, CAST(N'1998-09-08T02:36:22.520' AS DateTime), CAST(N'2001-01-23T12:28:02.960' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'87704740-5fe3-bcf0-2b3d-39fd6673e939', N'0981e036-4a27-665c-9182-49d0bbe5f214', N'139-8856911', N'office', 2, CAST(N'2014-06-27T11:09:24.190' AS DateTime), CAST(N'1976-04-19T17:11:10.010' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'41ef61b3-9245-58ea-4765-3a4ba085d512', N'21fc5cc9-b5db-2fd5-870c-de8b19c15690', N'466-076-3540', N'cell', 1, CAST(N'1985-01-05T09:57:49.590' AS DateTime), CAST(N'1970-09-12T03:29:24.850' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'191f4642-9429-44d7-2f5d-3a8a09b10408', N'701e9dc9-7670-2d31-acfd-372bab0805c6', N'578888-5551', NULL, 3, CAST(N'2012-10-03T15:10:42.030' AS DateTime), CAST(N'1974-07-24T19:12:32.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b55b0af9-2259-a896-6058-3aa767a37dc3', N'f18f017d-04da-3bf3-7cc4-76f4f5d5eede', N'5321581111', N'home', 1, CAST(N'1959-10-07T21:34:16.690' AS DateTime), CAST(N'1966-10-28T21:09:54.540' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f59720cf-a698-ca00-8ed9-3ac0354cb2b1', N'701e9dc9-7670-2d31-acfd-372bab0805c6', N'830-9590013', NULL, 1, CAST(N'1965-07-18T19:00:17.780' AS DateTime), CAST(N'1976-12-25T22:36:17.420' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'63c30fd2-2586-259b-1db0-3b0e743df01f', N'2d4bcb31-4795-2aea-0b28-7a8722ab6410', N'835599-1107', NULL, 3, CAST(N'1978-01-14T17:15:33.270' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'361f6c27-06d2-cc1d-2e79-3b6af4775ccc', N'e7aad64f-04ed-c896-5891-b0ed24e22a6a', N'957778-9418', N'home', 1, CAST(N'1976-10-07T11:52:13.060' AS DateTime), CAST(N'2005-02-10T13:20:40.660' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3e4c0335-caa0-2be5-fa11-3ba1e748fca2', N'79e47169-5a21-b8bb-125b-f8b30afe66b7', N'857-7721454', NULL, 1, CAST(N'1981-03-13T17:04:38.310' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c1277445-9587-2191-7e75-3ba53b8c581b', N'21b48d96-1e73-8dbc-accf-93e294bd5e35', N'1227672553', N'office', 1, CAST(N'1990-01-18T11:45:56.710' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0bc7654c-c263-85e7-2b8c-3baef66d48b9', N'492a3ac6-9b1c-efa7-0691-023a4c29b65a', N'674732-3845', N'office', 2, CAST(N'1991-08-09T05:44:54.650' AS DateTime), CAST(N'2007-04-03T02:24:52.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c301cbda-5d5b-e929-a020-3c564660b63f', N'29b9897b-5d05-c5a5-c81e-cf9d76e2983a', N'874-9410994', N'cell', 3, CAST(N'2002-03-11T20:40:34.800' AS DateTime), CAST(N'1971-04-28T10:27:44.120' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4b45a0d6-32a9-5a69-9649-3c838386f5c0', N'a1c66f30-4e03-f513-9899-239f0268601f', N'663-6829734', N'cell', 2, CAST(N'1961-03-03T00:09:55.350' AS DateTime), CAST(N'1978-08-25T12:38:20.820' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ad7e1e2f-2361-b0c7-a19e-3c8ca2acb2da', N'b7bb975e-d517-1ab4-ba67-36b40cb178d4', N'566-1391204', N'cell', 1, CAST(N'1968-02-06T01:48:55.770' AS DateTime), CAST(N'1991-08-08T20:13:10.190' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd003ac5d-b274-1676-5480-3d120021a1da', N'ecefd93e-795f-216e-ee89-f8d6d162e89d', N'136-0346415', N'cell', 1, CAST(N'1960-08-23T10:03:25.820' AS DateTime), CAST(N'1968-05-13T01:07:34.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7dba95d0-e4ef-06c7-008e-3dd14da95bc3', N'158e9183-ed1d-b5cf-c032-6b4e4eb650b1', N'226-1214647', N'office', 1, CAST(N'1959-11-30T07:13:22.160' AS DateTime), CAST(N'1983-02-15T00:02:56.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'eb26e883-ac86-8973-70c2-3f69658f418a', N'22bcea00-07b2-52f2-64e0-4651a3a0aaf1', N'8353118441', NULL, 1, CAST(N'1974-06-18T13:35:31.210' AS DateTime), CAST(N'2007-11-08T02:50:00.370' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'69db16fa-ded2-9688-548b-3f80d1f5ff86', N'282452aa-edba-3df3-2034-aa055aa57c3a', N'935-7475306', N'cell', 1, CAST(N'1975-06-30T17:11:07.400' AS DateTime), CAST(N'1967-05-23T17:58:48.860' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c2c1786c-0eb2-4209-2d65-3ff0f9d8cf92', N'6672ab2e-6fd6-ea33-71cc-00255a13d888', N'981-6902844', N'office', 2, CAST(N'1992-08-18T16:04:48.400' AS DateTime), CAST(N'1979-11-30T16:47:32.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'075d09ab-49cd-8be8-b550-402ee3834518', N'52a23ffb-91ad-bcc0-e93b-58abef6b787a', N'475-0325815', N'cell', 1, CAST(N'1980-02-24T15:10:30.000' AS DateTime), CAST(N'1980-03-15T02:28:08.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd4b155b5-ab24-3286-4299-406d52fb402b', N'654841c8-e445-7806-4615-18e9b16d22aa', N'030266-2342', NULL, 1, CAST(N'1999-06-02T16:04:08.480' AS DateTime), CAST(N'1958-05-31T06:03:50.700' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ea318ff0-4a99-45c8-2435-4077fcfe0b72', N'814cc60a-4ca9-879f-1e33-e1a70d68cb73', N'714627-5721', N'cell', 4, CAST(N'2004-03-24T22:17:41.220' AS DateTime), CAST(N'1965-07-14T12:00:48.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a7fc18b1-46af-0843-7185-407a6cfb0ec1', N'e4d62b47-6199-5876-04b0-31d22e513af9', N'029-544-8121', NULL, 2, CAST(N'1985-01-25T18:09:36.890' AS DateTime), CAST(N'1981-06-23T11:55:00.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b11876df-c9f7-779a-bf69-407c4e513578', N'e4f28c7c-a018-1ac3-ad93-1e224f10afb7', N'1420700403', N'cell', 2, CAST(N'2006-01-05T17:05:11.860' AS DateTime), CAST(N'1988-08-15T13:25:13.570' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2a5707e1-f6ce-c25e-2f6f-416c002c7a85', N'198eaea2-9578-ce16-4cf6-000bbbd22af3', N'403-3738398', N'cell', 1, CAST(N'1954-03-22T12:49:42.470' AS DateTime), CAST(N'1990-06-15T12:44:07.210' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6b192284-8242-5943-7590-416d295d7de2', N'2fdf7e9a-4032-3a1e-8a39-2163057c641e', N'500975-8829', N'cell', 1, CAST(N'1980-08-11T11:33:59.230' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'771fa47d-4b2c-1a41-a8e6-4189ba2d22c3', N'95287c22-6e3e-204b-d430-b5b47ebb222e', N'9750940430', N'cell', 1, CAST(N'1955-04-13T00:59:47.540' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cc4d7dcb-4678-d997-a203-418dd2c8129e', N'4d5ca1f6-65d6-d10a-33f1-ab558d0b9de4', N'574361-3414', N'home', 1, CAST(N'1953-04-01T03:37:44.440' AS DateTime), CAST(N'2003-06-26T00:51:18.520' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bf004158-1cce-99b4-9194-41b077d3361c', N'b10b94f0-7bcf-45df-f1c0-bc14b274f5f6', N'761568-8511', N'cell', 3, CAST(N'1995-06-23T16:14:58.130' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'be893c02-4129-ad52-d704-420bc5d2ba5d', N'66c0f6eb-a0fb-7f40-4b11-fbfb4e9f8299', N'404735-0604', N'cell', 1, CAST(N'1961-11-19T07:50:38.630' AS DateTime), CAST(N'2001-09-20T11:44:13.470' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'565d773c-fc68-6863-9a20-430b880c60b9', N'844db8de-7660-557f-18e6-1dd52e973730', N'9173487173', NULL, 1, CAST(N'2014-05-13T00:40:17.350' AS DateTime), CAST(N'1999-09-27T06:12:58.790' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'15794079-ef21-1778-79ab-436426e086b0', N'3bc73d83-5d9a-a9f6-2bb9-530d31d455b2', N'322294-6815', N'cell', 1, CAST(N'1958-06-18T06:53:18.350' AS DateTime), CAST(N'1992-09-11T21:03:25.500' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'20957b8e-d347-4d87-94f5-4391f380a928', N'6f8a8be9-36c6-87e5-bcfb-c4bbbc493fe9', N'349-8399159', N'office', 2, CAST(N'1978-08-01T13:39:15.430' AS DateTime), CAST(N'1979-10-12T15:10:20.780' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd33860fe-66d6-fdc4-ec52-43e25e3cdd19', N'd838f4e1-63dd-7fcd-f1ca-79d86c7b894a', N'314220-6146', N'office', 1, CAST(N'2017-03-05T21:40:26.790' AS DateTime), CAST(N'1996-04-09T05:09:04.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'97cc7c68-c9ec-ac0e-e7e5-43f086e92c17', N'bfb1c6c7-3eac-3ba0-0842-c15393817101', N'169-537-2163', N'cell', 1, CAST(N'1965-07-16T04:29:21.420' AS DateTime), CAST(N'1953-08-16T18:22:42.140' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5e10f947-4afb-44e6-9131-447c8bf27293', N'0c8ebcc7-79f4-62b5-e348-a618599b5a19', N'227745-4658', N'cell', 1, CAST(N'1994-05-26T12:21:00.760' AS DateTime), CAST(N'1974-10-14T10:24:52.950' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'56f23513-d407-a68e-7fcb-4488682eee2c', N'd2c8d433-aae9-0831-e030-4b891893abac', N'120-7159700', N'cell', 1, CAST(N'1960-06-11T23:28:16.750' AS DateTime), CAST(N'1989-04-14T08:42:19.050' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b3667e3d-1317-44e6-8d8e-460544a98597', N'd25514d0-a69c-f268-7305-63d033e90782', N'055-5368423', NULL, 2, CAST(N'2017-08-19T20:28:17.910' AS DateTime), CAST(N'1977-08-21T13:53:13.480' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'870eb9cb-b69c-d7fb-1f12-46464f811e12', N'1f55ef41-5151-ec41-f403-8a3e65b01044', N'292-9814342', NULL, 5, CAST(N'2015-12-11T14:29:03.230' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'77e1722d-1b8f-f1c0-d620-46a463f3a629', N'0c48d959-cc13-c5df-d1c9-9e359906f8f2', N'900-223-7119', N'office', 1, CAST(N'1960-11-06T13:01:39.870' AS DateTime), CAST(N'1965-08-06T16:17:33.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'83d3b6da-ce6a-f63b-c46c-46c435d4bcc0', N'561436da-99bc-f1a1-334d-5160c04a03ed', N'440-888-4519', N'cell', 2, CAST(N'1997-06-17T09:03:54.450' AS DateTime), CAST(N'2005-06-11T11:06:08.480' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'69367f2c-c71d-4ebe-5cf3-46c87dc853e0', N'db73590e-81b8-c89d-5626-846d5aaf1ea8', N'242-6498887', N'cell', 1, CAST(N'2016-02-09T00:41:12.630' AS DateTime), CAST(N'1996-12-07T17:13:39.650' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'05561c80-d247-ad0c-a8d1-46d0eff38e57', N'0e884b8c-93a6-2beb-2d30-db7347381e77', N'171-1356156', N'office', 1, CAST(N'1980-05-30T11:55:54.030' AS DateTime), CAST(N'1975-07-10T11:31:14.790' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b84095c9-6381-1227-1ed9-472ee3264eda', N'420d6a7e-7be8-bfc3-be5b-694c59d43cb7', N'176015-1748', N'cell', 2, CAST(N'1991-05-19T16:44:36.110' AS DateTime), CAST(N'2012-08-21T21:43:38.400' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'aef6b2f9-1177-2eb0-f1ed-47a3765a469d', N'3d7d0599-0b67-3d94-c701-9658be88d3d0', N'139-0702303', NULL, 1, CAST(N'2017-03-12T13:56:17.400' AS DateTime), CAST(N'1994-08-30T07:20:03.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2ce5a125-3928-4d58-7d92-484d11cfea8f', N'2ad28961-418b-91be-d710-0a7d2748bb70', N'369-383-5591', N'cell', 2, CAST(N'2000-06-04T11:10:33.890' AS DateTime), CAST(N'1964-09-09T07:16:52.490' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fc646875-c088-0be0-6a77-487157efdbd5', N'62f21f55-b3bf-217a-8697-8070f966923c', N'625799-9108', N'office', 1, CAST(N'2003-08-16T08:32:48.980' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9d7aaf44-95ea-c8b9-523c-487f8133825d', N'5e82f887-d8f8-2c3f-0b40-b7e72d446bad', N'318-813-0322', N'cell', 2, CAST(N'2004-02-03T08:38:36.680' AS DateTime), CAST(N'2016-12-22T14:59:18.500' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ad410a46-9537-77e6-81a1-48bc6a87e2f5', N'd2776a00-d552-c5f5-225a-d3e57b728159', N'477766-3433', N'cell', 1, CAST(N'1991-07-25T19:15:56.160' AS DateTime), CAST(N'1971-10-09T07:58:57.490' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'38c3040c-53f4-7445-2eaf-48d8d45db231', N'afe99c8f-790f-c153-21e5-2de28bc8835a', N'541029-6598', N'cell', 3, CAST(N'1997-03-02T00:23:18.760' AS DateTime), CAST(N'2005-09-04T04:35:36.300' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c5e6e36c-ecc6-f921-c8a5-4930ebc37ebd', N'11cfe3d2-427a-beb9-fa0e-b8b3cc148409', N'212-292-6518', N'home', 2, CAST(N'2013-04-08T21:50:44.070' AS DateTime), CAST(N'2000-05-10T21:33:52.130' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a9372e53-ecea-8e0d-a58b-495d4205ac79', N'79005d3c-a544-9cc0-1ad7-2616b8c5bfdc', N'775538-7510', NULL, 2, CAST(N'1983-07-19T02:57:04.270' AS DateTime), CAST(N'1976-02-07T14:21:41.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bc6c8924-f29e-fe3e-a596-4a3d91629c9f', N'168ac464-d503-88b8-8492-db391d28c6d5', N'694-2337400', N'cell', 2, CAST(N'1961-11-16T05:32:02.590' AS DateTime), CAST(N'1981-08-03T18:35:58.500' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f0747406-4074-cdfb-89e1-4a82a4aa5e77', N'4774734b-48d5-713d-5d36-a963453164be', N'000710-5166', N'office', 4, CAST(N'2003-07-18T00:50:38.870' AS DateTime), CAST(N'1972-03-05T12:09:18.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e0ef356d-5a32-3a45-ccc5-4aa581f68bb0', N'8fa323fd-895f-cf19-c73d-c3405f74bdce', N'663130-0489', N'home', 1, CAST(N'1954-06-23T13:31:18.630' AS DateTime), CAST(N'2004-02-19T04:46:15.950' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8297d6fb-eb81-d246-d9b4-4ac2e33eb08b', N'a52d01db-4e04-7da6-0dbb-fa66076574e1', N'669-3755492', N'home', 2, CAST(N'1975-07-29T09:26:12.530' AS DateTime), CAST(N'1962-09-17T15:13:28.700' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'65bca03f-4739-ac0a-6834-4b78e15fac82', N'8c4569b0-ef7f-4705-820b-8032a5ca2dbf', N'092629-1466', N'office', 1, CAST(N'1955-03-12T22:51:33.360' AS DateTime), CAST(N'2001-05-22T18:13:59.100' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd6057842-cc6d-6deb-3422-4b85766ac787', N'f337e1a3-45d5-33b7-9a6f-3b57de03c788', N'045405-4949', N'cell', 1, CAST(N'1958-04-27T11:33:21.010' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'89fafe66-78ae-197a-7936-4c0666f5910e', N'30f20823-885a-332f-86ef-b08b142f744c', N'209-0226097', N'office', 1, CAST(N'1983-08-12T05:55:41.430' AS DateTime), CAST(N'1998-06-27T00:47:17.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2b113f5f-81d0-56f3-9dee-4c4259bec7d2', N'acd651a1-3d00-345a-a525-48e5d028cbe7', N'231321-3538', N'home', 1, CAST(N'1972-10-21T05:45:44.260' AS DateTime), CAST(N'1992-12-23T16:32:45.500' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'eb0c857d-7b18-e13d-43ef-4c7e3210731a', N'37a5d6cc-6153-9e5d-2075-76deb55cad74', N'521-6411111', NULL, 1, CAST(N'1987-08-14T15:36:37.250' AS DateTime), CAST(N'2010-04-16T10:04:47.850' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8eb60066-fdb7-d722-040c-4cbc2686cb4b', N'bbf080ed-8ff7-de34-2166-04adf0722f3f', N'177-886-1846', N'home', 2, CAST(N'1969-12-12T20:00:39.880' AS DateTime), CAST(N'1960-12-09T06:16:47.990' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'dcab53c5-399e-192b-f58f-4d1770462205', N'7dbe3f53-7d08-9a11-90f0-1d1d51f8a768', N'542-0624466', N'office', 2, CAST(N'1997-01-30T10:34:03.770' AS DateTime), CAST(N'1980-07-07T19:11:45.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'aff042a9-1da4-3556-78aa-4d2d6f9108a0', N'72a6a799-f38d-bda3-0499-e369647b8785', N'075142-0277', N'office', 1, CAST(N'1956-10-18T07:32:14.700' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'aeccbde5-76fb-01d9-2cfa-4d59477abafc', N'cb05d5d0-c5e9-78fa-70d2-36caf9b230ea', N'908-5875449', N'cell', 1, CAST(N'1975-10-24T07:46:21.160' AS DateTime), CAST(N'1983-04-26T15:13:15.350' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ecaf0ff0-944e-7073-7b0e-4d6fc9430f8e', N'b8ae99fe-7445-3c14-5b59-c691e4fcd855', N'284-6642043', N'cell', 1, CAST(N'1966-05-23T23:22:27.160' AS DateTime), CAST(N'2002-12-30T11:44:21.700' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'12ef0cf1-92b2-c1f7-600a-4d8678114e20', N'8f157a23-af8a-ba68-9273-bdd79572f3af', N'097-588-3461', N'office', 4, CAST(N'1986-09-16T19:31:36.250' AS DateTime), CAST(N'1968-06-28T05:12:31.360' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c4a8f127-6ac4-48cf-e65f-4dbf084de995', N'15eafd11-8ba1-0d3d-cbf1-95d5781193e7', N'057389-8650', N'office', 1, CAST(N'1963-02-01T03:59:36.980' AS DateTime), CAST(N'1964-04-10T03:17:31.180' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'80c216dd-0a63-015e-e29d-4df298b853de', N'4774734b-48d5-713d-5d36-a963453164be', N'3263882933', N'cell', 2, CAST(N'1962-04-19T16:02:59.300' AS DateTime), CAST(N'1987-04-23T12:13:20.410' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f224b06b-7774-f1a5-a54d-4e5bf875fa7f', N'178cf09b-f48a-5a21-fe92-5bfac20585a9', N'307-3010237', N'cell', 2, CAST(N'1986-09-15T13:23:50.640' AS DateTime), CAST(N'1962-02-22T22:47:42.070' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7993fd9e-7539-de06-4ded-4f80dca24b43', N'04ea5e3c-e72c-f996-ecd8-1093ceb27775', N'111805-7057', N'cell', 1, CAST(N'1964-09-14T10:34:01.760' AS DateTime), CAST(N'2011-09-05T22:59:48.650' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'71d369ff-37d4-2640-0069-4fb0b7bd431f', N'de0a1662-190f-dce3-5919-86b9361fb8f1', N'6703735552', N'office', 1, CAST(N'1960-04-19T08:00:01.230' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'635f055b-1521-b5fe-c08a-4fdf65cfe012', N'd9a7b48b-83d2-9e4f-d1ef-18a8451297b6', N'574858-9021', N'cell', 2, CAST(N'2002-03-05T05:20:09.710' AS DateTime), CAST(N'1955-07-18T01:52:28.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cd739a65-0942-8a49-2c0b-506f6899c68d', N'0dc4ca11-e671-d581-31b5-44eb11e85c28', N'099624-8346', N'cell', 2, CAST(N'2015-07-09T14:55:55.010' AS DateTime), CAST(N'1970-05-23T01:23:10.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6deb0ecf-1f18-e198-1a19-507bb09ac25f', N'5f95f536-21ef-5961-b255-b41994289f94', N'6560402669', N'cell', 1, CAST(N'1980-03-07T17:23:20.300' AS DateTime), CAST(N'1974-12-10T10:44:05.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'484d25a8-17a6-dd6c-69a4-507f159588bd', N'4358786f-1608-3b43-45bb-fa3490213b2a', N'660-8604064', NULL, 1, CAST(N'1983-02-02T17:27:32.550' AS DateTime), CAST(N'1966-03-24T20:58:15.710' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0f6aa52b-5190-8f8d-363c-50af4e1fabea', N'8f157a23-af8a-ba68-9273-bdd79572f3af', N'139778-0958', N'cell', 2, CAST(N'1956-11-09T02:59:11.650' AS DateTime), CAST(N'1963-08-26T12:09:43.120' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'40861e9b-08ac-700d-00a4-50baf490aad5', N'f7a6b9bf-27d1-ab5a-955a-ae0fc47afb54', N'4881611372', N'cell', 1, CAST(N'1990-06-09T18:23:06.590' AS DateTime), CAST(N'1977-08-08T13:37:00.160' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a5eeb0fe-db4e-9522-00d4-50d13948811c', N'f642946b-40e0-f388-2c2a-05d97e5a3233', N'567-461-7036', NULL, 1, CAST(N'2000-04-26T08:16:20.360' AS DateTime), CAST(N'1981-09-24T17:50:22.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'63373b0b-1b9f-9f37-15b1-50da284ccf6f', N'2e855192-56b3-16cf-df3d-0ee8784a1531', N'2519541478', NULL, 2, CAST(N'2004-06-24T19:54:52.440' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2987e22a-a84e-6954-954b-51278d3ae712', N'f313a0fc-445c-cf83-068c-4cd959c5865e', N'2781082100', N'cell', 1, CAST(N'2012-04-09T05:54:32.300' AS DateTime), CAST(N'1998-12-25T04:32:33.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'346307f2-2412-8438-933a-518888c13cf9', N'9dc0889b-b5e7-a83f-4cba-3f864551b8a8', N'810-375-7308', N'cell', 2, CAST(N'1987-12-22T02:19:21.010' AS DateTime), CAST(N'1978-08-03T18:46:15.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c848b204-808b-90c7-e72e-522f678ac9c1', N'b31a758e-e0c6-7886-5394-e1d4060f3514', N'333-746-9013', N'office', 1, CAST(N'1980-05-05T07:35:25.430' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1020a229-15ad-6a87-7d2b-524a136d1a80', N'6d9de423-992d-7114-72f2-971ca5e9a099', N'407414-2299', N'cell', 1, CAST(N'1971-08-13T10:35:12.890' AS DateTime), CAST(N'1979-09-10T16:17:15.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4d6dfdf9-c0f2-fb7b-f4fa-52890a9ef8f1', N'e7771c48-f76c-d603-0d6f-189cb9dfe163', N'234-745-1439', N'home', 1, CAST(N'1980-04-11T13:25:30.110' AS DateTime), CAST(N'1961-08-22T14:53:47.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0de93821-2a37-49aa-87a5-530a2cd9de5c', N'96c060a3-3303-de7c-93d6-75feb33bea04', N'609543-9329', N'home', 2, CAST(N'1995-11-07T10:26:22.100' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6633359e-70a4-1154-075c-53e026516023', N'8f1351b7-4ee4-e2f6-47b8-4d71aa86a85a', N'118629-1054', N'cell', 3, CAST(N'2000-11-11T12:51:06.610' AS DateTime), CAST(N'1969-01-11T16:35:58.970' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'479a3e82-8339-7c22-2a9d-544ae8ce0875', N'ac0d5096-14c3-5e12-f28b-ede6bd394d36', N'240-2923970', N'office', 1, CAST(N'2010-02-11T23:14:14.050' AS DateTime), CAST(N'1986-09-21T16:23:56.050' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5a36ba6a-e142-ac28-af0c-54c0e4907b98', N'82d9172a-b51e-4587-7208-270708049dd7', N'238-3035737', NULL, 1, CAST(N'1966-06-22T01:51:30.890' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7bfea452-b669-fd00-f6c1-550132d7a62d', N'9efdcea9-dcb1-c6fe-2a65-70097f0757c0', N'528-489-5077', NULL, 1, CAST(N'1958-12-17T05:14:22.640' AS DateTime), CAST(N'2015-12-04T11:04:20.520' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'34c413b7-2dde-1f2d-eb0a-55068f4cff60', N'74a002fd-ec01-437c-45e3-ab8a46daa2d3', N'3158872909', NULL, 1, CAST(N'1985-01-02T20:49:00.870' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0de91e94-e4c8-9147-4e69-55097cde2344', N'0c2f13ff-bfc7-e90a-70ef-e0ee00c7ce61', N'927-4759008', N'cell', 2, CAST(N'2017-07-31T15:45:09.310' AS DateTime), CAST(N'1994-07-28T19:55:31.620' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'32f8fd99-c708-f8b1-86ed-5579241a91b7', N'a96aa8ae-42f4-aa8c-574b-5306d8d3b7a2', N'644-1883596', N'cell', 1, CAST(N'1999-01-18T13:38:59.970' AS DateTime), CAST(N'1972-12-15T02:34:17.990' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'79983cda-814f-53db-0462-557bfd3768b5', N'417e671b-f0c7-4626-b9fd-e086c4ec3e1c', N'832-1763686', N'cell', 1, CAST(N'1979-03-31T15:17:22.470' AS DateTime), CAST(N'2017-11-18T17:55:08.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e06a4d6b-66d6-139b-3186-55ac81b85260', N'3409a2b2-48c9-5535-4129-ee5a5c3117bc', N'5603811760', N'cell', 2, CAST(N'1997-05-16T22:11:53.270' AS DateTime), CAST(N'2000-06-24T12:17:25.610' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'61463291-5722-1e0a-087d-55bebea0a491', N'39e4a4b7-bbb5-de93-e172-86593cf111fc', N'5747083491', N'cell', 1, CAST(N'1969-09-27T03:47:01.700' AS DateTime), CAST(N'1999-10-31T01:10:43.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'25bba6c5-3ef2-311c-95ef-56aaa3e0eaf8', N'55036e2e-a022-67b9-307a-8a53ab77d5cf', N'164-412-3486', N'office', 1, CAST(N'1996-05-14T07:03:50.760' AS DateTime), CAST(N'1957-01-14T08:57:18.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fa5580ba-5161-79b8-bea7-56d3ec2a6fe5', N'b1a74251-4cd2-46d9-bb29-d1bb2237a12b', N'723673-4826', N'office', 2, CAST(N'1971-01-15T09:46:45.030' AS DateTime), CAST(N'1965-09-02T09:38:44.100' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ffadfa8f-dca8-83fd-fb12-5788ad6e2e14', N'bf9973b4-bca4-8b20-c171-16ddb732d1f3', N'228-814-8736', N'cell', 1, CAST(N'2003-12-16T10:47:40.090' AS DateTime), CAST(N'1960-06-04T04:00:18.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c4ac4b5b-d4e3-584e-2f5b-57a946454cfb', N'0d14bb37-2feb-daf1-daa9-7f7b30f59506', N'915986-3876', NULL, 1, CAST(N'1968-06-01T12:35:26.840' AS DateTime), CAST(N'2010-03-16T12:55:26.610' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9a988674-beee-604c-164f-57e3b0f50998', N'2ee75969-619d-c499-95d4-684923989945', N'1891207066', NULL, 1, CAST(N'1968-05-23T15:30:02.350' AS DateTime), CAST(N'1986-12-26T23:00:50.630' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'eb24ebc4-0ad7-c154-4471-57e4a4faa2c0', N'2e855192-56b3-16cf-df3d-0ee8784a1531', N'7196057374', N'office', 1, CAST(N'2004-06-14T09:12:53.350' AS DateTime), CAST(N'2004-11-06T11:26:03.980' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ab1ed743-6172-ce67-63c5-57ec3d3afe62', N'f7e156df-e7ea-2fcd-6c48-609765ac8e2f', N'2040190757', N'cell', 2, CAST(N'1978-03-29T09:53:36.030' AS DateTime), CAST(N'1964-08-04T08:57:02.460' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b62c9ff2-be2a-f98c-5575-582732b3ea0a', N'298728c9-4438-1b8b-f404-273f09155a73', N'985-3502150', N'office', 2, CAST(N'2005-01-30T16:20:20.150' AS DateTime), CAST(N'1954-04-08T16:11:03.250' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1618b233-3df1-3364-016c-5888c6418845', N'c532dbd6-9fad-4038-8e88-85e7b95dd297', N'475-5170734', N'home', 1, CAST(N'1975-01-19T05:28:16.720' AS DateTime), CAST(N'1964-04-12T04:15:36.280' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'20048173-1648-07d8-bdbe-58d638b70fc6', N'024ff35a-7e84-345d-ed2e-26c4e844cacf', N'480-010-8256', NULL, 2, CAST(N'1993-09-22T10:27:24.530' AS DateTime), CAST(N'2012-06-21T21:54:43.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'db05d7da-0100-0446-2479-5968f0c3f41e', N'4e33972d-d9eb-5415-e8a6-f43ee6c64d87', N'312457-5023', N'home', 1, CAST(N'1954-12-08T02:30:04.940' AS DateTime), CAST(N'1982-11-25T19:54:30.950' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6c243165-aa91-6499-890b-5974d651597b', N'5b9e9d9b-106a-9d65-a56b-9780da430095', N'541-272-6443', N'cell', 2, CAST(N'1970-02-08T06:55:37.790' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'be584f66-0a22-c4cd-eddb-59e59d250a3e', N'8f1351b7-4ee4-e2f6-47b8-4d71aa86a85a', N'503-7415697', N'home', 1, CAST(N'1983-04-27T05:25:26.180' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bd7e2196-95ef-f9ce-144b-59f9a7f090ab', N'94211eb1-79e8-9a82-ba1d-a6a10c2c5641', N'151-908-8388', N'office', 2, CAST(N'1963-12-09T14:50:53.360' AS DateTime), CAST(N'1978-05-10T15:49:55.390' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4f07e1f0-eb17-dcd6-fec8-5a100824ba2e', N'8b4c5a50-4d9d-1e84-6b0e-2feda9c6ee84', N'8993016979', N'cell', 1, CAST(N'1982-06-06T04:31:59.660' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1d501ba4-9556-fd90-cbdd-5a244cd75f22', N'e9708cb9-c244-0522-dd98-783b41935735', N'401861-8634', N'office', 1, CAST(N'1989-09-04T19:49:55.260' AS DateTime), CAST(N'1961-07-01T23:49:53.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e0df9b0a-0402-7744-149b-5a36396c6b2c', N'd159c73d-f349-b4b0-57f8-4ee62f70d498', N'5004236687', N'office', 1, CAST(N'1984-07-17T21:24:41.310' AS DateTime), CAST(N'1970-09-18T07:05:38.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9aa9aac4-eca8-c264-5fb0-5a486b6a0fe0', N'25676b82-392c-dd79-c23a-18e282591447', N'143355-2029', N'home', 2, CAST(N'1989-03-03T15:05:50.280' AS DateTime), CAST(N'1958-07-20T18:18:04.660' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'982132e6-7fc0-c5bb-fb48-5ab0c1355359', N'e002b7fd-e7dd-a8cb-1a42-55f912567e03', N'684-7582529', N'cell', 2, CAST(N'1972-06-25T11:33:03.960' AS DateTime), CAST(N'1988-06-25T15:17:13.280' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'83738023-b913-1320-49fb-5ac1b240eec2', N'861e8674-ef6d-f694-208d-cc2bf12cc956', N'477-9180596', N'cell', 1, CAST(N'1989-04-28T15:23:40.540' AS DateTime), CAST(N'1977-07-21T20:11:46.060' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fe6fc95a-b5d3-0adc-e5f8-5ae82be0e651', N'8e4a2e23-0613-9c15-62d0-c2222bc013db', N'817-658-3455', N'office', 3, CAST(N'1973-03-07T23:07:13.690' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ebcaec11-4dcf-67af-d4e3-5b08e30aef11', N'15e0898e-acc5-7cc1-e4f5-81f3f6e90e60', N'3333331772', N'cell', 1, CAST(N'1963-11-05T01:39:19.880' AS DateTime), CAST(N'2001-07-16T18:33:33.830' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'34f099a0-79ea-db7d-a1d2-5b53de7cb60a', N'97fdf0a6-d20b-b2d8-dd38-43809d6037d6', N'789-3169419', N'cell', 1, CAST(N'1977-02-19T22:20:54.690' AS DateTime), CAST(N'1973-01-14T21:26:38.070' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7acc3640-12bb-cae0-dc93-5bc66c5991ef', N'c88c06c0-9cd0-2bda-7bbf-244415596cae', N'160-774-4396', N'cell', 1, CAST(N'2013-09-08T23:40:38.370' AS DateTime), CAST(N'1980-12-01T21:28:54.530' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8122960b-fd67-b245-f55b-5bd21c8a5a02', N'3181779d-7098-d55a-2fb8-12f0f58e898f', N'220-2101617', N'cell', 1, CAST(N'1980-11-05T20:19:50.270' AS DateTime), CAST(N'1973-01-11T01:26:01.960' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a61a14b6-a7f2-9311-ef66-5c7d663c2544', N'a6160a25-386f-f4f4-1595-0ac758b3b576', N'7478745825', N'cell', 2, CAST(N'1992-07-15T06:49:15.260' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f3f7b4f7-8903-5576-2930-5c8a504c2aa7', N'5a4d3d1d-bac3-cb3a-3976-7e29d71f31bd', N'205823-9454', N'cell', 1, CAST(N'1977-12-07T20:03:13.630' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8ba5e387-0e76-8cf8-ec95-5cc9b1661f4c', N'8e377c8e-87fe-3a5c-23ef-cd28f95f95ab', N'378858-7319', N'cell', 1, CAST(N'1955-09-24T12:16:00.500' AS DateTime), CAST(N'1962-10-28T04:01:30.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3838a823-8b1b-1740-b097-5d07817a99d7', N'c601740b-14f4-07e2-49ec-af1a0181da12', N'133-1291178', N'cell', 1, CAST(N'2006-08-08T16:39:23.780' AS DateTime), CAST(N'1961-03-17T10:59:57.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'dd0890c3-53bc-a256-7165-5d18f10d4e69', N'2ffd9d63-441b-49e7-832e-aad877657e0c', N'620722-7923', N'cell', 1, CAST(N'1968-11-26T12:25:16.310' AS DateTime), CAST(N'2016-11-08T11:08:42.830' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e1fbe78d-d7ac-359a-74a7-5d90c7cd105f', N'0c2f13ff-bfc7-e90a-70ef-e0ee00c7ce61', N'378788-8686', NULL, 1, CAST(N'1995-04-18T14:39:52.350' AS DateTime), CAST(N'2002-02-11T16:56:14.890' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'93b87d77-43e9-c38a-f8b3-5de4b158411b', N'5a3253c5-f63a-5cbb-6caf-696021928ee8', N'107-625-6017', N'office', 1, CAST(N'1998-10-08T02:02:50.470' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'377849a7-d794-a4e8-ef02-5e2c5ebce32b', N'e65c9fc6-6666-6b89-c81a-7ab4426a2efd', N'761-5833820', N'office', 4, CAST(N'2014-10-18T16:34:04.380' AS DateTime), CAST(N'1987-04-21T19:16:03.570' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5b2e04ed-0d62-6fb5-76e2-5e52d3bd3e73', N'78780482-51e3-7979-2a01-8356b1874f8c', N'875716-1355', NULL, 1, CAST(N'1985-06-17T09:59:28.790' AS DateTime), CAST(N'1998-01-10T08:10:21.940' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e6809ad3-5dc9-625c-61c4-5ed6b07425d4', N'0e9df77e-ee1e-cc20-c273-9e903a945652', N'806-601-2054', N'cell', 1, CAST(N'1991-02-01T03:21:03.720' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6621e06a-cca3-59c4-13ab-5ef6cd242316', N'd36b7c76-451a-fd0c-f9ab-761d17a5e66a', N'377-9921060', N'office', 2, CAST(N'2004-07-19T02:10:44.310' AS DateTime), CAST(N'2014-11-04T09:21:47.250' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f6434683-8de4-c311-ee0f-5f1ff0c61447', N'dd4f8672-cbc3-e94b-0c13-ed6f19b936a4', N'0068390822', N'office', 1, CAST(N'1989-05-16T03:46:07.970' AS DateTime), CAST(N'2005-02-03T02:29:11.160' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ffa60237-16e3-fccf-5015-5f4d41d8501a', N'b98474e7-743b-3d3d-0281-59ec5378fd66', N'483492-3790', N'office', 2, CAST(N'1997-02-05T08:51:05.690' AS DateTime), CAST(N'1994-04-20T18:38:46.420' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3b727a74-88a3-78fa-ea5f-5f625b34c45b', N'b45181b5-2490-4d74-d1cd-51e26446ce3b', N'966968-8768', N'cell', 1, CAST(N'1960-05-01T13:36:40.960' AS DateTime), CAST(N'1960-05-13T12:24:21.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'84a996fa-c477-4245-f9bf-5f83ebff5b7b', N'5d21f040-c17b-deb7-520b-d20b75e8d32f', N'808-142-4826', N'cell', 2, CAST(N'1988-10-10T19:14:30.940' AS DateTime), CAST(N'2004-03-04T23:13:00.370' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'027828a7-3b3c-e0fd-ff16-5fb87a49e61e', N'b20eaed0-9b62-1c20-f37d-25ad91399e01', N'775-991-1268', N'office', 1, CAST(N'1979-07-10T02:53:17.850' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bd86c51f-5243-7b5c-c48f-5fde04d76c26', N'a0f8c1fa-f149-2cb2-9899-6316ac9a0bf8', N'922658-0778', N'cell', 2, CAST(N'1979-10-27T10:29:47.780' AS DateTime), CAST(N'1958-08-08T18:16:27.950' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'076b1eae-cacf-a4e5-21f0-5fea11ab015b', N'daf5a3ef-8ceb-5943-2494-feed0531be73', N'404-6515813', N'cell', 1, CAST(N'1968-12-02T03:41:27.030' AS DateTime), CAST(N'1990-03-09T08:22:01.360' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b58617c2-c4c1-55f5-b462-603ceeb53146', N'c82c5122-d676-13ee-520d-e4f2d2e683b9', N'511-411-9963', N'home', 2, CAST(N'1966-06-26T00:21:49.470' AS DateTime), CAST(N'2007-11-08T02:17:15.520' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c8a16271-2bc4-df6d-20da-60a672db463a', N'b1a74251-4cd2-46d9-bb29-d1bb2237a12b', N'632747-8150', N'office', 1, CAST(N'1967-10-04T19:39:52.400' AS DateTime), CAST(N'1968-04-23T05:32:30.700' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0a0dac59-b75d-f7b6-95c2-60f4e9cbcdf4', N'3181779d-7098-d55a-2fb8-12f0f58e898f', N'8992348877', NULL, 3, CAST(N'2013-01-29T05:44:35.830' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ff3295f8-8086-59b1-5e9d-615c83e992ff', N'cd0b82b2-9fd1-bbd8-e42f-744758a0a5e6', N'542-3096675', N'home', 1, CAST(N'2012-04-09T04:18:13.140' AS DateTime), CAST(N'1961-01-28T17:34:55.700' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1f58bc2a-4cc2-31c5-8b74-616cf0b83204', N'17228aa5-31db-a1a8-df86-e0ff2c33070f', N'1705578195', NULL, 1, CAST(N'1985-06-01T19:45:06.870' AS DateTime), CAST(N'2002-04-29T20:14:32.050' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3b7626da-f435-04f8-d0a8-61893ce665c7', N'4e33972d-d9eb-5415-e8a6-f43ee6c64d87', N'073001-9963', N'office', 3, CAST(N'2006-03-07T19:49:58.150' AS DateTime), CAST(N'2014-04-28T03:40:56.860' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'05b30d18-f4b8-0cdc-be22-619e8c8ac945', N'ff615e84-d554-bd9c-21d8-eb8b0f28535d', N'400-5964651', N'cell', 1, CAST(N'1985-04-06T04:57:04.430' AS DateTime), CAST(N'1970-08-27T11:08:19.530' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'79583cda-9250-d558-ae2d-62f4f4997ce2', N'f10716c0-932b-39dd-438e-919db7f7ecaf', N'013290-0955', N'office', 1, CAST(N'1955-07-10T11:15:35.200' AS DateTime), CAST(N'2001-08-10T14:58:13.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'709b98ae-f30f-7b93-9f05-62f681a24771', N'5b9e9d9b-106a-9d65-a56b-9780da430095', N'1435857088', N'cell', 3, CAST(N'2005-01-28T10:57:04.190' AS DateTime), CAST(N'2007-05-30T15:15:51.720' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cedd17cd-ada0-1d8e-cb3c-62f7907d3ede', N'37b789ae-8092-b07c-44b3-b0ea90b75b8e', N'448-170-2545', N'office', 1, CAST(N'1955-02-07T12:06:17.030' AS DateTime), CAST(N'2006-12-09T05:43:00.540' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6424fe51-8e88-da45-4ce7-632da57ab24d', N'25676b82-392c-dd79-c23a-18e282591447', N'758-5115778', N'home', 1, CAST(N'1954-07-01T09:03:59.970' AS DateTime), CAST(N'1968-11-21T01:28:37.970' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'17a43da5-6238-074d-262b-63e6cfb667e2', N'b94db488-0525-ea84-99d8-6bd25ead5d68', N'346-695-9460', N'office', 1, CAST(N'2005-07-17T16:25:36.000' AS DateTime), CAST(N'1985-06-20T00:07:52.260' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9d4ef976-83ec-3093-afc5-6416bc66348f', N'de0a1662-190f-dce3-5919-86b9361fb8f1', N'569-6518326', NULL, 4, CAST(N'2002-05-24T13:30:02.260' AS DateTime), CAST(N'2007-12-04T11:31:05.640' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2b57fe81-75c3-bb9e-9fd6-646b8cf141cc', N'6920d81b-bb6e-e813-585b-e82085df04e6', N'8243962682', N'cell', 1, CAST(N'2003-08-05T10:01:39.060' AS DateTime), CAST(N'1955-05-30T02:54:43.980' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'385f924c-2bef-5456-685e-64c6338d6cff', N'2fa8c250-7e83-eb2e-47f8-fb1431b9802c', N'965-0059972', N'cell', 1, CAST(N'1967-12-21T13:39:55.170' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e6351b2e-449a-c588-1561-64d5b59cf51e', N'50e7f883-98ba-eb45-6b46-743440390f32', N'578-8516963', N'home', 1, CAST(N'2003-05-30T17:25:17.100' AS DateTime), CAST(N'2014-01-11T04:47:25.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2815d46f-00f5-0095-1e1c-64f072c3db66', N'66a3a4a3-6315-4974-df77-5b52f0fa2fa7', N'096-789-0972', NULL, 2, CAST(N'1994-05-21T02:26:35.270' AS DateTime), CAST(N'1983-01-05T21:35:37.560' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b00e00c0-6ba4-bfbe-6f03-6559db82dce3', N'cfbac6fe-9446-0590-39c0-d894b7dddb00', N'8219776541', N'cell', 1, CAST(N'1984-06-13T21:14:33.760' AS DateTime), CAST(N'1996-01-18T04:32:59.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ef0c89a5-f894-fba8-16e8-6560551d8b9b', N'b0dfcb7c-96d7-a1d9-6d60-6ae0133e2e92', N'606-992-1185', NULL, 3, CAST(N'1991-07-02T05:17:55.480' AS DateTime), CAST(N'2016-05-14T19:16:07.300' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7481e0e7-b5c3-15b3-8421-661421c39b89', N'3f958b87-ee2b-1ebc-736b-f1bd2cc70e18', N'0076170132', N'cell', 1, CAST(N'1966-10-17T06:41:40.620' AS DateTime), CAST(N'1966-07-11T02:50:51.370' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'eda7ecb4-0c19-5dbc-cd2a-6677439a3a02', N'2d839837-3711-89c4-fade-885c9eb17a9e', N'953-1221079', N'cell', 1, CAST(N'1992-01-03T17:02:00.250' AS DateTime), CAST(N'2018-04-13T06:50:48.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1915b4cb-edc5-d6aa-43eb-669ed4801f7b', N'55a57d26-432c-fda9-f2d0-257339f793fa', N'622-5460413', N'cell', 2, CAST(N'2011-05-22T16:55:06.160' AS DateTime), CAST(N'2012-04-24T18:58:56.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'79dd711e-634d-2ffa-2c65-66b07cd648f0', N'92aa84a7-72e3-c736-ff11-e8ccbc35ad91', N'4444825833', NULL, 2, CAST(N'2015-09-03T18:25:57.390' AS DateTime), CAST(N'1974-10-05T09:57:43.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'79e34264-3278-e1e2-0a1b-66c36d5150b1', N'b2a4f0b0-a7c7-cafd-f374-4d76224b954d', N'598-748-4352', N'office', 2, CAST(N'2015-05-14T14:24:31.070' AS DateTime), CAST(N'1973-01-03T02:38:46.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9a998993-8993-b31e-3078-66cf425ec9e9', N'5b345208-16e0-a373-62cd-9a1b062260a0', N'4028093464', N'cell', 1, CAST(N'2006-02-22T08:05:04.690' AS DateTime), CAST(N'2015-02-15T18:26:24.960' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'60fd067e-c354-be6d-4881-679347374bc2', N'87ffbe87-3ce2-af34-ee4e-ce5399c59411', N'686390-7345', N'cell', 2, CAST(N'1961-03-18T21:36:51.590' AS DateTime), CAST(N'2011-08-03T04:09:44.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f4af9087-271e-1727-2c63-67de9ae4f5a7', N'48030c69-1297-5fcc-1b22-1ef2adb69527', N'6421692574', N'cell', 1, CAST(N'1979-02-09T13:17:33.300' AS DateTime), CAST(N'2008-12-22T14:53:48.830' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e9f4c039-30fe-43b3-9e58-688b16ec8d0c', N'b1965eb0-7bdc-b902-a364-fe1db6ec466c', N'812725-0640', N'cell', 1, CAST(N'1954-04-23T05:54:18.000' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9a3e23f0-f5ee-85bf-1c81-689e6970bd86', N'ed5448d8-052d-e536-1dc4-120b96d43859', N'9446416214', N'office', 1, CAST(N'2002-07-09T00:07:59.590' AS DateTime), CAST(N'2010-01-25T00:07:46.090' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b67a3568-177b-6170-c7f7-68a368126b22', N'b4180adf-8327-4f24-0fb7-3a55f3178470', N'471-1524021', N'home', 1, CAST(N'1990-03-15T20:16:26.100' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'dbbb4356-a179-f9d9-ddc0-68f0af22ac25', N'047dc926-9ceb-03e7-e981-f032f0c6acfd', N'039-8153943', NULL, 1, CAST(N'1995-01-20T07:44:09.120' AS DateTime), CAST(N'1995-01-04T12:36:31.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7aa288e8-2205-ecfb-3868-6915627d7f4a', N'be4dfe18-2c08-fba3-b97b-2905727713f5', N'723-273-6859', N'office', 1, CAST(N'1979-09-01T10:22:23.060' AS DateTime), CAST(N'1987-01-21T03:30:54.320' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'050768a8-6292-6ab9-caaf-6969522a845e', N'bc662b26-343d-bf13-ef5d-04bdb7d81624', N'147510-4865', N'office', 2, CAST(N'1987-02-25T00:06:28.750' AS DateTime), CAST(N'1988-10-15T01:37:06.690' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1f44cb3e-67bf-b1ed-51dd-6a012df284ed', N'bc519677-9f85-eeb4-51eb-103cf4e8fb1d', N'460-7854675', N'cell', 2, CAST(N'1983-12-07T14:27:22.530' AS DateTime), CAST(N'1982-10-14T08:48:00.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c06d037f-3a8c-40e1-0e75-6a09b857c56e', N'96c060a3-3303-de7c-93d6-75feb33bea04', N'765-6430858', N'cell', 3, CAST(N'2016-07-10T19:52:25.360' AS DateTime), CAST(N'1974-08-18T01:46:00.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a1e15793-5429-1b83-2e50-6aac9b404c49', N'78a3154a-e10d-0f48-536d-0f8f264b8f8c', N'663343-1519', N'cell', 3, CAST(N'2000-08-26T13:56:50.490' AS DateTime), CAST(N'1959-07-02T22:54:28.980' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fca461d1-eec0-7e78-9515-6b09d53c3cb9', N'f949d8e1-f3b0-bd12-2b2f-1fa0d58605ba', N'309-0166970', N'office', 3, CAST(N'1991-11-23T14:39:32.850' AS DateTime), CAST(N'2010-09-21T06:42:12.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4a517710-bdcf-5003-b62f-6b809f546438', N'a327a627-4199-e783-e291-4a549e8fcfa9', N'130-564-8216', N'cell', 1, CAST(N'1968-09-29T03:36:02.350' AS DateTime), CAST(N'2011-01-15T19:47:05.740' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4243f8fd-87cb-e6ab-2e04-6b8beca5022d', N'd96cfa78-2286-cc66-7bfe-98d23a7e88e1', N'289-2996923', N'cell', 1, CAST(N'1955-02-16T03:55:01.780' AS DateTime), CAST(N'1961-08-18T10:13:39.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8b208e68-67fb-9d62-7a38-6bd23f6f9eeb', N'0f17af71-a953-ecec-b275-5769657e3d34', N'401-730-7639', N'cell', 1, CAST(N'1982-01-18T07:19:01.170' AS DateTime), CAST(N'2008-07-14T09:44:20.920' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e607f8bb-af60-6a70-c5d0-6bfdc91a41bc', N'5b7937a7-3420-4faf-0477-ec751bbd2ed1', N'4979627473', NULL, 2, CAST(N'1969-05-23T08:02:49.790' AS DateTime), CAST(N'2014-05-19T05:37:26.190' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7c592d94-ee08-9af3-3437-6c05ce1179dd', N'ca8d6850-c123-ba14-5894-2a2385f5c81d', N'314520-1067', NULL, 1, CAST(N'1955-12-19T04:29:34.310' AS DateTime), CAST(N'1993-08-30T03:10:41.560' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c6d67454-a3b9-84da-8f42-6c32caeeb22b', N'9b870adb-4abb-2276-75cd-dc589f7ae762', N'784-761-6523', NULL, 2, CAST(N'1992-12-11T01:17:13.620' AS DateTime), CAST(N'2017-01-06T15:54:20.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9c744116-0a64-c2ee-15b7-6c6209ebe969', N'a595244a-ed3d-3d1f-e19b-a8288da63bb1', N'676-432-9470', N'office', 3, CAST(N'2003-09-10T19:12:11.120' AS DateTime), CAST(N'1975-07-25T03:45:46.080' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ef0099ee-ce8a-e1af-e056-6c810343a66f', N'168ac464-d503-88b8-8492-db391d28c6d5', N'793-3421432', NULL, 4, CAST(N'1991-04-12T21:16:12.690' AS DateTime), CAST(N'1981-05-29T02:32:37.890' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2bffadf2-53ba-5032-df64-6c99d7d20005', N'4fdfbd59-41bd-49f7-1e1a-9da392d0dd3d', N'334-5942113', N'cell', 1, CAST(N'1957-09-17T02:12:21.780' AS DateTime), CAST(N'2002-05-09T17:43:47.300' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'efefeee6-79d6-6e37-469c-6ccf2d0b004f', N'26b27b26-e6ee-bbf1-7174-595025509579', N'157-5424849', N'office', 1, CAST(N'1983-02-28T21:16:26.380' AS DateTime), CAST(N'2006-11-23T06:13:13.670' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'befec1c2-0ae9-7cc4-cbd4-6ce22613e2b4', N'75d2447e-0f66-f906-f633-2dbc70a3c287', N'768-428-1170', N'cell', 2, CAST(N'2013-11-22T18:54:15.920' AS DateTime), CAST(N'2006-04-22T18:52:41.870' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b32067e7-1303-f179-9316-6d7c1897b91a', N'40faa097-3a8c-e7a0-896c-1255eac6a6d2', N'879-004-0778', N'cell', 1, CAST(N'2007-09-18T08:39:59.710' AS DateTime), CAST(N'1959-09-24T03:44:13.060' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9278c4d7-6049-b079-be01-6dd89c9165d5', N'193abdc4-f255-379d-8505-57e09123adcb', N'3331274797', N'cell', 1, CAST(N'1974-12-04T23:19:06.510' AS DateTime), CAST(N'1956-04-07T02:40:14.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'abcd2f49-0611-2563-1b3b-6dfcd08e4a7f', N'0e884b8c-93a6-2beb-2d30-db7347381e77', N'594-020-8571', N'cell', 4, CAST(N'2011-07-10T21:37:00.420' AS DateTime), CAST(N'1975-09-09T21:18:08.890' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'207417f1-a3ee-96eb-6aae-6e37bee7d053', N'e6a26b9d-4fd9-77ac-9e13-92e17256a07f', N'5406415039', N'home', 1, CAST(N'1953-05-16T04:33:04.630' AS DateTime), CAST(N'1972-11-02T04:10:06.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'781239e4-64c3-e8a9-d1c3-6e5cfdd3a59a', N'f949d8e1-f3b0-bd12-2b2f-1fa0d58605ba', N'274-6031974', N'cell', 1, CAST(N'1963-07-29T07:26:35.000' AS DateTime), CAST(N'2013-07-11T21:35:14.120' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f14e441f-5f35-b912-76f2-6e671d526eb7', N'04ea5e3c-e72c-f996-ecd8-1093ceb27775', N'9211422237', N'cell', 2, CAST(N'1976-08-29T02:25:36.190' AS DateTime), CAST(N'1965-01-30T05:13:04.390' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'31ae38b6-c72a-081f-1f87-6eb72aa9dfb4', N'2a5ac717-3e6b-42ae-2305-f4c7edfad7d4', N'1670602678', N'office', 1, CAST(N'1972-12-04T02:18:41.090' AS DateTime), CAST(N'2007-05-28T03:48:15.050' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a29bda35-3534-f9b0-00d1-6ec80daf3d18', N'e359811a-9ae5-e0e9-a488-493cc527d768', N'750-463-4552', N'cell', 1, CAST(N'1970-06-09T19:14:17.210' AS DateTime), CAST(N'1995-08-16T15:45:24.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5b0f34da-52d4-56bb-36b1-6f1b66eea108', N'6fc81ef3-2e1f-9771-79f1-493f38b0b3f4', N'802850-4102', N'cell', 1, CAST(N'2013-06-03T19:29:40.990' AS DateTime), CAST(N'1982-03-08T21:44:57.920' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'536372d1-397f-1ae6-291c-6ff5d375f2c4', N'ab64dcc8-5a0d-0749-7a7e-6158568ee832', N'384-2469883', NULL, 2, CAST(N'1985-10-04T15:06:18.190' AS DateTime), CAST(N'1994-06-28T16:30:04.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'61a87bc6-5ca4-8500-fbb3-7018004d3089', N'5f205b6e-f28a-8512-f924-c6d25a44e0fe', N'4202561264', N'cell', 2, CAST(N'2010-03-04T05:58:03.600' AS DateTime), CAST(N'1981-08-23T07:12:48.640' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1505568c-76c6-9087-6fb9-702453dee94a', N'90a59def-5aca-6754-f4e5-df6fcd29851b', N'570-1381055', N'cell', 1, CAST(N'2015-06-09T12:05:07.570' AS DateTime), CAST(N'1991-10-04T06:57:03.500' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'56a83334-a464-8ebe-6505-703230a928c0', N'bb9767c5-1598-bcd4-7c65-24dde030f7a3', N'150-682-0923', N'cell', 2, CAST(N'1994-11-05T06:21:37.110' AS DateTime), CAST(N'2002-10-21T00:22:56.580' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3de6704b-c2fc-def9-52ae-703677b99540', N'7c78408b-9c6e-7467-46c7-b35aa3f3981b', N'263688-0466', N'cell', 1, CAST(N'1969-07-05T10:51:04.590' AS DateTime), CAST(N'1981-03-07T03:58:19.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'69d7f5a4-9f51-6e29-27ae-703ead6d0437', N'af89fbfc-6e17-ffa3-55d6-ca382e50b014', N'875-638-4522', N'home', 1, CAST(N'1995-06-12T05:30:58.520' AS DateTime), CAST(N'2008-10-21T03:59:56.490' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1059bed2-ac32-f950-6272-70730a119413', N'72a6a799-f38d-bda3-0499-e369647b8785', N'499-903-9918', N'home', 2, CAST(N'2006-08-24T09:23:52.420' AS DateTime), CAST(N'1980-12-24T09:00:31.280' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'96f83074-68a2-e108-a1e5-708c35d9543e', N'b45181b5-2490-4d74-d1cd-51e26446ce3b', N'625-997-7021', N'cell', 2, CAST(N'1995-08-10T00:14:59.900' AS DateTime), CAST(N'1975-02-10T15:52:47.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1e690eeb-9415-2453-ef05-70a857cc82b5', N'45821833-19bd-5d2d-fce1-8b1f11e54225', N'232031-3019', N'home', 1, CAST(N'1959-09-22T04:10:28.480' AS DateTime), CAST(N'1972-08-30T20:54:21.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5657763e-dd1e-bc29-5b71-70c641b9f62a', N'cd0cde8e-00bb-9ef7-02a8-1bd00fa822fc', N'6348358329', N'cell', 1, CAST(N'1964-04-29T15:33:07.940' AS DateTime), CAST(N'1993-06-27T12:52:11.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b0605a53-64e6-dd77-28a3-7168909a34eb', N'7dfe2f2a-75fd-0df1-7843-a7a94282ab00', N'305-3920046', N'office', 2, CAST(N'2015-11-26T01:41:31.180' AS DateTime), CAST(N'1976-04-01T20:10:15.610' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'95838d71-971f-1757-5922-71d88f022a62', N'6db08f84-cbaa-27b3-fc0f-5fd62832b2bf', N'900264-3840', N'cell', 3, CAST(N'1997-03-10T02:19:39.740' AS DateTime), CAST(N'2016-01-05T16:04:18.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ce9410d0-ba9e-c2a5-b6ee-71f9b6da8b42', N'2fefc7ad-eada-812a-20e2-63981537ab60', N'510037-2632', N'office', 1, CAST(N'1995-10-09T14:33:35.430' AS DateTime), CAST(N'1966-06-25T16:25:40.520' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd2817057-84e0-02e7-6f6a-72dbbe45f56d', N'6ee94258-9b8f-5112-f8ae-6d407ced4750', N'8760740004', N'home', 1, CAST(N'1954-02-18T14:27:35.810' AS DateTime), CAST(N'1978-01-28T18:52:54.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2992a7fe-1b87-1418-7451-72e42b1f9b88', N'911bbb58-c32f-0617-d3d5-2ac178cfad1e', N'434-142-9291', NULL, 1, CAST(N'1993-10-21T04:22:08.600' AS DateTime), CAST(N'1972-12-25T15:41:53.210' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3fc19bac-94ad-9798-969f-72ef932fd285', N'75bfedad-4a62-c65f-be93-1dc820736d77', N'8126034354', N'home', 1, CAST(N'1978-05-14T17:02:07.680' AS DateTime), CAST(N'2015-02-12T09:57:20.650' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'525fe236-0f0c-a66f-ae78-73273e9507a9', N'2d4bcb31-4795-2aea-0b28-7a8722ab6410', N'2353467997', N'cell', 2, CAST(N'1970-12-17T07:56:45.350' AS DateTime), CAST(N'1983-10-18T01:59:10.650' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'42d3dbf0-e732-0c78-283f-732dbbb954bc', N'028534bb-6854-ede9-001d-f5d5d18e33b8', N'471-879-9256', N'home', 1, CAST(N'1966-09-06T16:32:10.660' AS DateTime), CAST(N'1990-11-13T18:10:03.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'695da835-3388-74fe-c658-739dcb9aaca3', N'e359811a-9ae5-e0e9-a488-493cc527d768', N'135-620-8987', N'cell', 2, CAST(N'2008-07-16T18:56:39.230' AS DateTime), CAST(N'1994-04-30T15:42:11.890' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5969c55e-b45d-3b94-28c8-73cf8336e6e6', N'2e16a7ba-e2a8-9ab8-afe4-940dbc5bf800', N'3264654317', NULL, 2, CAST(N'1988-10-10T05:44:37.160' AS DateTime), CAST(N'1964-02-05T22:12:42.530' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a4632463-35b7-a958-a3f4-73d77e734061', N'a3764951-94d6-3769-2b6d-95f3b9ef060e', N'783007-9922', N'office', 2, CAST(N'2002-04-21T08:46:33.400' AS DateTime), CAST(N'1960-04-27T19:27:55.040' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd2494051-4184-6473-88eb-74ce81d72c5a', N'27bc56d8-044e-247b-91b8-5d80478e6b4d', N'829549-0869', N'cell', 2, CAST(N'2009-05-10T10:40:28.480' AS DateTime), CAST(N'1961-02-11T19:17:51.400' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'68318049-cf9a-a64f-f47f-74e70aac3177', N'ecf7646f-3bee-4984-af36-279c10526d43', N'767-7053531', NULL, 1, CAST(N'1976-09-08T11:15:09.210' AS DateTime), CAST(N'2008-07-14T00:00:20.360' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'022e7dee-04dc-b647-2c95-751375d2bb40', N'e65c9fc6-6666-6b89-c81a-7ab4426a2efd', N'397-579-6256', N'cell', 2, CAST(N'1977-03-21T04:51:52.750' AS DateTime), CAST(N'1955-08-27T20:34:49.350' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4ab2786f-dba0-8f41-7ca2-75578245d4bf', N'3fcde392-008c-69e5-e9e3-76a0818da102', N'320028-9563', N'cell', 1, CAST(N'1986-08-30T20:33:31.890' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'937f2b27-185e-9975-3858-755bdccd01f3', N'f709b1f7-d07a-4953-6196-2ea6f369f204', N'515-008-7540', N'home', 1, CAST(N'2004-03-28T15:58:37.120' AS DateTime), CAST(N'1993-03-20T00:38:29.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e05b8a5e-f2b3-1d47-2221-75f46df20c07', N'1ea29037-0643-5b11-a0f1-909bf0be2c5e', N'0200447995', N'office', 2, CAST(N'1999-09-17T10:21:43.560' AS DateTime), CAST(N'1967-01-28T08:22:26.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7692ddb4-8a9d-781e-d478-76271c33cf29', N'291172c1-c9a9-06e9-768a-5e7fa6dbefbc', N'883-972-2165', N'cell', 4, CAST(N'2014-05-28T13:25:21.990' AS DateTime), CAST(N'1975-08-19T09:40:15.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f8650803-f802-8ace-3118-76663448bc4a', N'160ddc98-f83c-db41-e501-89c9a5ce1c32', N'674491-6783', N'cell', 1, CAST(N'1993-07-04T01:29:02.520' AS DateTime), CAST(N'1984-08-27T14:19:08.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b682c776-c608-82ca-5b36-767a8c615e29', N'bf1bb397-5f54-6b82-75ec-cb2e4a40d4e6', N'016-5406987', N'office', 1, CAST(N'1970-04-18T02:35:22.890' AS DateTime), CAST(N'1983-08-10T05:01:12.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1ed2601a-8c4c-37af-0354-7685c8280fed', N'ae515959-b9a4-e134-ae80-f30307ae568c', N'067-4183883', N'office', 2, CAST(N'2012-08-10T16:29:52.210' AS DateTime), CAST(N'1962-12-30T08:25:08.040' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f30d46f9-e55c-709a-b28a-76cce2b65aab', N'85e9a700-7843-d2da-3040-816b7e6b409e', N'3929550375', N'office', 3, CAST(N'2011-08-26T10:45:14.430' AS DateTime), CAST(N'1971-06-12T13:01:52.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e05bca79-6f7b-23fd-50d6-76d7fa73c752', N'd115e1cb-70a2-04db-2f82-626085624ad5', N'2901509051', N'home', 1, CAST(N'2017-10-30T07:31:05.100' AS DateTime), CAST(N'1956-01-05T07:25:17.920' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cadce55a-31da-2d8d-8b12-76f0b306d529', N'ff3f3678-5888-505a-6b08-f54fb1dbd17c', N'4740489133', NULL, 1, CAST(N'1961-05-30T18:44:44.250' AS DateTime), CAST(N'1980-08-06T08:12:27.140' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b12e65db-795b-2606-ae15-76f124709dcf', N'1eb1327e-43da-86c7-2a0f-4f5dda429e02', N'225-7802727', NULL, 1, CAST(N'1968-11-11T23:18:26.560' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a19bffb0-b6f1-bfce-9d3d-77ae0525c91a', N'0469f040-aea1-d804-8293-159d2335dc2e', N'247-9910094', N'home', 1, CAST(N'1969-04-09T11:07:07.450' AS DateTime), CAST(N'2016-10-17T12:25:04.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f5876867-c32b-30b0-0d3b-7823d63d31ac', N'5bbd9fad-142b-9585-a41b-f24a660e82e9', N'7553735137', N'office', 1, CAST(N'1957-05-11T19:31:37.460' AS DateTime), CAST(N'1990-06-13T01:39:32.530' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6c54c99e-433e-d738-4cc4-7846ffaf0038', N'0dc4ca11-e671-d581-31b5-44eb11e85c28', N'366-6203097', N'home', 1, CAST(N'2013-02-10T04:57:18.470' AS DateTime), CAST(N'2011-12-18T14:14:56.560' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c5321ef4-b3b1-ca01-ef83-786fb94d098b', N'721166a1-9c32-0b14-14d2-be617e0276c9', N'779-359-1824', NULL, 1, CAST(N'1997-04-02T09:40:42.620' AS DateTime), CAST(N'1985-04-10T07:21:54.120' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd0e862c4-b8c9-8381-5044-78a5c35337c5', N'5bef4028-a705-7fbd-e160-b69fd2734bed', N'5344891805', N'office', 1, CAST(N'1955-04-05T22:46:46.950' AS DateTime), CAST(N'2005-05-29T12:03:06.500' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b2953509-298c-e6d9-31f3-78f1e783fc48', N'fdeb567d-520c-392b-6480-471fb1dda1e6', N'989-1234082', N'cell', 1, CAST(N'1978-01-23T00:46:06.240' AS DateTime), CAST(N'1962-07-25T06:22:49.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c47a2799-cfe4-3ca2-ba05-794b68e82d10', N'5dcf8b32-c86e-2a23-c9a6-92fa36e57a79', N'2065473853', N'home', 1, CAST(N'1962-01-27T09:33:03.570' AS DateTime), CAST(N'1958-02-18T07:57:53.420' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'db2d4c61-ad73-be89-ab8e-79ff3c9dfa84', N'9efdcea9-dcb1-c6fe-2a65-70097f0757c0', N'225151-7548', NULL, 4, CAST(N'2004-10-05T12:43:54.740' AS DateTime), CAST(N'1992-09-12T14:24:20.920' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'56d3b3cb-252e-07bf-79ce-7ac85ce32704', N'8cfceb7c-f311-c333-1efd-873672fe655d', N'749091-0294', N'cell', 3, CAST(N'1994-04-08T16:52:13.040' AS DateTime), CAST(N'1977-09-06T12:15:28.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9919ac3e-c11e-8b92-a378-7af00e6aab70', N'3fcde392-008c-69e5-e9e3-76a0818da102', N'514-4037075', N'cell', 3, CAST(N'2015-09-09T11:29:13.170' AS DateTime), CAST(N'1960-06-21T13:18:31.050' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'162fe4e2-0fc8-0e2e-8773-7b0365b93cd7', N'372858e9-b431-7179-06d0-04aa0f74e8ed', N'778-454-4138', N'office', 1, CAST(N'2004-08-19T22:09:36.430' AS DateTime), CAST(N'1999-09-03T21:29:23.040' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'aaddfb45-eeb1-0f92-c95d-7b25ec07b5e6', N'8237e6ab-b7ac-b7d8-ea02-664db77d4cfd', N'6786184274', N'office', 1, CAST(N'2015-07-14T12:21:07.420' AS DateTime), CAST(N'2003-04-25T13:00:54.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9f2e3c65-731a-161d-d07e-7b5d69fbb668', N'298728c9-4438-1b8b-f404-273f09155a73', N'076-6397803', N'cell', 1, CAST(N'1961-03-11T06:33:39.080' AS DateTime), CAST(N'1970-08-04T03:23:33.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'529cd836-11aa-3611-4d5f-7c05bc0baebf', N'a671ff0a-4665-f5be-a52b-3e2ed1993283', N'078-4836912', NULL, 1, CAST(N'2004-10-23T01:07:15.510' AS DateTime), CAST(N'1991-11-21T10:06:51.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'99f9926c-be96-a8c8-2a91-7c199bd0f759', N'5edb671a-df8a-ef63-3cb8-9c7f67954c55', N'4969956569', NULL, 3, CAST(N'1988-09-19T22:51:13.480' AS DateTime), CAST(N'1998-12-21T10:27:05.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'36c132a0-74b5-9b2a-70ff-7c2086160d7e', N'699594fc-8567-ff40-bfaa-3f93ea74cfe9', N'9858413301', N'cell', 2, CAST(N'1982-01-25T01:07:24.970' AS DateTime), CAST(N'1959-10-19T08:03:32.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e22910d8-2372-0e2c-2b02-7d78e2272e2d', N'bacf3825-ce30-d5b3-92c4-b7fed4b4f475', N'8714248939', N'cell', 1, CAST(N'1963-10-09T21:10:42.230' AS DateTime), CAST(N'1957-08-04T19:03:26.700' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b825e7de-66b4-26cd-a3b5-7db27efc924a', N'168ac464-d503-88b8-8492-db391d28c6d5', N'5248269723', N'cell', 3, CAST(N'1964-06-23T22:01:41.710' AS DateTime), CAST(N'2004-03-16T10:16:37.350' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd732d1d1-f215-b911-a6ec-7e0fde12c581', N'4358786f-1608-3b43-45bb-fa3490213b2a', N'5074418215', N'cell', 2, CAST(N'1988-09-07T18:02:59.330' AS DateTime), CAST(N'1984-05-10T17:59:15.000' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b3e17372-7db6-0f74-1520-7e1b981cc094', N'0981e036-4a27-665c-9182-49d0bbe5f214', N'540-6059504', NULL, 1, CAST(N'1953-12-11T03:35:06.920' AS DateTime), CAST(N'2005-02-01T10:12:43.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c8d49b7e-fc3d-e850-1eb9-7e21f4c07cd8', N'80fa4d75-8b13-d4ac-71b7-886a1bfddbb2', N'354598-2535', N'office', 1, CAST(N'1960-12-01T21:03:12.680' AS DateTime), CAST(N'2011-05-03T21:17:35.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'76e2dcec-5821-cfac-10e0-7e663817ce52', N'5aff0ac2-87a7-b11a-fcbb-e0f36f3373a6', N'102-8970823', N'cell', 3, CAST(N'1969-09-17T09:53:23.850' AS DateTime), CAST(N'1992-10-22T23:21:01.960' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c7bac7c2-8eb1-bb77-bbed-7e90ee2ba069', N'ade26f20-b028-3fd4-08f9-5d85526d5aef', N'701-2680077', N'cell', 1, CAST(N'1989-01-22T17:58:39.480' AS DateTime), CAST(N'2009-11-22T14:46:52.860' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c7abfd0c-ce91-8b65-e04c-7fe1ec2311e1', N'8b224545-a456-60d4-5a08-79367fd275f5', N'970-4225877', N'home', 1, CAST(N'1981-06-12T21:27:20.100' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9ada48b4-8c27-2a3d-8d4f-801af02c0427', N'd69b8116-148b-1d8b-4887-7c5efb714995', N'198-3914164', N'cell', 1, CAST(N'1993-03-27T20:33:05.870' AS DateTime), CAST(N'2016-03-18T00:17:12.990' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'44ed2dd1-d6f8-9639-7cc3-802c4bc8a83d', N'2fa8c250-7e83-eb2e-47f8-fb1431b9802c', N'247-486-8577', N'home', 2, CAST(N'2004-10-27T09:21:12.340' AS DateTime), CAST(N'1992-01-14T19:36:48.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'10a5786a-f5cf-eda2-be96-80678cff49ef', N'4a7eaa44-ee26-6b64-c1fd-83956d431a06', N'952-556-8827', N'home', 1, CAST(N'1984-02-07T21:13:16.870' AS DateTime), CAST(N'1996-02-10T17:27:45.680' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8cfed6d9-5d03-43da-e3ab-8091c7224737', N'c95676fd-c864-95ad-4556-3ccb009388d8', N'600-732-5923', NULL, 1, CAST(N'1994-07-17T22:09:56.580' AS DateTime), CAST(N'1957-04-02T02:12:46.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'483d0b36-b2db-73ee-19d0-8127974fa864', N'5edb671a-df8a-ef63-3cb8-9c7f67954c55', N'7545712429', N'cell', 4, CAST(N'2003-09-30T14:11:57.260' AS DateTime), CAST(N'1998-10-23T19:34:48.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'965187a5-e8bc-1ae4-c0b4-81be34b92053', N'cba54225-2059-d4e5-6c39-3107c82b34c3', N'112100-1744', N'office', 1, CAST(N'1968-09-15T11:38:57.770' AS DateTime), CAST(N'2011-09-01T13:22:13.000' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1bb9508d-038e-c96f-46c9-829dda229b86', N'282452aa-edba-3df3-2034-aa055aa57c3a', N'664236-1753', N'cell', 2, CAST(N'1987-02-17T09:58:11.690' AS DateTime), CAST(N'2003-11-28T06:49:47.250' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bf40b7f3-5bed-4bdf-2d25-830eeb2a3513', N'c1e952d2-5bef-e2c0-a740-70a3019e057d', N'800-5955294', N'office', 2, CAST(N'2011-06-16T12:01:07.030' AS DateTime), CAST(N'2007-12-15T23:47:21.950' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd991ebef-a804-0936-d2bb-8330a76b4791', N'24604b18-3b24-f414-343b-60fe6fa4af91', N'668-461-6576', N'cell', 1, CAST(N'1982-05-06T06:53:27.900' AS DateTime), CAST(N'2016-07-16T03:32:56.870' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e87052c8-94fe-9485-1521-837d35972d19', N'94211eb1-79e8-9a82-ba1d-a6a10c2c5641', N'528-7743947', N'cell', 3, CAST(N'1965-08-18T00:20:05.990' AS DateTime), CAST(N'1963-06-10T16:26:10.860' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ea99e7ce-8e3f-d971-bf2d-8390b3f7571a', N'158e9183-ed1d-b5cf-c032-6b4e4eb650b1', N'155133-6155', N'cell', 2, CAST(N'1964-08-05T08:08:52.670' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1ebd2927-8560-7b77-16bf-83c8802c3e5a', N'e85618f7-f4ed-3bbe-9d56-86cd5f3a7fc9', N'999587-6381', N'cell', 2, CAST(N'2001-01-05T13:50:41.640' AS DateTime), CAST(N'1979-02-04T04:39:47.650' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'538b6af5-7d3d-30b8-3706-83e804e47fa5', N'31912448-aede-f726-aeba-2025c64cddfb', N'498890-3793', N'cell', 1, CAST(N'1961-06-19T02:41:28.390' AS DateTime), CAST(N'1988-10-28T23:22:18.280' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'dbf205a4-08c3-bf1c-7639-83f64cc824ac', N'94211eb1-79e8-9a82-ba1d-a6a10c2c5641', N'5844997082', N'office', 4, CAST(N'2014-01-15T10:34:48.490' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7abd1f6b-f4f0-e498-0562-8405f9ebee0b', N'348e692e-6c10-7ede-b229-88b0b1b4102b', N'871081-4510', N'office', 1, CAST(N'1985-02-10T17:53:44.960' AS DateTime), CAST(N'2001-07-18T18:29:07.670' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'77cd09ec-b3e3-247a-1a9c-840f006d4e74', N'de0a1662-190f-dce3-5919-86b9361fb8f1', N'778-6068513', N'office', 3, CAST(N'1988-09-14T10:17:05.190' AS DateTime), CAST(N'1971-03-31T12:53:41.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'87e5e66d-4ed8-2025-040d-8445bd12b33e', N'49849363-2487-4351-0761-57f581b71e46', N'967-4314411', N'office', 2, CAST(N'1992-07-23T13:16:16.840' AS DateTime), CAST(N'2000-12-21T23:26:12.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1fbade5d-22c9-56d3-cfa3-847034ae12dd', N'861a1c0c-ccd4-9d82-f634-5c20e6279f3e', N'091481-8222', N'cell', 2, CAST(N'2002-07-01T05:30:48.620' AS DateTime), CAST(N'1981-08-05T16:25:07.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9432a0aa-bda2-ae7b-dfaf-84d281414d66', N'08667db7-1a92-259b-d714-213e0cfc60cb', N'114-804-0088', N'office', 3, CAST(N'1978-11-28T03:54:47.780' AS DateTime), CAST(N'2016-04-28T08:05:56.200' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'440119c1-1474-6bd8-46a6-85598749d02d', N'065317fd-3715-d48e-040e-014261e6112e', N'0066593538', N'cell', 1, CAST(N'1980-10-30T16:31:29.950' AS DateTime), CAST(N'2016-04-23T05:59:38.620' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b4ebea14-9bf6-d903-7f89-869cab64ec52', N'6a793f2c-127d-dbfc-9875-d95327aeeb73', N'463-907-4703', NULL, 1, CAST(N'1988-02-19T18:28:31.790' AS DateTime), CAST(N'1979-12-09T01:26:51.480' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'34237d6e-291b-da24-b5c5-86ba4e234663', N'3fcde392-008c-69e5-e9e3-76a0818da102', N'221238-7694', N'home', 2, CAST(N'1991-11-04T13:20:40.830' AS DateTime), CAST(N'2018-01-09T14:10:38.780' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ed79c8bd-a599-d44b-f81b-86c9e37c6d99', N'31febac3-7421-fd2b-878f-69e666cc7146', N'765204-8409', NULL, 1, CAST(N'1990-12-02T19:07:00.530' AS DateTime), CAST(N'1955-05-12T23:33:56.180' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4fa6d0d9-d810-a468-5f77-86e56f070fe9', N'4a6ab7ba-6428-b29b-b2da-367bd564f464', N'6883078493', N'cell', 1, CAST(N'1958-07-05T19:34:24.960' AS DateTime), CAST(N'1981-05-11T07:35:27.460' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2c53028c-8ac0-b21a-b79f-86fdebeee6c0', N'2e16a7ba-e2a8-9ab8-afe4-940dbc5bf800', N'6025544480', N'cell', 3, CAST(N'2008-08-10T10:40:30.560' AS DateTime), CAST(N'1976-10-28T20:33:48.010' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b6bedc09-ff38-d457-ce8c-86ff5cab3678', N'6c2e1e75-59b9-8ff3-48b4-f5fd63854781', N'229348-7028', N'office', 3, CAST(N'2017-07-13T00:58:16.270' AS DateTime), CAST(N'1979-12-10T08:30:49.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e28be90e-138d-6b92-4e47-870cb0b3753e', N'7995ebdc-9ff1-f248-08fa-916b2e31263c', N'296-371-8023', N'home', 1, CAST(N'2017-04-29T01:11:26.820' AS DateTime), CAST(N'2015-11-28T05:36:49.390' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cfca1f41-9dcc-9c4b-cbbe-8737187f804d', N'5b640b1c-79aa-24b9-8368-75cdfddfc170', N'207280-1433', N'office', 1, CAST(N'2008-03-29T15:02:46.620' AS DateTime), CAST(N'1993-05-15T04:24:38.740' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'46b720a8-fa0c-b6f8-a990-8744829f21ae', N'94211eb1-79e8-9a82-ba1d-a6a10c2c5641', N'664-3426795', N'cell', 1, CAST(N'1959-06-25T06:19:55.060' AS DateTime), CAST(N'1956-09-23T03:53:27.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b8909046-683b-fcd8-1578-87843ac0f18e', N'cace4126-0c54-5e89-ea9d-7ef55979d312', N'794-9980665', N'cell', 1, CAST(N'2009-08-14T22:27:12.560' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'06bde5ad-a117-689a-e373-87a4b022fa9d', N'0d7dd9df-2567-2cef-1cac-cf2ff180d259', N'582-9345810', NULL, 1, CAST(N'1957-09-24T00:04:42.090' AS DateTime), CAST(N'1985-12-31T14:13:25.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f5486434-21f5-1876-a65b-87b85d083930', N'1de1cb13-289e-b3d7-8c88-f1918a7d14f9', N'554-6565229', NULL, 1, CAST(N'1991-02-09T13:53:09.710' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8a394536-44c8-2215-ddc0-87c46304bf55', N'a1c66f30-4e03-f513-9899-239f0268601f', N'657-1163185', N'cell', 4, CAST(N'2001-10-27T13:32:28.630' AS DateTime), CAST(N'2017-02-10T12:40:46.070' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ceca25c0-1c15-e05b-7044-87ff01ec81c0', N'4784ac4b-f0cd-82d6-151e-fcd52cc89739', N'548064-5493', N'cell', 1, CAST(N'1960-05-25T20:34:29.510' AS DateTime), CAST(N'2013-03-18T13:37:03.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'096dc940-d466-ef98-cebe-886dd885a37a', N'dbc54f3b-f076-0f18-e0f5-a4eb0cb5f151', N'328021-6202', N'home', 1, CAST(N'1980-04-26T23:32:53.190' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fd0ff782-92f7-6812-7185-88759441b12f', N'bc662b26-343d-bf13-ef5d-04bdb7d81624', N'625-1010288', N'cell', 1, CAST(N'1972-10-07T18:35:11.670' AS DateTime), CAST(N'1989-12-09T01:32:44.550' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cb942c49-3a3d-3658-1cd0-88784390f538', N'5edf2c53-c101-2a8d-4bb8-878d48bfa003', N'935-969-4481', N'home', 1, CAST(N'1982-12-07T15:03:51.980' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0fd6276e-400a-eb68-6c5f-88802bbac244', N'c257fa74-f21f-8f3c-1f3d-161d644ffffe', N'983999-5345', N'office', 3, CAST(N'2008-04-10T10:04:51.230' AS DateTime), CAST(N'1957-08-31T17:14:51.940' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7854fcf3-a11a-eaba-5d0f-89139fa7b792', N'b6abbd46-3e65-7588-08c1-d80ae139cd80', N'8815775951', N'cell', 1, CAST(N'2010-07-01T18:51:20.810' AS DateTime), CAST(N'1979-03-27T01:39:32.390' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'194cf669-acc5-9aca-9209-895c6496bcf1', N'74a002fd-ec01-437c-45e3-ab8a46daa2d3', N'620-7616271', N'cell', 3, CAST(N'2009-08-29T12:36:33.570' AS DateTime), CAST(N'1967-12-28T17:28:54.550' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0f35e8be-e787-87ea-d116-89c6444b4846', N'6821d0d4-1a2d-4e5d-b4e2-d0cd8f3d3efa', N'3236790287', N'office', 1, CAST(N'1975-03-10T19:00:37.810' AS DateTime), CAST(N'2013-02-15T18:50:37.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'68febdbb-d3e8-a607-da68-89cc3fbbec31', N'7c8d5ca6-cf76-1006-b587-7e30775eb3c8', N'725-2146813', NULL, 1, CAST(N'2008-09-12T10:15:54.660' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'58258301-aa3a-2e9d-ba01-89e890995aa2', N'f7e156df-e7ea-2fcd-6c48-609765ac8e2f', N'735-236-3952', NULL, 1, CAST(N'1963-01-21T18:02:25.260' AS DateTime), CAST(N'2004-11-07T09:35:30.680' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fe026231-a642-36d6-da19-8a7c57243bbc', N'5e82f887-d8f8-2c3f-0b40-b7e72d446bad', N'4991230462', N'office', 1, CAST(N'1988-04-28T06:38:30.680' AS DateTime), CAST(N'2007-01-06T07:13:14.830' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f6eff733-6e49-51a7-db15-8a966c0b711d', N'17ea96aa-4758-4fe9-672e-125d1175a1c5', N'039135-6889', NULL, 2, CAST(N'2009-11-06T02:57:45.470' AS DateTime), CAST(N'1995-01-07T11:37:21.090' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'25d2f159-5bd2-0861-95a7-8ab8e39b8323', N'3f22acd3-6bbf-6bee-6f05-831f87e1410d', N'399-991-4344', N'cell', 1, CAST(N'1968-12-27T12:05:50.210' AS DateTime), CAST(N'1960-12-15T00:19:28.830' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4fadd6c3-bbbb-0b7c-4219-8abec1eed05a', N'0caa0190-56c1-0ecf-73be-b896102e0ae2', N'255-4062911', N'cell', 1, CAST(N'1968-11-21T22:55:37.930' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'77e97a57-d422-b89b-f417-8abfcf81bf79', N'15eafd11-8ba1-0d3d-cbf1-95d5781193e7', N'358614-0274', N'office', 2, CAST(N'2013-01-11T06:20:58.900' AS DateTime), CAST(N'1981-01-09T16:35:14.690' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b90357bd-c13e-052f-ed86-8ad28325a9b7', N'4d472212-b448-62ef-9429-1fbfab5ef447', N'368-293-4667', NULL, 1, CAST(N'1962-06-23T08:09:37.220' AS DateTime), CAST(N'2018-01-07T06:20:53.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ed5a933a-7675-0ebc-a1f4-8b1089c1727f', N'0818efb8-ac5e-9564-c7d2-752fa5e467cf', N'594-516-0120', NULL, 1, CAST(N'2002-12-08T20:54:08.070' AS DateTime), CAST(N'1960-02-19T22:34:11.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5b41de68-1766-a988-b3a2-8b123a1095ce', N'b78de124-a67a-a73a-cb9b-a62859a9f064', N'470-106-4920', N'home', 1, CAST(N'1967-02-20T13:30:42.890' AS DateTime), CAST(N'2010-02-11T10:45:54.310' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9731446a-95a2-0958-57e7-8b16bb68bd71', N'0148d35c-71d7-2c8d-52d9-2b5c080200cb', N'3359434184', NULL, 1, CAST(N'1972-12-06T16:29:59.130' AS DateTime), CAST(N'1953-09-03T13:55:05.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2e1f1974-c2a4-d3d9-c7db-8b3d6ac70f6e', N'b2b0b384-5260-2019-ecba-2d97a95c0592', N'484-4120068', N'office', 2, CAST(N'1997-10-14T18:19:43.710' AS DateTime), CAST(N'1980-05-23T08:10:50.310' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1cab157a-bbc1-923c-c52b-8c79df28b61a', N'27bc56d8-044e-247b-91b8-5d80478e6b4d', N'957-721-3427', N'home', 1, CAST(N'1986-02-21T16:42:26.580' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cc5397fd-e45c-a046-c9a7-8cd4a0129d19', N'29b9897b-5d05-c5a5-c81e-cf9d76e2983a', N'917291-1329', N'home', 1, CAST(N'1967-05-09T08:33:40.190' AS DateTime), CAST(N'2013-01-09T02:30:38.430' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'08515b8f-6819-477d-1c0d-8d32a1dc3529', N'c62f0a66-2f74-2019-b20b-def8171ce234', N'506-498-2760', N'home', 2, CAST(N'2014-06-26T15:42:12.540' AS DateTime), CAST(N'1965-09-10T21:38:17.780' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e1f16551-52e9-d61c-a14f-8d9cd149966d', N'70982e04-d4ec-f7c8-4f07-e98307dbe822', N'619071-1108', N'home', 1, CAST(N'1953-09-02T09:56:59.120' AS DateTime), CAST(N'2013-11-09T16:47:13.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2ea0a899-c9ab-79b7-7bb5-8df84471aa19', N'a111a04d-3ff4-733b-bbd9-4b5c66759375', N'1292082758', N'home', 1, CAST(N'1970-11-15T04:34:36.890' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6c7821cf-f4c2-238b-9f38-8dfd2afff8a0', N'e12b2f16-8bfe-3bfc-0971-5d0c4806c504', N'576-647-7294', N'cell', 1, CAST(N'1974-08-27T01:59:40.910' AS DateTime), CAST(N'1990-04-15T09:30:52.100' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'53ee24a7-ac62-d30b-ebef-8e2c87eb8152', N'70b5f74f-d3be-9a84-b66d-c9e3ede68368', N'178-134-8486', N'cell', 1, CAST(N'1976-08-12T08:26:46.110' AS DateTime), CAST(N'1984-12-31T11:38:43.790' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'494d1f98-58a3-0ed7-b594-8e3f9ea19549', N'eb49798b-ed31-3834-62cc-41835d98d275', N'022-606-1067', N'cell', 1, CAST(N'1970-09-22T13:41:06.490' AS DateTime), CAST(N'1963-10-01T18:15:51.480' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c99734e5-d829-3bec-c765-8ebe0f8a5ef2', N'5d21f040-c17b-deb7-520b-d20b75e8d32f', N'121-849-0016', N'cell', 1, CAST(N'1982-06-03T00:37:20.950' AS DateTime), CAST(N'1985-11-24T22:57:28.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7647e8bd-3123-ace6-52bf-8eccbb894e95', N'961effff-888f-3115-1f31-7f94424f3dc8', N'656-197-9488', NULL, 1, CAST(N'1977-04-22T04:30:15.030' AS DateTime), CAST(N'1964-03-13T16:10:35.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ba9acf3a-4c5e-6a02-b18d-8ef4d0d2438b', N'172b78de-4b53-c847-8547-ebe193a31622', N'8575371643', N'home', 1, CAST(N'2011-04-12T17:53:14.490' AS DateTime), CAST(N'1975-07-25T13:30:09.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a9d10153-ee88-2757-f19a-8efbe36fd9e8', N'a327a627-4199-e783-e291-4a549e8fcfa9', N'3189592427', NULL, 2, CAST(N'2010-12-19T22:38:36.010' AS DateTime), CAST(N'1983-06-10T23:12:39.720' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'12e18f82-3db4-6ccd-e0a2-8f36837084fd', N'2ea8e8a5-14b0-ad67-3cdd-1ccf245d73d1', N'200-0782165', N'home', 1, CAST(N'1966-08-16T00:45:30.420' AS DateTime), CAST(N'1960-02-12T18:31:57.680' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8db4718a-e792-dcc2-ca65-8f5b98c4c33c', N'6db08f84-cbaa-27b3-fc0f-5fd62832b2bf', N'397590-2057', NULL, 2, CAST(N'1989-06-02T01:53:49.390' AS DateTime), CAST(N'1981-05-18T20:18:52.960' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'89e736d3-cb2f-5142-6252-8f8fc0cb9d00', N'8e4a2e23-0613-9c15-62d0-c2222bc013db', N'9558158438', N'cell', 5, CAST(N'2016-07-23T12:40:44.010' AS DateTime), CAST(N'1981-05-11T11:40:33.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1721a537-7fde-1427-5497-8fa7524deda4', N'298728c9-4438-1b8b-f404-273f09155a73', N'692-332-8137', N'cell', 3, CAST(N'2012-08-02T17:15:15.240' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b086d474-3454-21dc-bef0-8fdf1bd9b0bd', N'a595244a-ed3d-3d1f-e19b-a8288da63bb1', N'7427443705', NULL, 1, CAST(N'1972-03-27T06:44:03.570' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5af6c1df-11ef-394b-7f2f-90603cd11a3a', N'1f55ef41-5151-ec41-f403-8a3e65b01044', N'0489952530', N'cell', 4, CAST(N'2004-10-19T00:50:39.400' AS DateTime), CAST(N'1979-09-09T13:44:26.990' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cc3f4bb1-31e0-4eda-ffed-9068f2a251e4', N'636e5a42-a71c-3778-70b1-9dd4f1363399', N'528969-8183', NULL, 1, CAST(N'2004-12-19T18:21:40.910' AS DateTime), CAST(N'2017-02-10T19:26:21.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0e552ee5-e0a4-d55e-e755-91362a8c0c44', N'78a3154a-e10d-0f48-536d-0f8f264b8f8c', N'610-9615141', NULL, 2, CAST(N'1970-09-02T16:29:49.900' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'09f564a6-5962-df96-7abc-9159f07529cd', N'95def12d-601d-db1e-aa0c-95fd2eb2f34e', N'441-937-9589', N'office', 1, CAST(N'2003-01-05T04:55:48.220' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5716d40d-214c-13f1-c30a-9178dffde1a6', N'a3675614-52d8-b158-c564-7da7e5f3bd8b', N'5429024617', N'cell', 2, CAST(N'1981-12-23T13:58:57.210' AS DateTime), CAST(N'2001-03-20T16:57:41.060' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'74452f6f-a060-24d4-21ca-91904e58fdcf', N'3319eb7f-267e-aa54-3053-bd5f11162cdd', N'4798907363', N'cell', 1, CAST(N'1981-03-13T19:24:39.860' AS DateTime), CAST(N'1958-08-27T12:48:39.310' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'74bd869b-b69b-da30-5a45-9198c486118d', N'10c921a2-393f-d73d-bd8d-1eef0ad852e9', N'050-5691611', N'cell', 1, CAST(N'1957-02-04T05:00:52.620' AS DateTime), CAST(N'1992-05-26T07:23:38.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'956ed5e3-997c-b43b-9c15-91a4085fa8fb', N'028534bb-6854-ede9-001d-f5d5d18e33b8', N'094-4437432', N'office', 2, CAST(N'1973-07-26T08:39:59.160' AS DateTime), CAST(N'1975-10-25T05:56:35.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'68931be2-7ad7-c7fa-79a8-91cd7f21239a', N'90c758bf-3cff-30fc-1010-5152ff47b359', N'851319-3607', N'cell', 1, CAST(N'1983-11-28T20:44:23.090' AS DateTime), CAST(N'1971-03-19T23:07:43.000' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cfb78d13-2540-404a-e385-91d3c84abab9', N'8cfceb7c-f311-c333-1efd-873672fe655d', N'468-2696649', N'office', 2, CAST(N'1966-03-02T12:01:28.860' AS DateTime), CAST(N'1969-01-23T23:19:38.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c1c93fdf-dde6-e584-6f9f-92426aaed338', N'79e47169-5a21-b8bb-125b-f8b30afe66b7', N'734270-9488', N'cell', 2, CAST(N'1990-08-24T20:55:05.920' AS DateTime), CAST(N'2013-11-28T22:31:47.430' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd5a3056e-c748-9388-bb3e-92a6e5cae697', N'fa5ef110-4ff1-6a7f-e2f1-5122693c0fdb', N'5317211213', N'cell', 3, CAST(N'2015-12-29T12:06:18.760' AS DateTime), CAST(N'1958-09-01T13:09:47.240' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fcf2ff05-2d96-b199-c449-9326d55484ca', N'0d7dd9df-2567-2cef-1cac-cf2ff180d259', N'692-134-7881', N'cell', 2, CAST(N'2011-12-12T21:17:00.820' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'062191bc-b43a-9c20-920c-938ad9dc32b3', N'b652b338-111f-a52c-de9f-cfc12469a39b', N'6070748397', N'cell', 1, CAST(N'1960-07-02T17:52:32.460' AS DateTime), CAST(N'1995-08-21T22:49:15.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'36efd666-6de9-7741-c02c-93a7e6586014', N'ea1d492d-9e62-67e3-cc3b-7c9443974117', N'0996686858', NULL, 1, CAST(N'2001-07-21T22:00:58.930' AS DateTime), CAST(N'2006-09-18T20:16:50.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5c7c65f1-9a20-120e-05f0-941bc75145ca', N'd5dce10e-513c-449d-8e34-8fe771fa464a', N'986-651-9876', NULL, 2, CAST(N'2012-07-13T16:35:38.650' AS DateTime), CAST(N'2011-02-12T04:26:56.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9d57fc38-c478-db1c-93e5-946dd80056ff', N'fbe92c7e-8e20-6282-2cf9-511cdb5daa62', N'113882-1094', N'cell', 1, CAST(N'2003-10-26T13:38:31.890' AS DateTime), CAST(N'1953-04-26T08:10:20.400' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'213bdc8d-457d-733c-03ed-94b37b9eb319', N'0c25c3f8-af60-12bb-84ad-222755ede74f', N'732-0053364', NULL, 1, CAST(N'1970-03-11T13:25:44.010' AS DateTime), CAST(N'1964-09-03T07:27:46.480' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fadadddc-3e48-0f39-04a9-94e9c4c1403d', N'612cc783-bd84-acd9-1f2e-93bad43a660b', N'0134563510', N'office', 1, CAST(N'1975-06-23T19:13:56.140' AS DateTime), CAST(N'1997-01-19T18:36:45.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e72587a4-5a00-ad37-7b2b-9509c143ff98', N'b31a758e-e0c6-7886-5394-e1d4060f3514', N'3629199303', N'cell', 3, CAST(N'2000-05-30T17:10:42.070' AS DateTime), CAST(N'1985-10-08T16:43:42.430' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3c924e52-5828-84a9-1b10-95225c2cb0f9', N'04bf2c66-c894-e12e-b91b-2f6e15541103', N'654-755-7447', N'cell', 2, CAST(N'1988-05-18T07:22:41.740' AS DateTime), CAST(N'1974-01-04T04:28:05.500' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4fc90694-64fe-c70d-e0f9-9555ffcbb19e', N'09507f84-2108-305e-a561-5d8fda9c397f', N'465484-4572', NULL, 3, CAST(N'2018-10-17T08:21:34.640' AS DateTime), CAST(N'1983-01-23T06:14:20.740' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'28bcea27-8e92-6c14-56f6-95dc4cfbe28e', N'fa5ef110-4ff1-6a7f-e2f1-5122693c0fdb', N'6115048017', N'cell', 1, CAST(N'1984-02-18T02:16:20.770' AS DateTime), CAST(N'1957-11-14T22:47:16.120' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'316d4dd1-bd71-0c66-0777-9618793742ae', N'c257fa74-f21f-8f3c-1f3d-161d644ffffe', N'3889526837', N'office', 2, CAST(N'1987-06-25T00:56:35.520' AS DateTime), CAST(N'1977-11-22T16:51:20.740' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'02ecd174-7494-2af4-77f9-964795c6bf3f', N'598459e9-d698-d810-8c1f-12e00a3a0c6b', N'039-0415331', N'office', 1, CAST(N'2009-09-16T03:26:09.270' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'aa34d49b-9221-dcd7-42f1-964b2b0335ad', N'17ea96aa-4758-4fe9-672e-125d1175a1c5', N'987922-5128', N'cell', 1, CAST(N'2005-06-09T09:22:09.090' AS DateTime), CAST(N'1981-05-05T13:22:36.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'894ef1e5-d5cb-c582-cad4-966e82a6b7a0', N'e87c328b-5d85-f469-aa18-feb00de1b356', N'402-659-3362', NULL, 2, CAST(N'1999-08-28T13:42:06.840' AS DateTime), CAST(N'1963-03-09T15:21:32.710' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6a29d068-62f1-8081-ea4b-96f8a8f97aea', N'11cfe3d2-427a-beb9-fa0e-b8b3cc148409', N'768-837-7871', N'home', 1, CAST(N'1997-09-25T21:25:38.300' AS DateTime), CAST(N'1983-03-03T21:18:27.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'19a1ca92-63ae-1c8c-95c2-970286968288', N'95d03b0a-78b5-dc9f-6db3-4ea8eaed1193', N'360-9053558', N'home', 1, CAST(N'1990-09-14T09:15:19.360' AS DateTime), CAST(N'1967-05-17T15:47:48.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'df2aed94-af2c-c1b3-a4a1-970fd17ddfbe', N'78780482-51e3-7979-2a01-8356b1874f8c', N'448-819-4112', N'office', 2, CAST(N'2008-06-09T06:37:32.980' AS DateTime), CAST(N'1954-02-25T16:21:43.650' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'96dc2cf1-bed8-db15-d40e-976705d74d12', N'49b0f9f1-1268-8425-9cde-a44d56e76d00', N'475-329-6879', N'office', 1, CAST(N'1989-05-16T02:32:53.710' AS DateTime), CAST(N'1954-08-02T18:44:20.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e25ff3e2-a879-6149-1a2a-97c8624262db', N'3bc73d83-5d9a-a9f6-2bb9-530d31d455b2', N'729-8471280', NULL, 2, CAST(N'1987-03-25T14:19:22.040' AS DateTime), CAST(N'2010-07-28T17:07:06.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7a1b45bd-e803-cfca-ef7f-97e4255d6420', N'cffcdf67-800a-1141-bc12-ac40ff74f496', N'8565826460', N'office', 2, CAST(N'2017-11-25T09:15:49.750' AS DateTime), CAST(N'1961-12-01T08:38:39.320' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'309d7efb-cbd6-6077-8f4a-981499dd3127', N'6924b18e-d8b8-1fa4-edca-730551dcec7c', N'726-349-0232', N'cell', 1, CAST(N'1977-07-21T04:17:35.670' AS DateTime), CAST(N'1987-10-05T07:39:11.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2a2c0409-067e-1f5d-a9fc-984a8b5dc0e1', N'5aff0ac2-87a7-b11a-fcbb-e0f36f3373a6', N'390-9876779', N'cell', 2, CAST(N'1968-05-09T14:40:12.500' AS DateTime), CAST(N'1997-08-05T03:39:18.160' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c98ed68b-ed71-dc55-f45c-9853f34dee7e', N'cffcdf67-800a-1141-bc12-ac40ff74f496', N'7325434267', N'cell', 1, CAST(N'1990-10-17T17:34:30.460' AS DateTime), CAST(N'2003-10-11T00:29:27.070' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'659efb2d-a997-f9c2-dc68-985de074747b', N'3b97d90e-aa9c-89d8-66a3-37140194bfb5', N'763-533-2454', N'office', 2, CAST(N'2016-05-30T03:09:17.000' AS DateTime), CAST(N'1954-10-10T03:03:22.390' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5412fcbf-8462-5865-c559-9877572597fe', N'636e2c7a-1500-4042-39c1-cd5e4486822e', N'416-2169327', N'cell', 1, CAST(N'1994-03-15T13:25:54.580' AS DateTime), CAST(N'2007-06-03T10:59:33.400' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7dc1cbee-9f9b-80cb-97fc-98a513e67f98', N'6a134942-7954-a383-ab8d-ca2e5245c2eb', N'171-7748903', N'cell', 3, CAST(N'2011-12-01T12:16:45.510' AS DateTime), CAST(N'1995-06-30T01:58:29.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'db515d65-b56f-7b7c-82b3-9964472dc65b', N'70982e04-d4ec-f7c8-4f07-e98307dbe822', N'096432-2801', NULL, 2, CAST(N'1971-12-17T01:45:53.240' AS DateTime), CAST(N'1999-12-13T00:05:20.620' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e88212ed-0252-b366-35f7-9997b26f805b', N'f11641eb-bd58-38c6-9f05-65e2c0815485', N'5749822566', N'cell', 1, CAST(N'1958-11-08T01:00:45.360' AS DateTime), CAST(N'1959-11-20T23:38:43.560' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'040ea6c4-b7a9-490d-d469-999a296c8e13', N'b652b338-111f-a52c-de9f-cfc12469a39b', N'550232-1055', N'office', 2, CAST(N'2004-08-10T03:24:37.460' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'aad3c8b8-3d8e-0c75-826b-99a8e4f41952', N'd4dbd056-c0ec-92eb-e635-9e28e1e5638f', N'906019-3023', N'home', 1, CAST(N'1989-03-22T23:31:57.550' AS DateTime), CAST(N'1998-05-12T04:48:00.670' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'49359536-d259-b6c8-885a-99bb702e186c', N'd9158c1d-dc50-40e1-ccbc-082f28a03870', N'852-084-2878', N'cell', 1, CAST(N'1978-12-31T23:43:39.150' AS DateTime), CAST(N'1969-08-12T13:21:46.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'87fbefeb-a10d-c543-e76f-99fb39cf68cc', N'c2780b31-4733-ed48-a5ff-9b169ffddf8a', N'0139226211', NULL, 1, CAST(N'1962-02-11T20:51:30.950' AS DateTime), CAST(N'1968-10-25T18:24:00.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7c64b3d0-7bb8-db5f-34be-9aab3f92cf28', N'78496b1c-b6e0-c6f7-0356-5253b1011e64', N'158-6276746', N'cell', 2, CAST(N'1986-03-28T04:37:18.260' AS DateTime), CAST(N'1961-07-10T09:52:39.140' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'30728565-2b19-7e0e-5d28-9aed11a4c11d', N'90124dd5-ef9d-b13d-7b17-64fcd6260f0c', N'908-784-9129', N'cell', 1, CAST(N'2014-06-13T01:24:19.730' AS DateTime), CAST(N'1999-02-06T23:39:07.710' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'01df5e7d-9939-e00d-12f5-9b3f7c400462', N'd96cfa78-2286-cc66-7bfe-98d23a7e88e1', N'760-4275222', N'cell', 2, CAST(N'1985-01-08T08:07:54.650' AS DateTime), CAST(N'1962-04-17T23:10:51.820' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b5169c62-7929-ce20-c7ef-9b6f290997a7', N'da189b90-61e2-a231-965e-a5f4971d2705', N'193-706-4342', N'cell', 1, CAST(N'1958-07-17T21:57:08.790' AS DateTime), CAST(N'1958-05-09T17:57:13.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3ceb3cf0-a63b-7385-9773-9c26b7054724', N'a595244a-ed3d-3d1f-e19b-a8288da63bb1', N'3089566571', NULL, 2, CAST(N'2003-08-27T05:24:09.300' AS DateTime), CAST(N'1989-03-21T16:05:24.070' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'00e7d929-e9a8-9ca2-ec1a-9c4064ee0719', N'0fc77d6e-134c-0d88-acfe-4d10777b3712', N'8843225075', N'cell', 1, CAST(N'1978-02-15T03:47:07.630' AS DateTime), CAST(N'1960-05-01T21:18:20.950' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cb2ca5b9-8be0-8921-fe99-9cb36f9bb4ad', N'88f1fc78-12be-8de9-1178-2ac4004a0165', N'050-765-3548', N'cell', 1, CAST(N'1972-07-08T23:16:26.250' AS DateTime), CAST(N'1961-03-14T09:44:29.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e1793fba-58ca-562d-85f4-9ce828e15689', N'7b485aef-562f-5347-dc98-1dbb64c2ae37', N'7785966043', N'home', 1, CAST(N'2001-12-25T08:22:00.710' AS DateTime), CAST(N'2001-11-04T02:10:12.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'24dde078-8d29-dc8d-3bf4-9d0137369f8e', N'7ba99c42-ff5d-5a26-77fa-7cec462cd3d1', N'8935424424', N'cell', 1, CAST(N'1994-10-03T08:55:04.410' AS DateTime), CAST(N'2001-01-14T14:53:24.060' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'791be9c3-f274-f8b4-1871-9d26a20ad46a', N'a4074fb6-b8a0-0bd6-2b9c-f62fc9a05a0d', N'858-615-6212', NULL, 1, CAST(N'1967-11-24T13:57:14.330' AS DateTime), CAST(N'1996-08-02T03:46:00.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'94329747-d6ef-96e5-655a-9d3595ecf42c', N'9bcb97bb-897c-68e7-f30c-5c050489e3b9', N'2809709356', N'office', 2, CAST(N'1970-01-10T03:15:44.300' AS DateTime), CAST(N'1975-07-18T20:35:03.620' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4ddec17c-d0ac-0852-c101-9d8a0a9b2e4b', N'6f8a8be9-36c6-87e5-bcfb-c4bbbc493fe9', N'6135172812', N'cell', 1, CAST(N'1953-02-27T07:43:29.420' AS DateTime), CAST(N'2003-07-01T13:46:19.820' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'42539df6-a872-73d5-a7a6-9deec917ab74', N'88f1fc78-12be-8de9-1178-2ac4004a0165', N'026596-3532', N'home', 2, CAST(N'1976-03-15T11:11:23.060' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'eb8198dc-2b80-59be-e0d0-9e530b66cf1c', N'1e308786-0581-fb4f-f7a9-3aaa52e0ca27', N'624-7117704', N'office', 2, CAST(N'2005-06-01T20:59:51.150' AS DateTime), CAST(N'2015-02-15T08:43:35.200' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'20c49869-0c8d-de63-9104-9e7ed81cf6f0', N'cc92b8f3-ac35-3645-d442-c6f6acbf7dbc', N'516921-3892', N'cell', 1, CAST(N'1974-06-10T23:48:11.040' AS DateTime), CAST(N'2012-03-20T11:26:19.250' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ef96ac80-54c5-dcae-b491-9eaf46a4405e', N'26ca9df8-b57b-2b21-877f-48a3a578438d', N'153249-0436', N'cell', 1, CAST(N'1978-12-23T01:42:15.210' AS DateTime), CAST(N'2011-12-13T02:26:00.860' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a2e1328b-6d35-221a-3f9c-9ebe6ae4520e', N'7d15b850-34aa-f408-d2cf-f9bbe23d81b8', N'461-276-1837', N'cell', 1, CAST(N'1958-02-09T19:52:49.030' AS DateTime), CAST(N'2014-12-30T14:44:25.400' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1b3c53b4-e506-f5b5-e470-9ecd42fff324', N'5f205b6e-f28a-8512-f924-c6d25a44e0fe', N'012-862-3755', N'office', 1, CAST(N'1977-08-31T18:57:35.940' AS DateTime), CAST(N'1988-04-25T19:57:42.610' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1fcdcf4f-26ed-6612-a6b1-9f157a667d79', N'6b21b319-c60d-caa6-746f-18a29139a2e5', N'995-282-4185', N'cell', 1, CAST(N'1983-05-20T07:49:48.020' AS DateTime), CAST(N'1978-08-29T09:59:45.060' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e2f2146a-57cc-352a-5473-9f19c29fd176', N'6672ab2e-6fd6-ea33-71cc-00255a13d888', N'866-5730275', N'cell', 3, CAST(N'1999-12-10T19:35:55.130' AS DateTime), CAST(N'2013-05-04T09:16:16.980' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bf4f42bc-b398-f910-9bbc-9f1b93fa95ac', N'178cf09b-f48a-5a21-fe92-5bfac20585a9', N'6502586015', N'home', 1, CAST(N'1956-09-20T11:18:18.810' AS DateTime), CAST(N'2009-01-11T19:09:23.190' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'df64565c-5835-4161-186e-9f44b839f4f4', N'ff0af445-7afe-3eb0-bff0-22fb5c671be1', N'016-382-6287', N'cell', 1, CAST(N'1995-07-21T00:38:55.660' AS DateTime), CAST(N'1989-01-04T22:40:39.920' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5659d64f-be3f-1b7b-505e-9f4f2e582bb4', N'60796318-a0b8-2bb2-0315-bb65cc77bab4', N'234672-3189', N'cell', 2, CAST(N'2000-08-04T05:25:04.920' AS DateTime), CAST(N'1973-09-06T11:25:06.520' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8fd84908-6205-6b36-4846-a07bb22f2b08', N'daf0f920-d24f-dd4e-7577-64747c97a6dc', N'504-0183919', N'cell', 1, CAST(N'1991-11-02T22:51:32.380' AS DateTime), CAST(N'2014-10-25T07:47:40.200' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0e266267-9ef7-288b-b3a9-a088bda96a24', N'78a3154a-e10d-0f48-536d-0f8f264b8f8c', N'228-550-5179', N'home', 4, CAST(N'2015-06-05T20:37:13.290' AS DateTime), CAST(N'2013-03-16T20:43:12.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2860fea6-ddf5-8519-f625-a0a178e6bc0d', N'b78de124-a67a-a73a-cb9b-a62859a9f064', N'843-901-5709', N'cell', 3, CAST(N'1995-12-31T03:13:41.970' AS DateTime), CAST(N'1982-07-09T13:34:24.070' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0aa33e47-07e0-d632-6b97-a130663c6a1b', N'3d11e4a5-f418-9252-9af3-4cafba5e409d', N'7708407844', N'cell', 2, CAST(N'1973-12-16T03:49:49.670' AS DateTime), CAST(N'1955-06-17T18:23:42.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9b56e805-f321-47f7-6bdf-a1311b7d884e', N'bc37f7f3-ca42-9efc-8a5e-209ac495e1f9', N'006980-5894', NULL, 3, CAST(N'2009-04-11T23:44:17.890' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6aee64d8-d932-5655-03a9-a170e0aefd9e', N'065317fd-3715-d48e-040e-014261e6112e', N'914-474-2461', N'home', 3, CAST(N'2015-05-18T04:37:38.000' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e0b9cf45-a235-e0cc-d982-a174326977fe', N'b36ec6ff-be65-6803-f627-df67e39aefe8', N'4282542653', N'cell', 1, CAST(N'2004-02-13T01:06:16.060' AS DateTime), CAST(N'1978-05-20T07:15:27.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f95476d2-0396-f7ed-78e6-a176c1f330c6', N'b0c85e23-f239-e644-33f3-ff0a990233b9', N'926387-5691', N'cell', 1, CAST(N'1959-11-17T22:28:04.630' AS DateTime), CAST(N'1977-08-02T07:14:34.550' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ef2ccaa1-4ed3-925d-2556-a1888bccf824', N'6fd7186b-cd15-cbd5-55ce-d00e730a4796', N'139995-2188', NULL, 1, CAST(N'1966-07-14T03:51:02.820' AS DateTime), CAST(N'1966-05-05T23:33:49.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1c10004d-2250-bdd3-345e-a1abdb3f262c', N'334605ec-212d-2f1d-810c-b757db4f5ec4', N'858-269-1525', N'home', 1, CAST(N'2011-11-12T18:20:36.850' AS DateTime), CAST(N'1963-05-28T18:01:04.080' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0a640377-f8e0-1172-c280-a22d60fd583e', N'37b96da1-dffe-006c-af19-41a9b7111bb7', N'799845-3694', NULL, 1, CAST(N'2004-03-12T15:54:07.070' AS DateTime), CAST(N'1974-10-14T18:43:20.250' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd01ca622-16a8-e456-ddda-a24537e1eef2', N'f6961a1f-cddb-5fcd-3321-0f6e57fb764b', N'134852-3842', N'cell', 1, CAST(N'1970-04-12T21:05:55.640' AS DateTime), CAST(N'1977-06-21T15:38:44.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bfc1a3b1-d9ba-d044-e11e-a29cc137cdf9', N'd6054e37-94b5-f0d1-8102-c06117eba401', N'9748303133', N'cell', 1, CAST(N'2015-07-24T10:43:27.560' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'decb3df4-8bf7-97e7-0d9a-a29ce3877533', N'a3675614-52d8-b158-c564-7da7e5f3bd8b', N'2913133820', N'cell', 1, CAST(N'1957-04-20T05:33:12.100' AS DateTime), CAST(N'1963-05-28T16:59:18.400' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c908338c-909d-9e10-a388-a2c4b2dea74c', N'ca04c785-c3cc-f958-32e3-7da3515e3efe', N'258-587-8954', N'home', 1, CAST(N'1984-08-23T17:10:47.650' AS DateTime), CAST(N'2012-06-12T23:15:43.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'528b694d-e70e-970a-9383-a353f744b114', N'8919b2e9-6b4a-d261-668f-762d6c05295a', N'733682-1498', N'cell', 1, CAST(N'1979-04-17T20:16:02.530' AS DateTime), CAST(N'1986-03-05T10:07:14.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cbaed4d4-e065-08fb-e408-a3a15cb90925', N'7968c1fa-202e-6636-af2a-8baa0cd41a63', N'917-800-1589', N'cell', 1, CAST(N'1988-09-07T08:59:05.860' AS DateTime), CAST(N'1992-03-31T03:12:05.290' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'93bf6764-9f84-471c-6edb-a3e2b7667396', N'a0f8c1fa-f149-2cb2-9899-6316ac9a0bf8', N'921243-9754', N'cell', 1, CAST(N'1964-09-22T10:19:51.450' AS DateTime), CAST(N'1986-04-17T19:06:39.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'72ebddee-53b9-ed48-f1f8-a3eb6d60b16d', N'8f1351b7-4ee4-e2f6-47b8-4d71aa86a85a', N'7507179759', NULL, 4, CAST(N'2008-03-10T03:37:59.940' AS DateTime), CAST(N'1969-01-23T07:43:01.430' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ad4a4892-5c41-b7ea-bdae-a41bf55c93d2', N'f962ca02-7f68-c467-c1da-a8051185294b', N'2843822775', N'office', 2, CAST(N'2004-06-23T05:46:07.910' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'579daf8a-1cf0-d7f5-29cf-a449bf47791a', N'9e7faeb6-8c63-5ca8-4948-eed4d49afeb1', N'881-663-4619', N'home', 1, CAST(N'1989-08-17T18:15:43.140' AS DateTime), CAST(N'1960-07-30T02:00:40.530' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'28e2b62b-5f0d-51a8-b9dc-a47eae899aea', N'15aed802-880e-f8d3-362a-c9d0b1b42b80', N'896-3082085', N'cell', 1, CAST(N'2010-01-06T20:32:23.250' AS DateTime), CAST(N'2010-04-28T16:55:01.230' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'16f6283e-fe3c-6749-a0c6-a4b3f35d7094', N'f3b50ba6-84ed-8c6e-1270-e99e78ecf575', N'052-744-9961', NULL, 2, CAST(N'2007-03-08T19:10:32.470' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'546d67c7-59be-71c7-fac5-a4ee3a371834', N'b31a758e-e0c6-7886-5394-e1d4060f3514', N'511-6846068', NULL, 2, CAST(N'1981-09-15T05:47:16.740' AS DateTime), CAST(N'2016-04-04T11:23:39.360' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0c88a242-da15-c446-e897-a538565202c2', N'04842085-eb7d-6a6f-a0a7-e8c79160dc8e', N'282-3419198', N'cell', 1, CAST(N'1967-11-07T22:11:11.320' AS DateTime), CAST(N'1962-04-09T20:30:14.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'03da99dc-3d60-06ae-2852-a599f66125c6', N'88ff71de-8e98-c7d2-7ed8-9c1abe66ee21', N'960-8071323', N'home', 1, CAST(N'1976-07-06T04:57:39.280' AS DateTime), CAST(N'1967-02-24T19:42:57.940' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1975beeb-cdf7-e94f-3c23-a5e21a928c0d', N'96ba137f-0fae-6526-5435-295bf94dd64b', N'210635-8591', N'cell', 1, CAST(N'1953-01-25T06:56:43.570' AS DateTime), CAST(N'1973-01-04T22:38:29.130' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b580923a-0d2f-d2d3-ab56-a5ef86d25a34', N'b572665d-3ca3-5699-4d32-16debd353a46', N'8745402590', N'home', 1, CAST(N'1964-04-07T11:08:31.880' AS DateTime), CAST(N'1995-05-16T02:59:20.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6e821f86-5dec-ad98-2193-a63e532023e3', N'be4dfe18-2c08-fba3-b97b-2905727713f5', N'675-0597766', N'cell', 2, CAST(N'2002-03-24T20:42:07.530' AS DateTime), CAST(N'1958-01-19T16:40:34.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b17185da-964e-20e1-287b-a6771b8afbb9', N'bf1bb397-5f54-6b82-75ec-cb2e4a40d4e6', N'570-333-3960', N'office', 2, CAST(N'1994-09-21T08:54:15.600' AS DateTime), CAST(N'2009-09-07T07:46:51.010' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'07fe0330-c4e3-4fb1-0b66-a72c78722c0e', N'f2f17e24-8914-8af9-56e3-44f534a52fd8', N'410-897-4788', N'cell', 1, CAST(N'1956-01-14T04:00:24.980' AS DateTime), CAST(N'2000-01-13T19:37:18.970' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6da4b2ae-1350-9551-13ad-a7470b454969', N'9e8e589b-1a22-d3ca-0a6f-17ba0e4e9906', N'2569383093', N'cell', 1, CAST(N'1964-02-04T01:19:41.830' AS DateTime), CAST(N'1990-04-20T06:17:35.370' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6bdc847a-22e3-1def-0482-a7523caee964', N'dd59b4f2-f445-d22e-b416-9d7e38ce3bd0', N'880252-9333', N'cell', 1, CAST(N'1996-06-30T06:35:15.200' AS DateTime), CAST(N'2017-12-03T00:36:24.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1f307413-d030-3e0b-cef9-a76b14ffbaa1', N'5b345208-16e0-a373-62cd-9a1b062260a0', N'0702317973', N'cell', 2, CAST(N'2016-05-05T07:46:20.730' AS DateTime), CAST(N'2005-09-25T18:54:16.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'050ede7c-e24e-9132-8e8b-a84bea66c7bf', N'0e0f1889-fbc2-8211-2455-297ee9dfa4b7', N'4843946196', N'office', 1, CAST(N'1975-10-27T06:49:30.140' AS DateTime), CAST(N'1973-01-28T19:03:29.240' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'438b344c-b826-4d4f-d004-a85d3e2d40c3', N'814cc60a-4ca9-879f-1e33-e1a70d68cb73', N'159076-6384', N'office', 1, CAST(N'1959-03-26T19:13:35.200' AS DateTime), CAST(N'2011-03-16T18:44:14.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0fb528b0-1538-d711-ed64-a86e32b553c8', N'fe01118a-9745-39bb-98b9-795b7e64901f', N'630-063-2939', N'office', 1, CAST(N'1966-12-17T19:42:58.750' AS DateTime), CAST(N'1955-11-25T12:28:12.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ee4aa6cc-7c22-040c-42a3-a91e53754774', N'da9d5a58-1dc3-10ab-b22c-e68bc624678c', N'263-3605999', N'cell', 1, CAST(N'1991-10-01T19:49:53.650' AS DateTime), CAST(N'1988-10-21T22:03:51.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd5cbfff3-68b7-2b85-f3f5-a936c6171350', N'b10b94f0-7bcf-45df-f1c0-bc14b274f5f6', N'244-4640060', NULL, 1, CAST(N'1980-01-02T03:44:19.480' AS DateTime), CAST(N'1974-01-02T16:09:13.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd260f7d7-984a-f89c-3ef3-a995ffbf8f3c', N'c82c5122-d676-13ee-520d-e4f2d2e683b9', N'223487-2050', N'cell', 1, CAST(N'1953-10-08T16:01:29.530' AS DateTime), CAST(N'1960-02-24T14:40:59.280' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'86fdcdf9-aca2-256b-7a76-a9e3b6d4f699', N'0c48d959-cc13-c5df-d1c9-9e359906f8f2', N'767-714-3211', N'office', 2, CAST(N'1991-11-22T15:01:43.170' AS DateTime), CAST(N'1976-06-15T09:30:53.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'64876ff5-a5af-3120-087d-a9ebe53d0f07', N'2d4bcb31-4795-2aea-0b28-7a8722ab6410', N'474426-5693', NULL, 4, CAST(N'2018-05-13T11:35:53.200' AS DateTime), CAST(N'2016-03-01T19:44:02.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'50f9eac2-325e-6b16-f10e-aa1869aacccd', N'324ab1fb-9b94-9b72-30b3-31653aabfdd2', N'534-5632422', N'cell', 1, CAST(N'1964-08-26T15:31:47.510' AS DateTime), CAST(N'2007-01-14T19:12:47.540' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd3a8aff5-a8ad-983b-6654-aa6558a6660a', N'0c9993ad-02d2-7b5d-d695-093e768602aa', N'820-7531227', N'cell', 1, CAST(N'1980-08-21T20:08:45.830' AS DateTime), CAST(N'2000-01-31T05:01:37.040' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7da206c5-d23c-95ef-9fb4-aa9d990ba89e', N'dec74ba5-8d46-acdd-adff-b1a16c98c27f', N'4148638186', NULL, 1, CAST(N'1965-04-26T15:22:52.680' AS DateTime), CAST(N'1977-05-16T20:44:53.870' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6b8a9034-0c3e-3406-48d9-aaa7c7e86916', N'bbf080ed-8ff7-de34-2166-04adf0722f3f', N'286-536-4634', N'cell', 3, CAST(N'1974-05-28T21:15:35.350' AS DateTime), CAST(N'2005-04-28T12:32:39.710' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'812ee444-1e27-345d-de6f-ab1e3102a3f7', N'19bc6981-b56d-8072-d6f0-bbffc74c5d86', N'441-186-8903', N'cell', 1, CAST(N'2006-11-12T07:03:54.000' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'464e81a6-3f38-d357-25d1-ab9f461c1ee5', N'5bd3905f-5785-cdff-b041-1250dc8cd135', N'708-7633558', N'cell', 1, CAST(N'1956-11-09T18:22:37.450' AS DateTime), CAST(N'1984-07-14T15:47:39.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'995fabc4-5df2-601b-618f-abab0469f03d', N'1ea29037-0643-5b11-a0f1-909bf0be2c5e', N'447-8449237', N'cell', 1, CAST(N'1959-06-23T09:16:50.170' AS DateTime), CAST(N'1963-06-19T11:45:52.730' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a86fc65f-ad1a-f44e-1af7-abd0f8706bf9', N'168ac464-d503-88b8-8492-db391d28c6d5', N'3010136000', N'home', 1, CAST(N'1959-01-11T10:42:06.650' AS DateTime), CAST(N'2005-05-06T23:11:42.720' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'be24f2d1-2a1a-a46c-957c-abe9039939c8', N'50caa968-57fb-9646-0073-236b8d194091', N'370-254-0982', NULL, 1, CAST(N'1971-12-13T06:10:15.510' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cb847727-e94d-d0ac-c305-ac469eeb8dd3', N'e85618f7-f4ed-3bbe-9d56-86cd5f3a7fc9', N'6258903063', N'office', 1, CAST(N'1983-08-22T22:05:00.330' AS DateTime), CAST(N'1972-07-31T11:10:09.720' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fd95c5e9-6fe4-2959-ded9-ac4991164a12', N'561436da-99bc-f1a1-334d-5160c04a03ed', N'525287-7334', N'cell', 1, CAST(N'1983-08-13T23:27:40.470' AS DateTime), CAST(N'1973-08-02T10:12:46.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'acfc9e70-54b9-f72c-e0df-ac4b7a3d8b2d', N'2d4bcb31-4795-2aea-0b28-7a8722ab6410', N'812285-2361', N'office', 1, CAST(N'1962-10-09T02:04:31.910' AS DateTime), CAST(N'2011-05-31T19:11:26.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9df67522-2f15-edfa-c131-acb288ac5523', N'09b32be4-eaea-10af-218f-4a60703ecc2b', N'541-0754558', N'cell', 3, CAST(N'2011-08-27T09:54:12.980' AS DateTime), CAST(N'1996-01-20T02:18:11.180' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6a3a2325-9a90-6736-266d-ad33bcaeded5', N'6765726a-51e7-9428-7d98-c4550edd4e12', N'344-751-5497', N'office', 1, CAST(N'1959-09-15T23:26:17.430' AS DateTime), CAST(N'1968-03-23T23:50:36.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'44edefe5-be29-6cb1-473b-ad57b7965de9', N'ee978c7b-83ef-0001-b027-52d56b8db100', N'6381965592', N'cell', 2, CAST(N'1982-12-02T10:51:30.780' AS DateTime), CAST(N'2007-12-08T07:59:11.410' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'797b7e7c-7a2d-f8eb-08ed-ad5a061216b1', N'f73cec99-ce9d-24ca-6686-002f290b093c', N'532138-6296', NULL, 2, CAST(N'1982-10-12T13:34:18.630' AS DateTime), CAST(N'1982-09-29T13:42:47.200' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a1452345-a8db-1ef9-5e4c-ad79b38b1daf', N'a155709e-9aec-1e48-2059-e5fecbc361b1', N'019-4391302', N'cell', 2, CAST(N'2002-02-15T12:27:14.280' AS DateTime), CAST(N'2000-10-26T03:32:21.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd4164574-a110-bf65-901e-aea8f3f5bff9', N'ab64dcc8-5a0d-0749-7a7e-6158568ee832', N'072-352-7876', N'home', 1, CAST(N'1969-06-15T11:36:44.760' AS DateTime), CAST(N'1980-06-17T04:36:10.080' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'38c2df31-8155-4188-53e6-af7cd4280f95', N'9dc0889b-b5e7-a83f-4cba-3f864551b8a8', N'713249-4624', N'office', 3, CAST(N'2013-07-30T03:22:03.550' AS DateTime), CAST(N'1987-09-02T19:02:06.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'270c75bd-bda3-2ea0-b648-af91a3503290', N'99136cdb-1f7b-c0ba-6e1b-8edf1d9db168', N'258953-7076', N'cell', 1, CAST(N'1957-09-06T06:41:56.380' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'66deb530-be09-2b5e-66c6-afa01be46d51', N'8e4a2e23-0613-9c15-62d0-c2222bc013db', N'823-146-8938', N'cell', 2, CAST(N'1972-08-31T21:31:32.130' AS DateTime), CAST(N'2005-08-21T14:22:22.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'26cd55a6-10ba-376b-bc61-b0096525456b', N'd9a7b48b-83d2-9e4f-d1ef-18a8451297b6', N'657-3054728', N'cell', 1, CAST(N'1991-04-12T19:31:27.290' AS DateTime), CAST(N'1977-04-30T01:02:13.660' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'10f2ce5f-8b5a-9d30-0d5d-b024e83c10d3', N'ba84b397-8cea-cefc-7cb7-c4cc1f841d2c', N'9590111238', N'cell', 1, CAST(N'2014-01-29T02:38:21.010' AS DateTime), CAST(N'1995-02-23T22:03:20.320' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'38e783eb-f8ea-dc75-0fd6-b0944d2bed95', N'95287c22-6e3e-204b-d430-b5b47ebb222e', N'1555465349', N'cell', 2, CAST(N'1966-06-22T20:50:41.840' AS DateTime), CAST(N'2018-12-24T06:14:36.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ca32420b-d3e2-6640-7668-b1123d00342e', N'2e017358-1fe7-316b-a1ed-38ed11e6ad18', N'6256198867', NULL, 2, CAST(N'1988-12-02T01:29:11.800' AS DateTime), CAST(N'1991-06-06T14:23:27.650' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd4a6ffe7-c6a1-1369-2eba-b16fb423a646', N'09fe4eae-193c-0012-7549-840229736197', N'6120073310', N'office', 2, CAST(N'2001-07-13T22:48:05.740' AS DateTime), CAST(N'1975-11-30T03:51:28.010' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e728d735-ec17-7cdb-1919-b2c9d88dbed9', N'0d35428a-db74-880c-40cf-11236cf85f3e', N'920-759-4063', N'office', 1, CAST(N'2007-05-06T21:21:29.360' AS DateTime), CAST(N'1994-10-20T20:20:54.560' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2e53596a-a642-17c5-cd7c-b31670cdf978', N'70ac03aa-d90e-c8a5-3099-c198ebd6b795', N'939-4805328', N'office', 1, CAST(N'1977-09-20T07:53:45.020' AS DateTime), CAST(N'1973-10-14T22:16:56.980' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b9b9005a-6bd3-e274-bbcf-b3993fa0c7e9', N'19d865a4-1ccd-0f2d-a757-0518e0b5e4a6', N'591-984-4238', N'cell', 1, CAST(N'1998-04-26T18:46:03.450' AS DateTime), CAST(N'1953-08-27T10:37:56.970' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd3446b08-42ae-5dfc-2ef4-b3f1313bdabc', N'202483a3-0e9a-5e84-2ed0-d7bb1b82d3f5', N'876017-7264', N'home', 1, CAST(N'1963-06-02T08:01:29.960' AS DateTime), CAST(N'2000-10-01T13:43:07.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'371f8606-b39c-a41e-423c-b463a035ff27', N'd2a8d1b1-ae9d-84f8-acb7-69cc67433f9f', N'7545975848', N'home', 1, CAST(N'1965-08-08T12:44:11.860' AS DateTime), CAST(N'2000-11-21T20:42:17.610' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6b7f83bb-8f75-b9cd-27e7-b465fdebc273', N'92aa84a7-72e3-c736-ff11-e8ccbc35ad91', N'354-777-6936', N'office', 1, CAST(N'1972-03-13T20:16:13.240' AS DateTime), CAST(N'1991-07-24T21:45:45.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'92b586b6-2953-3819-60eb-b4975da4be43', N'520ae4eb-91e7-cea0-29d6-3d5684e510aa', N'359-336-5588', N'cell', 4, CAST(N'2013-03-21T03:51:15.620' AS DateTime), CAST(N'1977-07-07T07:15:51.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'461efccb-0ac9-1a7c-f1fb-b4a212264322', N'4ee35e35-b543-f8ac-52cc-0c55fac33999', N'015-5107878', N'cell', 2, CAST(N'2018-02-04T00:24:16.000' AS DateTime), CAST(N'2006-02-26T13:46:57.460' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'766380f8-9bc9-23f9-ac0b-b4ce0470e669', N'de24dfab-e6d8-1bb6-f15c-fe52928b4017', N'325583-7759', N'home', 1, CAST(N'1977-09-08T04:50:22.090' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'04d7be59-dd1d-8898-cc5e-b4ce527110ef', N'6c67e0bb-ab21-07f5-3bb5-3729bd98d762', N'109-329-2246', N'home', 1, CAST(N'1992-03-24T01:51:13.340' AS DateTime), CAST(N'1956-05-07T05:49:09.610' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c2395ad2-c06c-6e61-04e1-b4d2d3df8b01', N'a0edc3fc-9d15-c026-3a37-6601518b3112', N'500-8487267', N'home', 1, CAST(N'1977-11-14T03:27:50.790' AS DateTime), CAST(N'1991-09-30T04:07:47.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3fa4679a-d367-a2e6-80d2-b503a0195767', N'c1e952d2-5bef-e2c0-a740-70a3019e057d', N'3951991362', N'cell', 1, CAST(N'2010-01-25T04:18:16.360' AS DateTime), CAST(N'2003-01-23T21:59:02.660' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'641db9bb-0462-e890-ac66-b50942d97c7e', N'49fba2f4-8c26-3c4f-e9f0-6a7e3ec256f7', N'175328-7295', N'office', 1, CAST(N'1956-11-29T12:00:06.240' AS DateTime), CAST(N'2012-04-27T19:12:42.000' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c9138896-b778-ceae-ba3f-b50f6063a6fe', N'2fa317f5-69e0-e9c2-8908-77345500106c', N'513799-5922', N'cell', 1, CAST(N'2003-09-22T06:18:42.660' AS DateTime), CAST(N'1987-05-12T05:27:10.080' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6d351910-7a3e-ddfc-4905-b535a65e4985', N'c8314f5b-e787-f1b6-32f4-38c9397e0bb8', N'570347-6305', NULL, 1, CAST(N'2006-08-23T14:00:18.970' AS DateTime), CAST(N'1982-04-10T06:51:24.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7ac71793-2dcc-819e-7bd8-b54d03ebd5e2', N'074055ed-73bc-f0e2-61d3-b0ed3e0e490f', N'9553375390', NULL, 3, CAST(N'2000-01-30T07:51:37.340' AS DateTime), CAST(N'1998-06-30T16:02:03.720' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'17ef843b-5b68-c146-859a-b5744de2508d', N'04ea5e3c-e72c-f996-ecd8-1093ceb27775', N'7807509444', NULL, 3, CAST(N'1995-09-06T21:46:11.520' AS DateTime), CAST(N'1970-09-26T18:58:44.520' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd8207dfc-e728-14a5-f74c-b5b6ca234fd5', N'bfe181d8-475e-61eb-63d0-f5cfe5dd0082', N'701-2378145', N'home', 1, CAST(N'1953-04-28T17:51:19.170' AS DateTime), CAST(N'1960-06-15T02:58:43.640' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'76c78d87-8176-b8b2-910a-b5e1cb428531', N'c89219ca-3cbd-729d-49f5-74fcd35f4813', N'089064-4128', N'cell', 2, CAST(N'1988-07-26T19:20:52.940' AS DateTime), CAST(N'1982-11-03T00:42:27.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fddbb3d3-12bf-b131-a11c-b61d49dc6040', N'074055ed-73bc-f0e2-61d3-b0ed3e0e490f', N'921-259-5361', N'office', 1, CAST(N'1958-01-30T07:49:52.120' AS DateTime), CAST(N'1971-07-18T15:51:50.470' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b539b0a5-9920-98a6-35ab-b69b8cd53db1', N'761303bd-4f3a-3490-4677-7e83c3e25968', N'2008275779', N'cell', 1, CAST(N'2018-10-06T23:23:52.830' AS DateTime), CAST(N'1978-09-13T13:15:36.530' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'41bc3b37-4f9c-4d9e-f644-b6a622cd3d05', N'19bc6981-b56d-8072-d6f0-bbffc74c5d86', N'947-630-3288', NULL, 2, CAST(N'2008-04-09T15:06:35.560' AS DateTime), CAST(N'2003-06-29T14:26:36.070' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c4834aa5-017a-f680-9f4e-b83695e8ceb9', N'26bed32d-9b63-d717-b6b5-8f2edb948db0', N'564124-5529', N'office', 1, CAST(N'1970-08-22T20:09:14.170' AS DateTime), CAST(N'2002-11-04T17:08:11.500' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'75f8987d-077a-95a0-ab37-b84d8e548d5e', N'96ba137f-0fae-6526-5435-295bf94dd64b', N'137-3590804', N'office', 2, CAST(N'2010-06-14T21:43:00.560' AS DateTime), CAST(N'2005-07-20T18:26:38.820' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8c81440f-e97c-c3f1-0745-ba0b4f956271', N'bc519677-9f85-eeb4-51eb-103cf4e8fb1d', N'853-9361805', N'office', 1, CAST(N'1967-11-25T03:34:47.810' AS DateTime), CAST(N'1997-01-19T13:46:23.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9a8eaf27-2e1f-1336-e307-ba0f621bd473', N'bd32c87e-d13a-f98d-6b1c-9f081c918f1b', N'651-568-2821', N'cell', 3, CAST(N'2007-03-05T01:19:25.590' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'059cd7b2-50f0-13c6-16d9-ba515a778bcd', N'32c5b4db-de85-311e-ab48-94c425903cf2', N'116-129-7320', N'office', 1, CAST(N'1965-02-14T16:30:46.180' AS DateTime), CAST(N'1971-08-01T01:23:44.260' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3e3452e3-7d20-098c-ecd8-ba76ef478219', N'561436da-99bc-f1a1-334d-5160c04a03ed', N'652-535-1886', N'office', 3, CAST(N'1999-04-12T06:46:27.590' AS DateTime), CAST(N'1956-09-20T15:04:50.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3c259a7a-2342-804b-712a-badc73c263a3', N'5aff0ac2-87a7-b11a-fcbb-e0f36f3373a6', N'3433832780', N'office', 1, CAST(N'1968-04-05T23:03:08.010' AS DateTime), CAST(N'2009-05-10T06:11:25.830' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'38f9eea9-96d0-b27d-9ff7-bb544abe7ce9', N'86cd2bc4-a850-3765-3e0c-85fd5b8e3d71', N'582-0777127', N'home', 1, CAST(N'1998-06-12T01:00:02.930' AS DateTime), CAST(N'1985-11-07T23:37:17.530' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a0049ba9-0af0-cee5-16c1-bb95d5417d2b', N'657e3be8-8380-8d1d-91bb-41ba2bf65db8', N'785-356-2013', N'cell', 1, CAST(N'1965-11-27T15:15:25.340' AS DateTime), CAST(N'2000-02-05T14:04:44.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0ecc40e8-c6df-406e-be9e-bc3fe48e0a43', N'a1c66f30-4e03-f513-9899-239f0268601f', N'021-990-3994', N'office', 3, CAST(N'1961-08-19T10:29:44.020' AS DateTime), CAST(N'1989-04-28T22:54:09.410' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'046c8f17-e9db-44f1-a7c5-bc549c1419c9', N'08667db7-1a92-259b-d714-213e0cfc60cb', N'140611-4361', N'cell', 2, CAST(N'1965-10-25T12:20:10.390' AS DateTime), CAST(N'1953-11-09T22:39:20.570' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fc99563e-2f8b-98da-2f5a-bcb47738c517', N'25eca171-6cdd-a8f3-53f4-eb2ce66e7b3a', N'289756-0652', N'cell', 1, CAST(N'1982-02-22T20:07:59.110' AS DateTime), CAST(N'1976-11-21T21:59:03.490' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fcf53ae9-7460-e9d5-1d96-bce3c8e3147f', N'7dfe2f2a-75fd-0df1-7843-a7a94282ab00', N'714475-0886', N'cell', 1, CAST(N'2015-06-19T05:21:23.190' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c81cce60-f1ab-b194-3ce0-bd7fce7c2197', N'b0dfcb7c-96d7-a1d9-6d60-6ae0133e2e92', N'008-964-5725', N'home', 1, CAST(N'1964-04-24T15:41:17.320' AS DateTime), CAST(N'1994-09-16T20:21:22.250' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1ce872ce-1101-d23a-1d69-bea49e9aa9c8', N'af1f6667-9afc-6cc6-05d2-2387775784a1', N'608408-1525', N'cell', 1, CAST(N'1960-07-14T22:56:12.790' AS DateTime), CAST(N'2009-10-29T23:44:35.200' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c712e5d1-43d9-1d89-763f-beaff87da71d', N'60796318-a0b8-2bb2-0315-bb65cc77bab4', N'213-412-3358', N'cell', 3, CAST(N'2014-10-23T04:13:52.830' AS DateTime), CAST(N'1983-08-03T10:32:14.560' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0b44e867-6fcc-aa9a-425f-bed5dafddf04', N'ee2bbc35-f462-355c-b1ad-d2d70e258073', N'876-051-6256', N'office', 1, CAST(N'1986-04-01T03:51:44.450' AS DateTime), CAST(N'1998-03-03T17:27:21.390' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4a5dc40c-1a1c-9767-fe9b-bf640e17cd3f', N'689f7e8d-bd65-eac6-a94a-9fbe8dd83dcd', N'922-414-0884', N'home', 2, CAST(N'2007-02-22T08:43:42.000' AS DateTime), CAST(N'1981-12-06T03:50:45.100' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a257ff40-d56b-9b2f-17fd-bf69a60b46d1', N'492a3ac6-9b1c-efa7-0691-023a4c29b65a', N'432-944-7059', N'cell', 3, CAST(N'2016-07-23T09:48:45.130' AS DateTime), CAST(N'1983-08-14T19:32:31.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5311bef3-11e8-8c8a-a796-bf6a64d37d05', N'c82c5122-d676-13ee-520d-e4f2d2e683b9', N'584698-1971', N'office', 4, CAST(N'1986-11-03T06:00:22.290' AS DateTime), CAST(N'1977-06-14T09:32:53.670' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c52fe983-0f51-66f1-b77a-bf7cc7c2ea5e', N'e65c9fc6-6666-6b89-c81a-7ab4426a2efd', N'363-383-1335', N'cell', 3, CAST(N'2000-12-03T05:20:33.970' AS DateTime), CAST(N'1982-07-11T02:54:28.470' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0f8371db-7fe0-6506-85e8-bfa52201e99d', N'4d5ca1f6-65d6-d10a-33f1-ab558d0b9de4', N'839-9757056', N'office', 2, CAST(N'2016-01-15T16:46:49.710' AS DateTime), CAST(N'2018-11-22T06:33:05.210' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a4e7aa72-2a59-09de-3389-bfb096e35d30', N'09b32be4-eaea-10af-218f-4a60703ecc2b', N'8624010421', N'cell', 1, CAST(N'1958-03-18T22:30:04.680' AS DateTime), CAST(N'1996-04-22T18:04:44.640' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'80d22556-af32-618e-2ebb-bfbcccae267f', N'2384913c-0421-394f-db46-6f1428d74293', N'381086-5362', N'cell', 1, CAST(N'1974-12-24T21:30:08.530' AS DateTime), CAST(N'1996-09-14T12:56:08.150' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'174ef316-77ad-9322-fd4f-c05ce138144c', N'f10716c0-932b-39dd-438e-919db7f7ecaf', N'477685-9547', N'office', 2, CAST(N'1976-05-11T11:05:53.170' AS DateTime), CAST(N'1997-12-14T08:41:35.140' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6f2fc72c-2645-ad1e-65d7-c0667d460a7c', N'4784ac4b-f0cd-82d6-151e-fcd52cc89739', N'247718-3856', N'cell', 2, CAST(N'1999-06-26T23:50:07.090' AS DateTime), CAST(N'2013-01-27T21:20:13.310' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'79fb4f79-5e2c-22c7-315c-c092a97a6d07', N'c750c6ab-ed17-4290-4cf1-741a519f6ab0', N'8242214677', N'cell', 1, CAST(N'2014-06-26T17:02:06.360' AS DateTime), CAST(N'1964-08-05T19:56:48.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'62225770-e8c4-7297-f1b6-c0f2141d65a2', N'd06a0502-33c2-2403-0391-75fe1f1e48a1', N'709023-2488', N'cell', 1, CAST(N'1966-08-20T16:39:06.980' AS DateTime), CAST(N'2018-05-17T12:26:22.060' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'375d005e-dca1-b060-7e44-c0f52d6c7e81', N'61538b7f-340e-a7e2-88aa-3623cfbb6682', N'452-496-9466', N'home', 1, CAST(N'2009-12-22T19:58:10.940' AS DateTime), CAST(N'2014-12-04T03:45:45.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'08ebd274-7f86-c309-0778-c15441533202', N'08d81c4e-fdc7-f4e3-464e-36022b129919', N'369-565-0694', N'cell', 1, CAST(N'2010-01-25T12:30:50.650' AS DateTime), CAST(N'2018-11-11T13:14:58.750' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'005ac36b-a787-4bd1-17ec-c1796013ca28', N'422821e9-e2fa-2858-41c0-a70ef072ae23', N'3119346994', N'office', 1, CAST(N'1960-08-27T05:50:04.000' AS DateTime), CAST(N'1983-10-21T16:36:00.300' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'429ec11d-77ba-dc69-cde7-c1e3aeb1fb63', N'a4f80cbb-38e2-c2f8-9276-c0b4307ac791', N'4799832588', N'office', 2, CAST(N'1985-12-06T02:54:01.000' AS DateTime), CAST(N'1956-01-19T16:23:04.690' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f6608109-b64d-3aba-0257-c1f42bca49aa', N'1e31a551-c277-84ef-2a30-9bafe7cfe8aa', N'184-4408548', N'home', 1, CAST(N'1957-12-19T05:52:09.240' AS DateTime), CAST(N'1964-12-15T20:21:43.990' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'710d6f13-e5d2-6c5e-fd6e-c284df44a04b', N'58e389ad-b7f9-9b59-134d-91e8d8084ae2', N'057-4886085', N'cell', 3, CAST(N'2016-05-18T04:29:55.100' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'165d00b4-0d3c-b422-8aea-c288c9593c45', N'b0d51a5a-82b2-72cd-d1cd-84297bdc9ce0', N'066-8677270', N'cell', 1, CAST(N'1980-08-19T03:40:00.450' AS DateTime), CAST(N'2004-05-16T15:29:17.490' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6b394d29-919a-67a5-9e98-c2a79af70f0a', N'5a4d3d1d-bac3-cb3a-3976-7e29d71f31bd', N'097-592-5677', N'cell', 2, CAST(N'2012-04-08T16:24:59.850' AS DateTime), CAST(N'1961-12-14T03:16:09.100' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'901d6bda-20d8-1cf4-9834-c2d56a4e2dd1', N'03467c84-eaad-024d-b28f-40b5e90ca9af', N'9837930737', N'office', 1, CAST(N'1953-02-18T12:29:44.440' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'74495174-3afa-e735-dac9-c2e59c7f2123', N'eff206df-aad1-3162-dd80-743b273278a1', N'848-877-1061', N'cell', 1, CAST(N'1978-09-11T14:53:10.790' AS DateTime), CAST(N'1965-09-11T10:17:55.420' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e9a111cc-b894-f311-3aa9-c30eac76359d', N'020f207c-0f38-8458-2970-3cba7beba0da', N'738-8633450', NULL, 1, CAST(N'2001-10-21T14:29:21.750' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'dcd74b83-778e-e53a-c9c6-c32c98130177', N'e08c2dac-5e73-e48b-6182-94ff1b6346c3', N'157-2304133', N'office', 1, CAST(N'1957-03-25T15:10:39.670' AS DateTime), CAST(N'1983-09-09T20:52:46.210' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'db3e7b01-3dd3-88f7-4344-c3712611f223', N'f66e2829-d221-bec6-d3da-1341c3a58ca4', N'895113-6985', N'cell', 2, CAST(N'1967-04-03T20:04:23.500' AS DateTime), CAST(N'1955-10-23T01:10:01.070' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5837cdba-1219-dd2d-7b50-c3adaf14e532', N'701e9dc9-7670-2d31-acfd-372bab0805c6', N'0783013810', N'home', 2, CAST(N'1973-05-25T12:51:56.490' AS DateTime), CAST(N'1982-02-05T08:37:45.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a42d132a-280d-6959-95e9-c427579acb82', N'd159c73d-f349-b4b0-57f8-4ee62f70d498', N'104-6909025', N'cell', 2, CAST(N'2001-06-14T00:19:36.540' AS DateTime), CAST(N'1990-09-11T07:48:24.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f69502b6-842d-cf78-cea1-c427869e8d48', N'ba00bc4e-e1c4-dbc7-952b-78286b867814', N'686-0169708', N'cell', 1, CAST(N'1975-03-08T22:43:59.210' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cb39c011-85ff-039f-c2d7-c49ab197fbc8', N'2a5ac717-3e6b-42ae-2305-f4c7edfad7d4', N'295591-6827', NULL, 2, CAST(N'1975-07-05T22:17:00.650' AS DateTime), CAST(N'1982-01-26T16:48:03.890' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'01485480-eafb-5413-24ed-c50539a74dd0', N'6c2e1e75-59b9-8ff3-48b4-f5fd63854781', N'7722646686', N'cell', 2, CAST(N'2004-04-09T22:12:06.100' AS DateTime), CAST(N'2000-06-07T04:34:11.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7d279aaf-a4f1-5f37-dd2c-c507dd6678dc', N'19472ae5-8f4d-2e60-68b1-081d23a86bd9', N'928-977-3615', N'office', 1, CAST(N'1980-02-13T01:49:53.800' AS DateTime), CAST(N'1955-07-08T11:18:48.620' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f57b8995-ae09-7926-af56-c540b301de58', N'a5c2ca5a-03e4-db38-7708-a16c9facfa21', N'201-589-1339', N'office', 1, CAST(N'1956-12-06T02:18:26.250' AS DateTime), CAST(N'1959-10-08T17:35:52.120' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8024fcde-a1de-f12f-1d22-c5a98f0a8e1e', N'8af05496-954a-18c8-d3db-8f3ff7abc3cf', N'377663-8049', N'cell', 1, CAST(N'1963-09-04T15:25:09.690' AS DateTime), CAST(N'2017-11-13T14:34:18.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'54a6c0e0-f4b6-bf93-2662-c5d9d83bf0fa', N'f3415d1a-bf76-00ce-59d8-048b20c6caf6', N'823363-1096', N'cell', 1, CAST(N'2007-09-14T11:02:46.020' AS DateTime), CAST(N'2014-04-30T09:30:54.000' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1a53f903-c25a-4b49-06d8-c61264366838', N'782747ba-5050-0b1e-9151-c93f51e2918d', N'6181017827', NULL, 1, CAST(N'2007-04-29T02:45:23.370' AS DateTime), CAST(N'1957-09-01T06:25:52.970' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1e756342-b918-5b20-2ad2-c64b52439538', N'74a002fd-ec01-437c-45e3-ab8a46daa2d3', N'029248-7903', N'office', 2, CAST(N'1989-04-11T18:34:06.250' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4134b57d-1475-a144-c7e7-c64d7ddaae04', N'323aad12-7ab2-c45f-53c4-6f11a940a191', N'9625351598', N'cell', 1, CAST(N'1980-02-13T10:53:00.720' AS DateTime), CAST(N'1996-02-22T18:10:25.360' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9427e4c4-45de-3a54-953f-c657fe3a071b', N'99136cdb-1f7b-c0ba-6e1b-8edf1d9db168', N'1646830347', N'office', 3, CAST(N'1980-04-01T18:23:22.160' AS DateTime), CAST(N'2000-02-09T13:32:22.860' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'086c8bc6-ac4c-1705-78e8-c6b68e9e4ca0', N'85e9a700-7843-d2da-3040-816b7e6b409e', N'823-0158391', N'cell', 1, CAST(N'1976-09-11T21:12:02.550' AS DateTime), CAST(N'1967-06-23T01:34:17.740' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8b94091f-cc83-b0bf-1aaf-c6f9de806686', N'8da6bb36-5683-ec54-e768-d094c03bec95', N'159410-8329', N'cell', 1, CAST(N'1979-02-10T03:46:42.440' AS DateTime), CAST(N'1953-05-16T23:24:18.430' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd5179381-81ec-2883-833a-c70555f4127b', N'2c082b65-9f94-f421-2e21-8558491980cd', N'8578295429', N'office', 1, CAST(N'2007-10-21T10:12:59.250' AS DateTime), CAST(N'1994-12-12T18:09:06.390' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'34681689-5098-25a9-7122-c72b7cea65b3', N'ecf7646f-3bee-4984-af36-279c10526d43', N'7788806971', N'office', 2, CAST(N'2007-04-08T00:23:20.750' AS DateTime), CAST(N'1988-04-20T04:28:26.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0429df91-782b-3b3c-972f-c7348f8b6414', N'7d718b4b-594b-883d-30bb-ec61f1f8b338', N'602-175-5259', NULL, 1, CAST(N'1960-10-19T12:14:14.410' AS DateTime), CAST(N'1959-06-17T12:11:36.180' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1f97dcad-cdde-d920-8558-c7881cf4f54b', N'afe99c8f-790f-c153-21e5-2de28bc8835a', N'7250339588', N'home', 1, CAST(N'1958-02-23T08:24:07.810' AS DateTime), CAST(N'1957-12-26T12:56:48.290' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4119da81-5472-52c5-56d5-c85168d39b41', N'd9ff15b9-f0b2-60f8-b5cc-2b3ef7cfd436', N'0870610285', N'cell', 1, CAST(N'1995-02-15T03:39:58.270' AS DateTime), CAST(N'1956-02-14T04:55:56.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ff99b7a2-cf0e-09dd-5a30-c89b15c9d473', N'861a1c0c-ccd4-9d82-f634-5c20e6279f3e', N'125-278-8890', N'office', 1, CAST(N'1954-08-01T03:08:24.530' AS DateTime), CAST(N'1953-07-03T16:12:37.780' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'01d4f54b-6fcd-b941-3a2b-c8bd8d85a6e2', N'b10b94f0-7bcf-45df-f1c0-bc14b274f5f6', N'685-087-3302', N'home', 4, CAST(N'1996-04-07T13:36:23.530' AS DateTime), CAST(N'2001-02-10T07:34:52.370' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fcc0f530-88de-a6b0-23e1-c96224754db4', N'6ef2814c-7b24-02e6-6aae-fc116a9eb867', N'978124-1770', NULL, 1, CAST(N'1975-06-01T09:29:54.510' AS DateTime), CAST(N'2014-08-25T10:53:33.180' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'692282b5-0884-f735-4eef-c9a98d499acf', N'351b3d1b-8f9b-da3e-c825-e9594b1c8485', N'8953898381', N'cell', 1, CAST(N'2001-08-17T14:40:50.260' AS DateTime), CAST(N'1972-12-08T08:29:46.320' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'061f957f-a607-56da-4c00-c9e46154d24c', N'562cc6c3-d3fe-c093-5347-fb67c2c881c3', N'554661-5694', N'cell', 2, CAST(N'1990-01-04T11:35:30.310' AS DateTime), CAST(N'2002-01-28T19:26:50.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ac8d7dc8-df21-2e6e-afa6-c9fe18e3969c', N'aa36e581-60cd-98da-fd80-54d707b3a5b4', N'137105-9665', N'cell', 1, CAST(N'1985-04-22T06:09:44.200' AS DateTime), CAST(N'1981-05-20T12:45:41.690' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'286dcab9-e693-98dd-372c-ca1af0e210c5', N'e8a7bb82-487a-d68e-fe9d-ee2e031d97b3', N'503631-2853', N'office', 1, CAST(N'2014-03-17T19:41:28.720' AS DateTime), CAST(N'1975-07-16T23:24:11.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cd78264d-f779-e55d-a120-ca5613d8d11f', N'c40bf9aa-974f-374c-dde1-bc6cf1080bc7', N'924636-4100', N'office', 1, CAST(N'2014-05-01T04:50:59.900' AS DateTime), CAST(N'1981-07-29T09:48:52.300' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2c8f7ca1-3f7b-1ac9-1fa5-ca63bcc491de', N'ae3b0719-2461-fc30-820b-13425696571b', N'4813169577', NULL, 3, CAST(N'1994-01-06T18:21:39.480' AS DateTime), CAST(N'1977-07-21T07:39:10.290' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'76e66aee-95b0-d002-581c-ca7926153f0a', N'ae3b0719-2461-fc30-820b-13425696571b', N'849-184-0674', N'cell', 1, CAST(N'1962-08-21T11:02:29.820' AS DateTime), CAST(N'1996-04-16T00:25:25.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'450a5138-31ad-546e-d57d-ca7fbb47d518', N'359b92f3-7ccb-a42f-3dce-3bdf005e4d90', N'199-6761567', N'home', 2, CAST(N'1978-05-22T05:55:01.500' AS DateTime), CAST(N'1994-09-27T17:00:43.940' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'272aa639-1391-af11-9bfa-ca88fdfd48cc', N'5edb671a-df8a-ef63-3cb8-9c7f67954c55', N'2650265559', N'office', 1, CAST(N'1959-09-23T16:00:56.210' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fb9b0643-9871-0e47-3ed3-caa906ab4daa', N'fe5cb64b-b0c8-5883-0700-b67d8ff937cf', N'845-2433360', N'office', 1, CAST(N'1981-05-13T06:43:33.910' AS DateTime), CAST(N'1973-11-11T18:04:10.730' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'11383c85-e743-b85f-e1f7-cad3f1cf13bf', N'09507f84-2108-305e-a561-5d8fda9c397f', N'3231445682', N'home', 2, CAST(N'2005-05-12T16:09:12.280' AS DateTime), CAST(N'1977-12-20T22:59:58.240' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4a452dba-8e15-639a-078c-cadd829fea8a', N'73b1db41-b2f9-3944-3d9f-9f022f7a4271', N'1287096782', N'cell', 1, CAST(N'1998-02-14T07:08:43.750' AS DateTime), CAST(N'1991-12-08T04:44:18.430' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'430487cc-8419-05d7-88c2-cbb4023bf1b5', N'09507f84-2108-305e-a561-5d8fda9c397f', N'173-375-3972', N'cell', 1, CAST(N'1970-10-26T05:49:12.070' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0bc76332-fa99-d2ce-feb1-cc21f9d7e24a', N'd70e5c50-6cae-392d-691f-f8b4a586c1c1', N'983454-6358', N'cell', 1, CAST(N'1988-08-12T11:08:01.900' AS DateTime), CAST(N'1995-04-26T16:19:54.780' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'64ff77c7-850b-90cf-15f1-cc3cf812c8c7', N'15e0898e-acc5-7cc1-e4f5-81f3f6e90e60', N'0652713780', N'office', 3, CAST(N'1974-02-06T21:25:42.610' AS DateTime), CAST(N'2018-08-13T14:02:36.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7a05721d-4cbf-003d-cbe0-cc7447d1154b', N'a94a318e-ec51-a120-a37a-84a4f636224a', N'178-4880272', N'cell', 1, CAST(N'1957-06-11T01:51:37.280' AS DateTime), CAST(N'1954-11-28T08:05:53.160' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'69ed91ef-8d1e-b820-54ac-cc80c71a900c', N'a96aa8ae-42f4-aa8c-574b-5306d8d3b7a2', N'274-525-4911', NULL, 2, CAST(N'2016-04-07T17:53:50.650' AS DateTime), CAST(N'2002-12-21T11:56:49.920' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9bced361-6462-eede-1dff-ccdcb7b85ea5', N'7b5c85ec-6d6c-2c44-7bfe-2dac625681fd', N'021-3536385', N'office', 1, CAST(N'1981-07-28T12:14:26.230' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'63005554-4333-cdbe-d8cf-cd6f14d890ec', N'e4adb2b2-8328-d573-9c91-16944aba2bec', N'255-549-5411', N'cell', 1, CAST(N'1959-09-24T21:13:36.670' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9c3d9186-c648-3cbf-0416-cd9d805d2752', N'3b97d90e-aa9c-89d8-66a3-37140194bfb5', N'912841-3375', N'cell', 1, CAST(N'1983-02-24T03:26:03.840' AS DateTime), CAST(N'1959-10-13T06:11:48.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd1c9e07b-c8d8-29ac-f696-cdea912ed17e', N'ec19197b-6083-f793-f059-2aa3e53ab4cd', N'362-942-0730', N'office', 1, CAST(N'1953-01-31T13:49:14.980' AS DateTime), CAST(N'2010-07-14T04:49:08.950' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2fea752f-5f78-d9f6-a40a-ce29168c3cd8', N'2caff05d-7f6e-a9f2-faa7-5d0b4de57c6f', N'223-705-0839', N'cell', 1, CAST(N'1986-01-24T14:12:39.470' AS DateTime), CAST(N'2009-09-18T12:22:34.250' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cf8f2cf7-5f23-e05d-0cd4-ce34b3f417dc', N'9efdcea9-dcb1-c6fe-2a65-70097f0757c0', N'909-1061430', N'cell', 2, CAST(N'1971-06-30T11:26:28.940' AS DateTime), CAST(N'2007-06-20T21:19:40.260' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'19c61d0e-3af3-41be-f1b4-ce75cbd60b34', N'08a664d7-2f1b-303a-605e-2258684fc892', N'355-8254835', N'cell', 1, CAST(N'2017-08-20T21:41:22.730' AS DateTime), CAST(N'1960-10-12T21:48:15.670' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6bbcd98d-cfa4-1ee6-fac1-ceafe60e0f4b', N'd25514d0-a69c-f268-7305-63d033e90782', N'3503026608', N'cell', 1, CAST(N'1987-06-08T08:51:19.390' AS DateTime), CAST(N'1959-08-28T23:22:10.130' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'17e1d836-77bb-a924-d335-ceddf6bb7c59', N'5c47f009-cad0-e6b4-59ea-46f2cfaf9eb9', N'097095-6508', N'office', 1, CAST(N'1984-11-16T06:35:41.120' AS DateTime), CAST(N'1997-08-03T17:05:12.830' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'36f274ee-b8c3-ec3a-8d09-cee1542f9724', N'04bf2c66-c894-e12e-b91b-2f6e15541103', N'344-530-8397', N'cell', 1, CAST(N'1958-06-20T02:46:37.500' AS DateTime), CAST(N'1982-04-20T15:49:47.050' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'37375c20-bd6a-9bf4-56f9-cf0935743ddc', N'00af1dd3-313f-d954-fd1a-be7d7ba9d546', N'645-8375870', NULL, 1, CAST(N'2009-11-16T00:16:51.670' AS DateTime), CAST(N'1987-09-03T11:32:44.180' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e12b7b22-bc43-2d73-9685-cf1c7b194f7f', N'2caff05d-7f6e-a9f2-faa7-5d0b4de57c6f', N'701-8192997', N'office', 2, CAST(N'2011-02-08T08:05:09.050' AS DateTime), CAST(N'1962-11-16T00:37:52.280' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a9b38ebf-6ee3-62f8-1d91-cf5700a57957', N'6f8c4c79-1044-b5bc-9c58-74ef4311fb1c', N'077-7345064', N'cell', 1, CAST(N'1976-03-13T01:48:08.180' AS DateTime), CAST(N'1969-02-12T17:59:09.550' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'942b39ef-b403-4c34-d578-cf84746a33b7', N'09082743-8883-4b1c-3d1b-c7a167a3b5fe', N'614722-7098', N'cell', 1, CAST(N'1977-01-11T19:13:57.920' AS DateTime), CAST(N'1965-03-26T16:49:32.260' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3d27770c-15d6-a55e-11ca-cfe90b21cf10', N'92bcf2e3-6cf7-1b13-a731-132ea3097502', N'9854429365', N'cell', 1, CAST(N'2008-10-22T03:29:16.040' AS DateTime), CAST(N'1989-10-22T10:56:19.890' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'33d7f1ed-9fc2-0a7f-45ed-d00a02e2a99f', N'56a51711-262f-2479-bba1-653d0c12445f', N'1578819210', N'office', 2, CAST(N'1993-01-05T01:48:54.410' AS DateTime), CAST(N'2018-03-20T09:58:36.820' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4f7ec849-dcaf-e386-6e44-d0a4d83e3911', N'c3c015f6-6e90-55f7-ea65-0b12c9128d30', N'021-0913061', N'office', 2, CAST(N'2009-04-06T09:30:23.880' AS DateTime), CAST(N'1962-03-19T05:56:22.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'077788a2-e4be-0a8b-3a31-d11540ea9127', N'79005d3c-a544-9cc0-1ad7-2616b8c5bfdc', N'213-3296001', N'cell', 3, CAST(N'2015-11-05T20:49:50.240' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3baf271b-7e16-4876-6143-d2512bf0ac96', N'0e884b8c-93a6-2beb-2d30-db7347381e77', N'529-336-4078', N'cell', 3, CAST(N'1990-12-06T09:32:31.310' AS DateTime), CAST(N'1985-09-02T04:58:25.310' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'96e59695-ea01-b367-177d-d27038bf1fbf', N'b98b5776-6d66-d14f-18e8-bcb7549cfe12', N'574-0927171', N'cell', 1, CAST(N'1990-02-18T06:52:24.410' AS DateTime), CAST(N'1980-05-21T07:42:43.250' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6ef27088-e8dc-4d4b-a47a-d2c26843e42b', N'55a9f545-02d5-5e74-4736-1666e6bf05c7', N'426926-4588', N'cell', 2, CAST(N'1989-09-23T07:24:59.440' AS DateTime), CAST(N'1993-06-04T01:22:58.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'983b6042-1f64-ee3c-3234-d34880ea1577', N'9bcb97bb-897c-68e7-f30c-5c050489e3b9', N'207119-5824', N'office', 1, CAST(N'1957-08-05T15:50:23.440' AS DateTime), CAST(N'1971-09-01T22:35:03.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'dd4581a3-745b-84c3-77ba-d4076dc00bb0', N'3d11e4a5-f418-9252-9af3-4cafba5e409d', N'4504965502', N'cell', 1, CAST(N'1964-01-23T01:39:48.930' AS DateTime), CAST(N'2015-01-19T09:23:20.470' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'32e8a91a-169d-d199-276a-d409aa1bbc5e', N'9e8ec18d-8013-334b-4a86-4a083c765c8b', N'169-668-7059', N'office', 2, CAST(N'1979-07-07T23:11:38.170' AS DateTime), CAST(N'1994-07-14T05:28:22.830' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'97fbb45d-6104-cd19-4438-d4d7fdc54cc6', N'896878f3-2c7b-cce3-387f-d8fdcc06b5c3', N'4511679749', N'office', 1, CAST(N'2000-08-18T12:14:59.440' AS DateTime), CAST(N'1970-12-17T06:45:38.140' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'61b68181-28ce-1edf-ec6a-d5180716904f', N'a52d01db-4e04-7da6-0dbb-fa66076574e1', N'501-891-6227', N'home', 3, CAST(N'1983-08-16T02:42:24.430' AS DateTime), CAST(N'1990-08-16T06:00:57.220' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9af08e44-36d1-b843-b689-d52fe9eca3e0', N'26a05e14-0c0c-3fc7-533d-5bcbc3fcade3', N'632-362-9520', N'cell', 2, CAST(N'1994-05-02T20:24:05.120' AS DateTime), CAST(N'1970-07-23T02:50:26.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'44fcb7b4-4ff3-511b-741c-d5bb0a082948', N'4358786f-1608-3b43-45bb-fa3490213b2a', N'8055723411', NULL, 3, CAST(N'2011-04-12T17:26:50.780' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a340ce0f-ced7-960c-3f56-d6f9201dd0dd', N'2b536fc3-31f4-6fbd-1da1-66866d432cc9', N'5674888730', N'home', 1, CAST(N'1983-03-15T21:26:49.170' AS DateTime), CAST(N'2003-08-04T07:22:51.280' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'04d0a8f3-a7f6-0cd6-3f9f-d74deab2c4b7', N'38946839-bc36-667c-36fe-4df1b3aa287d', N'052-290-1087', NULL, 1, CAST(N'1993-09-01T21:16:51.070' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ecdef0ba-28f0-467b-ad49-d8376152c4a9', N'0a626034-b069-7c62-1016-b9db40bdf875', N'426008-6712', N'home', 2, CAST(N'2016-04-08T01:19:00.450' AS DateTime), CAST(N'2010-09-19T10:20:38.610' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'46a3fff3-4937-992a-98a7-d89381eb1fac', N'cf476a7e-ca28-5c68-82ca-5282dc44f3aa', N'1140343205', N'office', 1, CAST(N'2018-09-18T10:39:35.870' AS DateTime), CAST(N'1963-10-28T20:21:59.470' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cc9a02af-c09a-1df4-39e6-d899e1c2c1e5', N'656e44cb-d249-7fc9-5f35-bba4321f5ecc', N'359-483-7435', N'cell', 1, CAST(N'1974-09-27T06:25:03.040' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'79a13ab7-07a2-1c4b-7005-d8e0444d5500', N'68b28bd3-ed09-b7c8-8f97-644845005173', N'1941552897', N'office', 1, CAST(N'2016-08-05T12:06:10.160' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'44269a9a-f56b-1a09-6c10-d9eec7badd8a', N'1eb1e4dc-410e-ee09-7cc0-99033ca9c8b7', N'884-3213632', N'cell', 1, CAST(N'1964-06-16T12:17:00.480' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e2868916-7715-0828-7244-daa935548c0d', N'0d14bb37-2feb-daf1-daa9-7f7b30f59506', N'039-111-0603', N'office', 2, CAST(N'1972-09-16T06:37:24.070' AS DateTime), CAST(N'1955-07-29T13:06:00.310' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4d57270f-3f1e-2d23-4023-daba27c03a13', N'f66e2829-d221-bec6-d3da-1341c3a58ca4', N'651993-5075', N'home', 1, CAST(N'1960-06-12T06:38:43.050' AS DateTime), CAST(N'2010-05-13T08:51:06.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'abf0ec9e-2d85-314a-b4fb-dadba780b72b', N'b98474e7-743b-3d3d-0281-59ec5378fd66', N'9281505432', N'office', 1, CAST(N'1957-11-08T22:30:14.260' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd0a53083-7a9a-179c-a0cf-db134e721e1f', N'fbde3bd9-359b-6c05-8bcc-c2e121d4dac2', N'7761162300', N'cell', 1, CAST(N'1954-01-04T06:47:30.370' AS DateTime), CAST(N'1965-12-29T02:28:41.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'49118872-97fe-4889-7cb4-dc6b97c2022f', N'26a05e14-0c0c-3fc7-533d-5bcbc3fcade3', N'409288-1557', N'office', 1, CAST(N'1958-11-23T21:01:52.820' AS DateTime), CAST(N'1977-10-22T08:01:20.730' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1f6f6047-38ff-0fd6-f351-dc6f6612a8bf', N'2c0f0b65-f5ef-02dc-4482-a372eac2b9eb', N'559474-8119', N'cell', 1, CAST(N'1958-08-20T23:05:36.410' AS DateTime), CAST(N'1976-08-16T18:50:15.070' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fe4b1834-f326-c165-d871-dcd3f8df6555', N'291172c1-c9a9-06e9-768a-5e7fa6dbefbc', N'0660824583', NULL, 1, CAST(N'1962-09-27T11:44:17.080' AS DateTime), CAST(N'1986-01-25T18:13:04.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1d7e0863-82d3-7035-6b78-de27c24b60eb', N'3409a2b2-48c9-5535-4129-ee5a5c3117bc', N'9244586122', N'cell', 1, CAST(N'1986-06-25T19:25:26.680' AS DateTime), CAST(N'1996-02-12T08:43:17.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6d1241c9-edf4-1d9b-c753-de8d47d2f2de', N'30a6be37-ae6d-a411-ac95-a3c5bd749a27', N'522-1776929', N'cell', 1, CAST(N'1989-02-26T05:29:29.040' AS DateTime), CAST(N'1988-12-16T14:46:22.230' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5311544e-db0a-c12e-4c3e-de9e6a3ac5b2', N'8b8767a8-dd65-2f5a-cc47-a836b58c5d0e', N'548-811-1941', N'cell', 1, CAST(N'2000-03-25T20:57:42.860' AS DateTime), CAST(N'2000-08-20T00:49:40.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'bc29f972-05f8-2b14-3efe-deb6d4ec71ab', N'80fa4d75-8b13-d4ac-71b7-886a1bfddbb2', N'063981-4179', N'office', 3, CAST(N'2000-02-25T06:49:14.050' AS DateTime), CAST(N'1984-07-11T21:14:34.520' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'42bf9965-e09e-43bc-9fe3-df66fbd70159', N'042c9909-edf2-b816-0de5-4d5f4db0b25c', N'929-9925017', N'office', 1, CAST(N'1971-07-31T15:22:20.170' AS DateTime), CAST(N'1959-07-07T03:02:31.950' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'24cd206f-13c3-9d8d-3602-dfa18f2e3d98', N'7f559f2c-a7b7-dad4-e008-3ba89c122837', N'149-0377450', N'cell', 1, CAST(N'1966-06-06T11:07:20.620' AS DateTime), CAST(N'1987-02-20T12:34:29.690' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'df00a11f-7af6-50c7-968d-dfa2f819204c', N'52a23ffb-91ad-bcc0-e93b-58abef6b787a', N'338-639-0948', N'office', 2, CAST(N'1987-05-22T04:38:10.350' AS DateTime), CAST(N'1963-05-18T01:26:42.920' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a60328f2-4839-83d6-114a-e08697f4a55f', N'5bef4028-a705-7fbd-e160-b69fd2734bed', N'339675-1603', N'cell', 2, CAST(N'1971-10-03T15:25:01.920' AS DateTime), CAST(N'2006-01-29T18:06:55.280' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'11ddc04a-6c88-8017-02e2-e0c36cb80dc4', N'29b9897b-5d05-c5a5-c81e-cf9d76e2983a', N'944770-6639', NULL, 2, CAST(N'1995-09-27T12:16:13.580' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'67d67fec-f33c-49e5-508a-e0f18f858768', N'5edb671a-df8a-ef63-3cb8-9c7f67954c55', N'401-7037426', N'cell', 2, CAST(N'1961-09-16T02:25:37.700' AS DateTime), CAST(N'2011-11-02T01:28:56.050' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e2fc2a5b-ea3f-fca6-1ec6-e1192c562dbc', N'4d787436-cc2c-9f68-77d5-58b34e6c4257', N'034-8526311', N'office', 1, CAST(N'1975-05-26T16:20:02.070' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8bb8fa80-33e8-787d-270b-e122aebc5e95', N'77845d60-8041-03a2-976d-582e6f466e25', N'6736689248', N'office', 1, CAST(N'1959-09-16T01:06:57.470' AS DateTime), CAST(N'1989-10-13T05:21:14.080' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4f097534-f7dd-6d37-6839-e1852e72ecf1', N'911bbb58-c32f-0617-d3d5-2ac178cfad1e', N'4789974172', N'cell', 2, CAST(N'2006-04-18T06:34:51.430' AS DateTime), CAST(N'1981-03-23T02:38:25.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd0c67a7e-3cf6-f073-40f9-e214b086b606', N'0e884b8c-93a6-2beb-2d30-db7347381e77', N'088525-9705', N'cell', 2, CAST(N'1984-01-06T23:47:36.810' AS DateTime), CAST(N'1977-09-07T04:58:48.060' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'00cf32f7-b036-0a35-f885-e2a889aac96b', N'70bbcd78-ae11-220e-23e7-0936fca70187', N'7935674174', N'office', 1, CAST(N'1991-01-21T03:20:02.570' AS DateTime), CAST(N'1977-08-02T19:28:21.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fb67b824-3244-faa1-bdb4-e2ba1312bfbd', N'4e33972d-d9eb-5415-e8a6-f43ee6c64d87', N'932308-7737', N'home', 2, CAST(N'1996-10-18T12:03:09.510' AS DateTime), CAST(N'1995-10-26T10:20:52.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'be3dbbc5-4525-2d1f-8219-e2d38ba0bbcd', N'8f1351b7-4ee4-e2f6-47b8-4d71aa86a85a', N'945744-3136', NULL, 2, CAST(N'1990-01-09T04:51:43.620' AS DateTime), CAST(N'2009-04-07T11:07:04.640' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b2bb5d95-931e-badf-6c1e-e32885d9a489', N'ec19197b-6083-f793-f059-2aa3e53ab4cd', N'123-351-5153', N'home', 2, CAST(N'1953-06-05T11:49:23.510' AS DateTime), CAST(N'2018-06-12T15:04:43.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7c4fcfe8-4324-a05c-5f47-e37269571331', N'd96a1178-72d9-304e-3cbd-a2fd64c43338', N'018745-8880', NULL, 1, CAST(N'1978-02-28T11:06:51.200' AS DateTime), CAST(N'2000-11-12T19:36:30.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6a183235-31db-2a74-356c-e37b32144327', N'da189b90-61e2-a231-965e-a5f4971d2705', N'5129915195', N'cell', 2, CAST(N'1994-02-01T05:40:03.730' AS DateTime), CAST(N'1957-05-26T08:42:26.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e2ca8c79-c4c6-b43d-f899-e389bf4a0b91', N'de0a1662-190f-dce3-5919-86b9361fb8f1', N'3926287140', N'home', 2, CAST(N'1972-04-30T02:41:20.220' AS DateTime), CAST(N'2004-07-21T12:21:50.820' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'086346ff-f23e-c33b-24d8-e40e16b6657e', N'7dbe3f53-7d08-9a11-90f0-1d1d51f8a768', N'773-9726861', N'office', 1, CAST(N'1980-08-21T00:50:50.850' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'956095ed-f9f7-ac30-d498-e43d547fe066', N'3df78c6c-56fe-2486-923d-d6dc72e8cd75', N'214480-6919', N'office', 1, CAST(N'1976-09-22T23:36:11.460' AS DateTime), CAST(N'1963-10-14T08:48:00.360' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f2a0c72e-9258-c398-fc27-e47a40dda602', N'35828ee9-1163-b31b-91d3-3f21320f0ba1', N'906-044-4016', N'office', 1, CAST(N'2018-10-21T03:41:03.520' AS DateTime), CAST(N'1994-01-05T14:15:13.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'992b326c-6374-4e65-2561-e4aac7d3e0e1', N'e290f8b5-c685-b432-45e1-0d905ce0ca90', N'0177726830', N'office', 1, CAST(N'1968-03-16T04:11:28.450' AS DateTime), CAST(N'1971-04-22T23:57:42.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'98a16061-d46c-a63b-963b-e4c0b9f298bb', N'2ad28961-418b-91be-d710-0a7d2748bb70', N'113748-6602', N'cell', 3, CAST(N'2009-02-10T21:43:28.810' AS DateTime), CAST(N'1976-06-02T11:26:08.460' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'28914378-4fef-4d9c-67a2-e4ed895f0672', N'f18f017d-04da-3bf3-7cc4-76f4f5d5eede', N'7597777251', N'home', 2, CAST(N'1992-12-02T12:46:34.970' AS DateTime), CAST(N'1965-06-16T14:53:27.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'78e7c873-223d-e674-ad2b-e5974db4af0d', N'8f157a23-af8a-ba68-9273-bdd79572f3af', N'232-0104152', N'office', 1, CAST(N'1954-04-17T09:22:22.810' AS DateTime), CAST(N'2001-09-09T13:37:27.010' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b6f80a88-6eb8-96ac-49cc-e5ad36cad8dc', N'8af05496-954a-18c8-d3db-8f3ff7abc3cf', N'335-736-6741', N'office', 2, CAST(N'2012-01-02T17:13:46.810' AS DateTime), CAST(N'1956-03-13T19:10:18.450' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ef9929a6-2685-d48c-dc79-e5b699cc4aa7', N'33ce60fc-7bc7-1fb5-1caf-11e929c0794b', N'170-3428669', N'cell', 2, CAST(N'1991-03-13T07:26:57.700' AS DateTime), CAST(N'2007-09-05T01:20:59.790' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd69cd391-f170-c454-ff4b-e5d6a55f3631', N'8e4a2e23-0613-9c15-62d0-c2222bc013db', N'095134-2166', N'cell', 1, CAST(N'1966-05-02T03:20:15.030' AS DateTime), CAST(N'2005-06-21T22:39:44.200' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2c82fa87-825f-a8c7-e9e1-e5e900f47efb', N'1a3f3055-4baf-c489-8544-19a64eb833f7', N'073-2782366', N'office', 2, CAST(N'1995-06-16T11:04:37.650' AS DateTime), CAST(N'2006-07-18T01:10:16.760' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'91eafe62-4ce8-82ca-004b-e5ebe3a121c0', N'898bb756-0d79-0c18-e1b5-11174f4db55f', N'744-106-7077', N'cell', 1, CAST(N'1953-01-05T11:32:56.270' AS DateTime), CAST(N'1964-06-24T14:29:59.400' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4089b8a2-4d46-b385-66eb-e62064afd7c7', N'cf3054c5-e3c8-d1c0-c7da-b777e94d9dc0', N'415-2879650', N'home', 1, CAST(N'2008-03-23T10:46:35.300' AS DateTime), CAST(N'1988-07-23T03:55:34.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'60a1e7d9-0781-d7ae-48f1-e6429c0a41af', N'99136cdb-1f7b-c0ba-6e1b-8edf1d9db168', N'084-1900702', N'cell', 2, CAST(N'1976-01-29T07:13:01.340' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9b4929d2-1228-5ac2-9bdd-e6659ab14d0a', N'70ac03aa-d90e-c8a5-3099-c198ebd6b795', N'789-315-7492', N'office', 2, CAST(N'2003-11-14T21:12:30.520' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0e215b9d-7616-58c9-2e93-e670e132c70a', N'15e0898e-acc5-7cc1-e4f5-81f3f6e90e60', N'6160895023', N'cell', 2, CAST(N'1971-07-29T04:47:15.420' AS DateTime), CAST(N'1984-10-01T22:50:39.890' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'388f6dc3-1805-594b-7368-e6aba0c061f2', N'8dc486cd-ab14-26d2-17a1-c8525018b191', N'3212814496', N'cell', 1, CAST(N'1970-07-30T19:51:22.140' AS DateTime), CAST(N'1955-01-25T04:18:43.430' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c77ba4a6-815d-25e5-7327-e6e361e73500', N'd915bc8c-364f-a338-1d8a-d7c16ef7c834', N'971-6365196', N'cell', 1, CAST(N'2005-12-22T15:00:57.930' AS DateTime), CAST(N'1985-11-19T06:59:06.640' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'82d4ccf3-0e8f-f447-f287-e6f55087d3b2', N'0370fe12-b835-98f2-e206-31cd889c163e', N'521873-0578', N'cell', 2, CAST(N'2011-06-15T13:04:11.970' AS DateTime), CAST(N'1992-06-24T21:34:03.350' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'eb4dc234-53d6-7888-d424-e700dd1520c5', N'e65c9fc6-6666-6b89-c81a-7ab4426a2efd', N'030-427-9999', NULL, 1, CAST(N'1973-05-11T00:53:03.860' AS DateTime), CAST(N'1970-08-16T03:14:03.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'747195ec-d592-cc15-3483-e7559659fffa', N'c601740b-14f4-07e2-49ec-af1a0181da12', N'732-9674869', N'cell', 2, CAST(N'2011-10-07T09:56:16.530' AS DateTime), CAST(N'1975-09-29T06:17:53.380' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'639ee15c-1abb-4580-8243-e762c80e6129', N'3cf28156-3972-f0f0-180a-4054b8bdc193', N'980-4169999', N'cell', 1, CAST(N'1963-07-11T16:01:16.000' AS DateTime), CAST(N'1958-07-02T02:15:45.470' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8b0ee737-fdac-ecb4-b2c9-e7d8b71c3df8', N'3471dadd-ed91-fa5f-3202-8fa609fea299', N'333823-5627', NULL, 1, CAST(N'2007-04-13T20:25:46.990' AS DateTime), CAST(N'1958-10-07T05:03:39.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ab85edd0-a63d-1962-83e7-e83650a2a78b', N'7dbe3f53-7d08-9a11-90f0-1d1d51f8a768', N'560846-3768', N'home', 3, CAST(N'2010-02-19T23:27:59.590' AS DateTime), CAST(N'2013-01-01T01:42:57.240' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'01f14323-382f-0f00-3dcd-e84f8f80f792', N'a5ea58a4-ca03-64cc-c64e-cedd4bbbdf9c', N'381-4773308', N'cell', 1, CAST(N'1997-04-18T00:56:18.220' AS DateTime), CAST(N'1985-05-22T11:20:20.580' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e05bcf98-e121-99f3-0929-e8c3e95a44e1', N'aa7b8bb5-4e9b-d617-c87c-6a4b07f010c5', N'1650778644', N'cell', 1, CAST(N'1987-07-17T14:14:56.000' AS DateTime), CAST(N'1996-01-25T22:55:34.160' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a1556818-a46b-3df4-c2cd-e919a668243b', N'9e8ec18d-8013-334b-4a86-4a083c765c8b', N'4506674212', N'office', 1, CAST(N'1977-08-17T16:17:29.010' AS DateTime), CAST(N'1997-09-05T13:23:38.710' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a69ec49f-eca9-c2f0-32e8-e93b8b1e4571', N'074055ed-73bc-f0e2-61d3-b0ed3e0e490f', N'447-2094144', N'cell', 2, CAST(N'1959-01-05T05:09:46.510' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'ba04de4e-3877-7212-8a6b-e945f4b4b3b8', N'981bc6c3-40d2-e40b-e20c-d7425e15a43a', N'8638365460', NULL, 1, CAST(N'2005-06-12T19:15:31.010' AS DateTime), CAST(N'2005-01-24T01:21:07.460' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4a9663d3-e577-cb75-5e98-e97cd50d5ac6', N'66a2ae7c-d3f9-80ee-081d-c5f419e71aec', N'614576-1769', N'home', 2, CAST(N'1992-05-19T11:41:51.100' AS DateTime), CAST(N'1979-06-23T00:21:19.870' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9839a455-a469-bc73-c798-e9abf5bc8814', N'cd7588f7-8e93-d68f-8f9e-baa9003c858b', N'8350039360', N'cell', 1, CAST(N'1985-07-05T08:08:07.700' AS DateTime), CAST(N'2018-03-20T20:52:51.250' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f413f4e7-e2c5-4420-8837-ea00f96a56b3', N'8e0089a3-0bd5-ea81-6f5e-f201ab99c5a1', N'070949-8128', N'cell', 1, CAST(N'1977-07-08T11:16:35.710' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'61768b68-b5c4-6669-89b4-ea16eb8ef831', N'3df78c6c-56fe-2486-923d-d6dc72e8cd75', N'8182260761', N'office', 2, CAST(N'1978-08-25T23:58:30.840' AS DateTime), CAST(N'1983-10-02T23:48:50.830' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8d03860a-6dea-9c89-d057-eb29686abc9b', N'63dd5192-750f-5983-4d46-5f3e32aa953d', N'1831395251', N'cell', 1, CAST(N'1996-09-18T09:52:26.260' AS DateTime), CAST(N'1999-09-05T06:05:29.650' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e6e8e1af-5eb3-d432-da22-eb30762dd031', N'c257fa74-f21f-8f3c-1f3d-161d644ffffe', N'8655138714', N'office', 1, CAST(N'1974-08-02T01:58:47.620' AS DateTime), CAST(N'1997-11-27T15:42:50.850' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'acd5cca5-446b-1e90-83cf-eb7abbb27f42', N'133b7235-5778-c21d-f5b7-588ee709bf6d', N'198-487-2769', N'home', 1, CAST(N'1985-08-23T14:59:23.430' AS DateTime), CAST(N'1959-08-14T15:03:13.840' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'161595a8-f832-615d-0b04-ec0d2860ba61', N'dc4abeda-2e99-6323-fb6b-8bb70b80adcb', N'113-1348652', N'cell', 1, CAST(N'1959-06-15T10:38:41.120' AS DateTime), CAST(N'1984-03-29T23:05:13.040' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'630386a8-bce2-0da8-9cf0-ec22ba2287df', N'534b2b03-c0bd-159b-7a90-44755d08c155', N'7617434422', N'office', 1, CAST(N'1991-12-23T10:10:22.770' AS DateTime), CAST(N'1986-06-28T11:44:41.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'277c432a-695e-c10b-78bc-ec7f572305d4', N'6672ab2e-6fd6-ea33-71cc-00255a13d888', N'743697-2855', N'cell', 1, CAST(N'1983-04-29T15:12:27.670' AS DateTime), CAST(N'2013-11-01T14:08:33.540' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7f982a01-d6e5-0cd9-aa80-ed0f8b1fec01', N'fb6eff33-c231-b08a-e06e-4118f840158e', N'457-295-2635', N'cell', 1, CAST(N'2012-12-08T14:14:13.810' AS DateTime), CAST(N'1999-12-16T02:14:49.690' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'dd23382d-b824-c282-2426-ed67d1ed57af', N'179db00c-33fe-1e15-f6c0-aa34d0c31d07', N'558-2614763', N'home', 1, CAST(N'1981-02-06T22:45:15.590' AS DateTime), CAST(N'1990-04-18T15:28:59.260' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5c8eaee7-a488-0ca5-c66f-ed975e6f29da', N'6651bd73-21a4-3ce0-02d6-43703f18ab3b', N'615-440-4140', N'office', 1, CAST(N'1970-06-11T08:07:24.930' AS DateTime), CAST(N'2005-01-05T11:55:59.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd69b04f3-ba12-3b11-3123-ed97909e28e3', N'82d9172a-b51e-4587-7208-270708049dd7', N'336076-4909', N'cell', 2, CAST(N'2012-06-19T00:37:09.630' AS DateTime), CAST(N'1964-01-15T06:12:37.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6378912d-ee73-52a8-5490-ed9b082771c6', N'5b7937a7-3420-4faf-0477-ec751bbd2ed1', N'6545936114', N'home', 3, CAST(N'1986-02-28T11:20:42.270' AS DateTime), CAST(N'1991-02-21T09:47:07.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c98734bd-e854-02f2-1934-ee43917c7bb1', N'06da91ea-bdac-7365-63fc-1850c6ed04d4', N'252-6830505', N'cell', 1, CAST(N'1977-01-09T18:01:59.740' AS DateTime), CAST(N'2005-01-08T23:37:38.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4a371615-a05c-ffa2-6e90-ee464cb5286d', N'420d6a7e-7be8-bfc3-be5b-694c59d43cb7', N'843-523-2200', N'office', 1, CAST(N'1970-02-27T07:40:35.130' AS DateTime), CAST(N'1980-12-20T13:58:18.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'85b3e977-7931-cb09-3502-ee58a9a8c39e', N'4774734b-48d5-713d-5d36-a963453164be', N'355-321-9290', N'cell', 3, CAST(N'1978-03-05T16:02:52.850' AS DateTime), CAST(N'1972-06-25T08:40:41.800' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0b5b64c4-754e-be32-98e7-ee90142334c2', N'1e308786-0581-fb4f-f7a9-3aaa52e0ca27', N'848-530-6306', N'office', 1, CAST(N'1957-02-17T00:34:24.770' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'786ec288-ce8a-b802-ad98-eed415ba5c27', N'562cc6c3-d3fe-c093-5347-fb67c2c881c3', N'8055282926', N'cell', 4, CAST(N'2008-06-10T00:44:34.320' AS DateTime), CAST(N'1988-01-04T20:23:10.930' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e5aa585f-5eea-64fb-9dd4-eed4e35f6e02', N'33ce60fc-7bc7-1fb5-1caf-11e929c0794b', N'457934-9081', NULL, 1, CAST(N'1961-03-03T19:51:46.630' AS DateTime), CAST(N'2000-11-15T07:01:56.680' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2816039f-2380-b711-cdc3-ef23f34ca8e9', N'78496b1c-b6e0-c6f7-0356-5253b1011e64', N'989441-1545', N'office', 3, CAST(N'2010-10-23T09:11:32.090' AS DateTime), CAST(N'2009-10-13T14:53:54.620' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd28756f4-e895-019e-8cf6-efc7293ba3ac', N'151a0626-1d8b-54a0-af91-2c2f9b40ff91', N'340-5024244', N'home', 1, CAST(N'1971-08-03T05:48:46.930' AS DateTime), CAST(N'1956-03-29T12:49:14.410' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c0c26d78-9d4f-338e-414a-f014ebae3fec', N'8413d734-24e4-53aa-38ff-7ba6b8cf19ba', N'699-760-0963', N'cell', 1, CAST(N'2014-07-04T11:53:18.170' AS DateTime), CAST(N'1998-01-21T22:07:21.200' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'affade49-731d-e642-c27e-f07794ced182', N'071bed5f-bd58-5d72-3a7d-d24ede146b93', N'4581501187', NULL, 1, CAST(N'2017-11-16T05:50:59.710' AS DateTime), CAST(N'1956-12-03T07:14:52.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'96b29fc4-405f-24ae-7fea-f081d0fbdfda', N'961effff-888f-3115-1f31-7f94424f3dc8', N'4703864727', N'home', 2, CAST(N'2000-08-23T00:40:27.630' AS DateTime), CAST(N'2012-04-24T12:30:41.780' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'6da21e51-54a2-6396-8880-f13a16b53954', N'3d632681-b862-b188-22e9-a399741f3931', N'3057160891', N'cell', 1, CAST(N'1969-12-06T03:44:39.580' AS DateTime), CAST(N'2017-07-04T13:47:27.560' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b54c4668-2f5f-be73-7adf-f151e7544725', N'0aa082b4-3e2e-120c-ae62-4cfccaf907d3', N'074-7116978', N'home', 1, CAST(N'1977-12-13T11:28:35.220' AS DateTime), CAST(N'2010-04-20T04:42:21.990' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2646d997-b781-702e-4a83-f16f8a69c2bf', N'edf8bc8d-198e-2287-3fa1-63697766bfd6', N'870-9155425', N'office', 1, CAST(N'1973-01-06T03:19:53.960' AS DateTime), CAST(N'1971-03-25T15:06:35.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'47266e57-e92c-9d90-9b94-f1709e2c0d59', N'd5dce10e-513c-449d-8e34-8fe771fa464a', N'165-9738392', NULL, 1, CAST(N'1964-08-26T02:16:37.230' AS DateTime), CAST(N'1994-10-06T17:15:16.290' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'576ce4f7-c893-ca9d-b49e-f1b02da01d5d', N'eff206df-aad1-3162-dd80-743b273278a1', N'222-741-2154', N'office', 2, CAST(N'1996-01-01T02:57:45.990' AS DateTime), CAST(N'1956-08-28T23:27:26.030' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'29c056f8-1c9b-b0cc-9715-f1ed255590f0', N'730b2dcd-0506-6235-440b-665bfeef13de', N'899085-0984', NULL, 1, CAST(N'2001-03-25T21:34:06.330' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'27cf10b8-9f0d-00d7-a9ab-f2f371329cc7', N'4d472212-b448-62ef-9429-1fbfab5ef447', N'366-8645103', N'cell', 4, CAST(N'2008-08-21T18:42:44.210' AS DateTime), CAST(N'1958-02-27T18:36:27.600' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'471c15c9-304c-07ec-39fb-f32858995f40', N'4016fa9d-1d61-a8a3-a60b-751e1d21b0a9', N'905-768-8736', N'office', 1, CAST(N'1989-11-30T20:25:09.630' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b37ddc1a-edcc-b18a-e129-f36607b15acf', N'60796318-a0b8-2bb2-0315-bb65cc77bab4', N'164-2929230', NULL, 1, CAST(N'1953-07-10T05:58:57.550' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1815a079-3e3b-762a-c1fb-f3777e1b7a43', N'f949d8e1-f3b0-bd12-2b2f-1fa0d58605ba', N'334675-6241', N'cell', 2, CAST(N'1973-03-09T10:06:01.880' AS DateTime), CAST(N'2014-10-29T19:00:20.330' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd3c2f8ec-f6af-43f3-6771-f38fc579f9e4', N'054e8c96-4c91-a5e0-e447-79e1d94a500e', N'120244-9060', N'home', 1, CAST(N'1975-04-19T08:13:08.880' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a4ace6dd-ee91-71a1-13e4-f3e179c39120', N'97fdf0a6-d20b-b2d8-dd38-43809d6037d6', N'745-930-0197', N'office', 2, CAST(N'2007-08-01T16:30:37.630' AS DateTime), CAST(N'1964-10-02T23:28:30.680' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9302da5f-35ef-27a2-39b9-f3ed030c077a', N'ebdeec85-334b-a1a6-7360-936a5e203b9b', N'6167895772', NULL, 1, CAST(N'1976-04-01T16:16:49.480' AS DateTime), CAST(N'2014-01-04T11:02:01.610' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'cf79a112-e0bf-c2a4-9923-f3f1d65ca289', N'09b32be4-eaea-10af-218f-4a60703ecc2b', N'995470-7588', N'office', 2, CAST(N'2004-02-02T12:06:36.050' AS DateTime), CAST(N'1955-06-06T17:58:08.130' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5142175c-eaa2-14a4-8db4-f3f9e0c38e11', N'78a3154a-e10d-0f48-536d-0f8f264b8f8c', N'038908-0013', N'cell', 1, CAST(N'1959-02-19T21:50:59.580' AS DateTime), CAST(N'1966-08-05T05:50:16.320' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c8bb8b03-e338-59f8-7ccc-f435cba9ce3d', N'fbe92c7e-8e20-6282-2cf9-511cdb5daa62', N'724-8991402', N'cell', 3, CAST(N'2017-08-20T05:47:32.700' AS DateTime), CAST(N'1999-11-16T05:40:21.590' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'72ebdc78-4001-8be0-2fdf-f44b172fd48c', N'2ffd9d63-441b-49e7-832e-aad877657e0c', N'572-198-0443', N'home', 2, CAST(N'1987-08-01T21:47:32.080' AS DateTime), CAST(N'2011-02-20T21:57:00.180' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'3ee12631-cea9-0eb5-000e-f47854f4b315', N'6d9de423-992d-7114-72f2-971ca5e9a099', N'398849-2282', N'home', 3, CAST(N'1990-10-28T22:06:46.160' AS DateTime), CAST(N'1967-02-15T07:42:28.710' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a7558946-acce-ac1a-c0de-f481ac178822', N'a4f80cbb-38e2-c2f8-9276-c0b4307ac791', N'415285-3427', N'cell', 1, CAST(N'1977-05-15T10:26:25.380' AS DateTime), CAST(N'1969-01-31T04:38:27.270' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0a944b78-968e-d8f8-8a99-f4c8093181da', N'ee978c7b-83ef-0001-b027-52d56b8db100', N'173-2706975', NULL, 1, CAST(N'1958-01-21T19:57:43.580' AS DateTime), CAST(N'2012-02-18T04:55:12.900' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd601584e-eb05-d6a7-fe67-f502a47bb63d', N'1f55ef41-5151-ec41-f403-8a3e65b01044', N'031-235-4087', N'office', 3, CAST(N'1999-10-09T00:11:55.280' AS DateTime), CAST(N'2003-05-08T05:15:26.720' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'1feceebd-33c3-4a32-e26c-f5c846804c3b', N'ea1d492d-9e62-67e3-cc3b-7c9443974117', N'023-978-9104', N'cell', 2, CAST(N'2012-04-30T10:58:26.690' AS DateTime), CAST(N'1954-12-20T20:29:51.160' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f2ee28d7-a41e-a4e3-dd46-f62541bf27da', N'6674dc3f-b735-80d6-adf4-b6098abdec53', N'964-612-4131', N'office', 2, CAST(N'1991-07-14T11:35:51.160' AS DateTime), CAST(N'1959-03-19T04:00:34.990' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'76e7ae84-ddd7-e01b-ef71-f64822ce910c', N'562cc6c3-d3fe-c093-5347-fb67c2c881c3', N'928-5802368', N'office', 3, CAST(N'1993-08-25T17:54:14.440' AS DateTime), CAST(N'1992-06-28T16:44:10.720' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0118f293-90d4-a232-cddb-f6d1d597a6c3', N'e87c328b-5d85-f469-aa18-feb00de1b356', N'319047-4123', N'home', 1, CAST(N'1960-11-13T15:57:13.990' AS DateTime), CAST(N'1975-07-19T00:50:48.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c952eb37-9bff-420a-9f39-f6ee3d5d5839', N'bf34923b-3175-2e40-bf1d-8c7d0ee96bbd', N'2536536785', N'office', 1, CAST(N'2008-12-06T13:23:03.830' AS DateTime), CAST(N'1978-12-15T18:21:06.940' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'900ddec4-024f-b59c-fd04-f704d9475013', N'f5076efb-20e9-9060-f690-b12282ed061d', N'2793747288', NULL, 1, CAST(N'1998-08-15T20:31:09.070' AS DateTime), CAST(N'1998-07-26T22:05:43.910' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a26a7cef-767a-165a-4525-f715fc28bdcb', N'75d2447e-0f66-f906-f633-2dbc70a3c287', N'478-262-8288', NULL, 1, CAST(N'1966-03-25T02:07:34.180' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'7bc3be0a-0aa5-4839-c84a-f729ec1cf78d', N'45f30b95-3dae-8a82-c4d4-b31df3253834', N'260-0512030', N'office', 1, CAST(N'1974-07-26T03:04:33.870' AS DateTime), CAST(N'2001-07-08T18:57:16.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'54e9f72d-65d4-2153-8596-f750e26d1c9e', N'b0c85e23-f239-e644-33f3-ff0a990233b9', N'112-6080667', N'cell', 2, CAST(N'1983-09-11T05:39:42.600' AS DateTime), CAST(N'2012-10-31T01:14:34.020' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'4f418da5-13a8-531d-b2f0-f776bd145215', N'1d832846-5931-e275-2a3f-587e748564a4', N'955524-2191', N'home', 1, CAST(N'2000-10-03T01:14:34.330' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'14bc8aa7-a646-7dea-7b4e-f79c5bb7dbd6', N'b10b94f0-7bcf-45df-f1c0-bc14b274f5f6', N'611-3976579', N'home', 2, CAST(N'1983-08-06T12:40:49.310' AS DateTime), CAST(N'1963-07-07T19:25:33.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'321f1d90-737d-b1b5-8f4e-f7d321551900', N'2137544c-b035-0156-91f0-17e76c8e4306', N'113-7979205', N'office', 1, CAST(N'1975-11-02T14:35:49.110' AS DateTime), CAST(N'1970-05-09T11:02:37.610' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'73690269-0efb-a304-300a-f86efa17e5de', N'bb9767c5-1598-bcd4-7c65-24dde030f7a3', N'523-566-0820', N'cell', 1, CAST(N'1986-10-18T04:37:21.840' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'5e970520-72ed-e71e-061f-f8afa63f69ee', N'31912448-aede-f726-aeba-2025c64cddfb', N'3505259542', N'home', 2, CAST(N'2006-08-26T02:25:21.410' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8f7d6e63-27b2-5346-f54c-f91bb8e9975a', N'08667db7-1a92-259b-d714-213e0cfc60cb', N'2234220072', N'cell', 4, CAST(N'2007-07-16T07:26:11.730' AS DateTime), CAST(N'1986-05-05T13:14:07.820' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'156f2c99-87b0-69ff-e6e1-f9379483a019', N'359b92f3-7ccb-a42f-3dce-3bdf005e4d90', N'0180353529', N'cell', 1, CAST(N'1960-02-18T00:29:27.560' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'34950524-ad95-818e-cc5d-f9af883e4e80', N'0370fe12-b835-98f2-e206-31cd889c163e', N'155-3317905', N'home', 1, CAST(N'1984-01-21T17:18:14.020' AS DateTime), CAST(N'2016-05-26T19:28:28.570' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'e8f8ecc2-9f1c-089e-bb1a-fb3ebc75e985', N'6674dc3f-b735-80d6-adf4-b6098abdec53', N'8652228631', N'office', 1, CAST(N'1981-12-03T13:02:23.270' AS DateTime), CAST(N'2008-05-26T01:40:28.770' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a5a76dff-1de4-fc9e-76fd-fb4e7368cecd', N'c05408fa-7d3f-c0a8-b100-a538ea339856', N'570-510-4881', N'office', 1, CAST(N'1995-06-12T07:14:42.820' AS DateTime), CAST(N'1976-06-24T18:17:20.780' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'12f73c6b-7750-fcdc-6cd5-fbd364fd5cf7', N'2d839837-3711-89c4-fade-885c9eb17a9e', N'066532-4139', N'cell', 2, CAST(N'1997-07-30T04:49:13.920' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8111ed1a-305b-a398-1bfd-fbd7e3beacc2', N'0c47010b-e6f4-0a99-21ad-03f86c3986fe', N'3143633283', N'home', 1, CAST(N'1982-04-01T02:23:59.330' AS DateTime), CAST(N'1959-12-07T02:58:26.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'754193bf-95f1-db53-5e76-fbfeae0cc3d7', N'8b224545-a456-60d4-5a08-79367fd275f5', N'499-3027841', N'home', 2, CAST(N'1997-11-06T01:28:55.440' AS DateTime), CAST(N'2004-04-09T09:13:19.210' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'56cdfae8-8dc0-f402-5249-fc01a438e651', N'e4f28c7c-a018-1ac3-ad93-1e224f10afb7', N'3813805420', N'office', 1, CAST(N'1988-04-10T10:38:18.930' AS DateTime), CAST(N'1959-09-20T07:37:56.550' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0923bbe3-bc34-e20f-c8c6-fc1e70fee9f5', N'a6160a25-386f-f4f4-1595-0ac758b3b576', N'482475-0219', N'office', 1, CAST(N'1988-11-13T02:41:43.110' AS DateTime), CAST(N'2000-03-26T07:47:46.100' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'0f58eefe-20ef-ec3c-0a66-fc31dab0ed36', N'ad9db893-4388-1845-5cc1-dab2d6600f95', N'0810035261', N'home', 1, CAST(N'2017-06-07T06:02:57.360' AS DateTime), CAST(N'1980-10-19T13:46:58.100' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'61663255-bc45-e845-00a8-fc5300d7ab96', N'b3569359-a438-02c0-a745-8268e83a67f9', N'249-9796115', N'office', 1, CAST(N'1964-10-23T23:19:44.880' AS DateTime), CAST(N'1955-03-09T21:38:16.360' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'b8d4c107-255d-f6d1-027d-fc696a9682d6', N'96c060a3-3303-de7c-93d6-75feb33bea04', N'3310300505', NULL, 1, CAST(N'1963-10-05T00:21:28.640' AS DateTime), CAST(N'2005-10-01T12:09:36.110' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'9cd16148-c782-921b-c330-fc88cf16e5ae', N'88274f9d-1358-da1d-69e9-58229df61a49', N'124-9096725', NULL, 1, CAST(N'1997-01-06T16:58:20.580' AS DateTime), CAST(N'1970-06-15T11:59:20.980' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a8ae7eab-b0e4-b774-eb1b-fc9359ddfde6', N'87778770-0095-c249-ae71-009fedcec639', N'9222082109', N'cell', 1, CAST(N'1999-10-18T11:36:47.030' AS DateTime), CAST(N'1976-01-06T07:08:54.630' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'c87f04bc-a863-99f2-6060-fc9a8340a495', N'56a51711-262f-2479-bba1-653d0c12445f', N'582-5665893', N'cell', 3, CAST(N'2010-08-25T07:46:36.280' AS DateTime), CAST(N'2009-11-30T16:16:46.240' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f0e97f70-bc2c-7b71-646a-fcfa61f309b9', N'8e4a2e23-0613-9c15-62d0-c2222bc013db', N'505543-6531', N'office', 4, CAST(N'1979-05-17T14:05:00.640' AS DateTime), CAST(N'1994-03-10T08:43:49.880' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'528a69ed-15c2-1af5-68db-fd291fcd8d05', N'158e9183-ed1d-b5cf-c032-6b4e4eb650b1', N'269-693-1240', N'cell', 3, CAST(N'1973-06-09T23:19:41.540' AS DateTime), NULL)
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'a7eb27bb-a5b5-bee9-5321-fd2a8310e2e9', N'9b870adb-4abb-2276-75cd-dc589f7ae762', N'3306666968', NULL, 1, CAST(N'1979-07-08T08:00:57.830' AS DateTime), CAST(N'1958-05-10T03:50:07.540' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'19a82a33-c731-c8f0-0ae2-fd78b7b57511', N'5f95f536-21ef-5961-b255-b41994289f94', N'071-689-8922', NULL, 2, CAST(N'1986-08-18T08:12:09.820' AS DateTime), CAST(N'1997-06-15T05:14:55.440' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fcb868b9-27c5-4d4a-febe-fdebfe726afb', N'd4c183ce-fd71-9b6a-be2d-31efb4a1ee13', N'0344454729', N'office', 1, CAST(N'1961-02-11T23:06:57.630' AS DateTime), CAST(N'2016-05-14T12:47:54.170' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8a859a19-b7f7-2cf6-7bbc-fe316572dc9b', N'0469f040-aea1-d804-8293-159d2335dc2e', N'0334002407', N'home', 2, CAST(N'1981-08-04T07:23:10.030' AS DateTime), CAST(N'1963-09-27T04:21:24.420' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'd65403a3-eeb4-ce2f-bb47-fe3daef7f4d0', N'699594fc-8567-ff40-bfaa-3f93ea74cfe9', N'881-3001928', N'office', 1, CAST(N'1955-11-09T05:14:02.170' AS DateTime), CAST(N'1982-07-28T16:50:20.490' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'454fe117-36fd-19dc-b556-fe647800daf2', N'ff0af445-7afe-3eb0-bff0-22fb5c671be1', N'8887501786', N'cell', 2, CAST(N'1996-04-26T16:40:25.190' AS DateTime), CAST(N'1972-11-04T06:23:56.810' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'2dcecc48-ab37-1576-da30-fe794dbaa023', N'fe823627-391e-cac1-3379-e50b91727860', N'910-6827391', N'home', 1, CAST(N'2018-06-12T13:45:01.420' AS DateTime), CAST(N'1962-07-07T07:10:30.480' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'8f21091e-48ff-5d1b-29ba-fe9f850ba5c8', N'8e0089a3-0bd5-ea81-6f5e-f201ab99c5a1', N'959183-0993', NULL, 2, CAST(N'1984-12-16T14:18:18.660' AS DateTime), CAST(N'1996-04-22T17:41:57.190' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'f858dfce-bdfa-e4d9-f8f6-fecaa31f0ba4', N'6db08f84-cbaa-27b3-fc0f-5fd62832b2bf', N'101590-1228', NULL, 1, CAST(N'1953-01-05T03:49:54.820' AS DateTime), CAST(N'2005-04-07T09:33:58.510' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'df612632-58dc-6ce6-0bbe-fedaad57781d', N'e6a26b9d-4fd9-77ac-9e13-92e17256a07f', N'3985291692', NULL, 2, CAST(N'2004-07-08T15:42:15.990' AS DateTime), CAST(N'2016-06-20T09:42:39.340' AS DateTime))
GO
INSERT [dbo].[Phone] ([Id], [ContactId], [Number], [Label], [Ordinal], [CreatedOn], [UpdatedOn]) VALUES (N'fa0c6789-a29f-d9c7-1dc6-ffaae006fc03', N'bcf33b19-2739-2e46-9b2f-51e1755dafea', N'254844-7840', N'cell', 1, CAST(N'1996-06-29T05:41:42.890' AS DateTime), CAST(N'1975-12-25T08:11:02.290' AS DateTime))
GO
ALTER TABLE [dbo].[Phone]  WITH CHECK ADD  CONSTRAINT [FK_Phone_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Phone] CHECK CONSTRAINT [FK_Phone_Contact]
GO
