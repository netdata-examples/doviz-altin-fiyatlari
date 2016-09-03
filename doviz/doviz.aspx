<%@ Page Language="C#" AutoEventWireup="true" CodeFile="doviz.aspx.cs" Inherits="doviz" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Döviz Kurları - Altın - Netdata</title>
    <meta id="description" runat="server" name="description" content="Anlık döviz kurlarının ve altın çeşitlerinin fiyatlarını gösteren sitedir." />
    <meta id="keywords" runat="server" name="keywords" content="Döviz kurları, altın, gram altın, dolar, euro, döviz, ons, çeyrek altın, yarım altın, tam altın, cumhuriyet altını, 
        ata altın, 14 ayar altın, 18 ayar altın, 22 ayar bilezik, ikibuçuk altın, beşli altın, gremse altın, reşat altın, hamit altın, gümüş, riyal, dinar, dirhem, kron, frang, çapraz kur hesap, döviz çevirici" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no" />
    <meta charset="utf-8">
    <meta http-equiv="refresh" content="60" />

    <link href="/doviz/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="/doviz/CSS/bootstrap-select.min.css" rel="stylesheet" />
    <link href="/doviz/CSS/loader.css" rel="stylesheet" />
    <link href="/doviz/CSS/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="/doviz/CSS/sitil.css" rel="stylesheet" />

    <script type="text/javascript" charset="windows-1254" src="/doviz/JS/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/doviz/JS/bootstrap.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/doviz/JS/bootstrap-select.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/doviz/JS/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/doviz/JS/dataTables.bootstrap.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/doviz/JS/bootbox.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/doviz/JS/main.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $(".loaderStore").fadeOut("slow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loaderStore">
            <div class='uil-reload-css'>
                <div></div>
            </div>
            <div class="divLoaderMesaj">
                <span class="spnLoaderMesajMetin"></span>
            </div>
        </div>

        <nav style="background: #4285F4; border-color: #1995dc;" class="navbar  navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a style="padding-top: 10px;" class="navbar-brand" href="http://www.netdata.com/">
                        <img src="/doviz/Img/logofornetsite2.png" alt="Netdata">
                    </a>
                </div>
                <div class="collapse navbar-collapse" id="myNavbar">
                    <ul class="nav navbar-nav">
                        <li class=""><a target="_blank" href="https://www.netdata.com/IFRAME/62758f2b"><span class="spnShowDatas">Örnek Verileri Göster</span></a></li>
                    </ul>
                </div>
            </div>
        </nav>


        <div class="container-fluid form-group">
            <div class="row text-center">
                <div class="col-xs-12 col-sm-9">
                    <div class="row">
                        <div class="col-xs-12 col-sm-4">
                            <div class="row form-group">
                                <img id="dovizResim" src="/doviz/img/doviz.jpg" class="img-thumbnail" />
                            </div>
                            <div class="row form-group">
                                <div class="col-xs-12">
                                    <div class="panel panel-danger">
                                        <div class="panel-heading">
                                            <div class="row">
                                                <div class="col-xs-offset-2 col-xs-8">
                                                    <label>Dönüşüm Hesap Tablosu</label>
                                                </div>
                                                <div class="col-xs-2 text-center">
                                                    <button id="btnDonusumTabloDetay" class="btn btn-danger btn-xs visible-xs" type="button">
                                                        <span class="glyphicon glyphicon-minus" aria-hidden="true"></span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divDonusumBody" class="panel-body">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#dovizCevirici" data-toggle="tab">Döviz Çevirici</a></li>
                                                <li><a href="#altinCevirici" data-toggle="tab">Altın Çevirici</a></li>
                                            </ul>
                                            <div class="tab-content">
                                                <div id="dovizCevirici" class="tab-pane fade in active">
                                                    <div class="row form-group">
                                                        <div class="col-xs-6">
                                                            <div class="radio">
                                                                <label>
                                                                    <input type="radio" value="alis" name="dovizRbtn" checked="checked" />
                                                                    Alış Fiyatı</label>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="radio">
                                                                <label>
                                                                    <input type="radio" value="satis" name="dovizRbtn" />
                                                                    Satış Fiyatı</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <div class="col-xs-12">
                                                            <div class="row form-group">
                                                                <div class="col-xs-5">
                                                                    <asp:DropDownList ID="ddlPara1" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                                <div class="col-xs-2 hesapIcon">
                                                                    <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                                </div>
                                                                <div class="col-xs-5">
                                                                    <asp:DropDownList ID="ddlPara2" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <div class="col-xs-12">
                                                                    <input id="txtPara1" type="number" class="form-control" placeholder="Değer giriniz..." onkeypress='return SadeceSayi(event)' />
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <button type="button" class="btn btn-danger" onclick="DovizCeviriYap(event)">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Dönüştür</button>
                                                            </div>
                                                            <div class="row ">
                                                                <label id="lblDovizDonusmusSonuc"></label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="altinCevirici" class="tab-pane fade">
                                                    <div class="row form-group">
                                                        <div class="col-xs-6">
                                                            <div class="radio">
                                                                <label>
                                                                    <input type="radio" value="alis" name="altinRbtn" checked="checked" />
                                                                    Alış Fiyatı</label>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="radio">
                                                                <label>
                                                                    <input type="radio" value="satis" name="altinRbtn" />
                                                                    Satış Fiyatı</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <div class="col-xs-12">
                                                            <div class="row form-group">
                                                                <div class="col-xs-5">
                                                                    <asp:DropDownList ID="ddlAltin1" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                                <div class="col-xs-2 hesapIcon">
                                                                    <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                                </div>
                                                                <div class="col-xs-5">
                                                                    <asp:DropDownList ID="ddlAltin2" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <div class="col-xs-12">
                                                                    <input id="txtAltin1" type="number" class="form-control" placeholder="Değer giriniz..." onkeypress='return SadeceSayi(event)' />
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <button type="button" class="btn btn-danger" onclick="AltinCeviriYap(event)">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Dönüştür</button>
                                                            </div>
                                                            <div class="row ">
                                                                <label id="lblAltinDonusmusSonuc"></label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <label class="text-danger">NOT : Bu sitedeki veriler www.doviz.com ' dan alınmıştır.</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-8">
                            <div class="row well">
                                <div class="col-xs-12">
                                    <label class="lblPopulerAciklama">Serbest Piyasa (Satış Fiyatları)</label>
                                </div>
                                <div class="col-xs-4">
                                    <div class="row form-group">
                                        <div class="col-xs-12">
                                            <label class="favoriBaslik">Dolar</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <asp:Label ID="lblDolar" runat="server" CssClass="favoriCevap"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-4">
                                    <div class="row form-group">
                                        <div class="col-xs-12">
                                            <label class="favoriBaslik">Euro</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <asp:Label ID="lblEuro" runat="server" CssClass="favoriCevap"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-4">
                                    <div class="row form-group">
                                        <div class="col-xs-12">
                                            <label class="favoriBaslik">Gram Altın</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <asp:Label ID="lblGramAltin" runat="server" CssClass="favoriCevap"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-xs-12">
                                    <h1>DÖVİZ KURLARI - ALTIN</h1>
                                </div>
                                <div class="col-xs-12">
                                    <h2>
                                        <asp:Label ID="lblTarih" runat="server"></asp:Label>
                                    </h2>
                                </div>
                                <div class="col-xs-12">
                                    <h2>
                                        <asp:Label ID="lblBaslik" runat="server"></asp:Label></h2>
                                </div>
                            </div>
                            <%--<div class="row form-group">
                                <div class="col-sm-offset-4 col-sm-4 col-xs-12">
                                    <select id="ddlKategori" class="form-control" onchange="KategoriDegistir()">
                                        <option value="0">Döviz Kurları</option>
                                        <option value="1">Altın Çeşitleri(Serbest Piyasa)</option>
                                        <option value="2">Gram Altın</option>
                                    </select>
                                </div>
                            </div>
                            <div id="divBankaCesitleri" class="row form-group">
                                <div class="col-sm-offset-4 col-sm-4 col-xs-12">
                                    <asp:DropDownList ID="ddlBankaCesitleri" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-xs-12">
                                    <button id="btnGoruntule" type="button" class="btn btn-danger" onclick="Goruntule()">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>Görüntüle</button>
                                </div>
                            </div>--%>
                            <div class="row form-group">
                                <div class="col-xs-12">
                                    <div class="table-responsive">
                                        <%--<asp:Literal ID="ltrIcerik" runat="server"></asp:Literal>--%>
                                        <table id="tblIcerik" class="table table-bordered">
                                            
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divSagLinkler" class="col-xs-12 col-sm-3 hidden-xs">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-xs-12">
                                    <ul>
                                        <asp:Literal ID="ltrSagLinkler" runat="server"></asp:Literal>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </form>

    <script>
        var data = [<%=TabloHtml%>];
        var baslik = [<%=TabloBaslikHtml%>];
        $("#tblIcerik").DataTable({
            "scrollY": "100%",
            "scrollCollapse": false,
            "bScrollcollapse": false,
            "bAutoWidth": true,
            "bFilter": false,
            "bPaginate": false,
            "info": false,
            "data": data,
            "columns": baslik,
            "responsive": true,
            "bSort": true,
            "order": [[2, 'desc']]
        });
    </script>
</body>
</html>
