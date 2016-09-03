using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using System.Data;
using System.Xml;
using System.Text;
using System.Web.Services;
using System.Globalization;

public partial class doviz : System.Web.UI.Page
{
    public static string dovizKurXml = "https://www.netdata.com/XML/25b2c958";
    public static string gramAltinXml = "https://www.netdata.com/XML/f0bd54e9";
    public static string serbestPiyasaAltinXml = "https://www.netdata.com/XML/cdb1eca4";


    public string TabloHtml = "";
    public string TabloBaslikHtml = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        PopulerKurlariGoster();
        ParaCesitleriniYukle();
        lblTarih.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
        IList<string> segmentler = Request.GetFriendlyUrlSegments();
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        DataSet ds = new DataSet();

        if (segmentler.Count == 2)
        {
            string kategori = segmentler[0].ToString();
            if (kategori == "doviz-kur")
            {
                string cesit = segmentler[1].ToString();
                ds.ReadXml(new XmlTextReader(dovizKurXml + "?$where=dc_Para_Seo=" + cesit));
                string paraAdi = ds.Tables[0].Rows[0]["dc_Para"].ToString();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sb.Append("[");
                    sb.Append("'" + ds.Tables[0].Rows[i]["dc_Banka_Tam_Adi"].ToString() + "',");
                    sb.Append("'" + ds.Tables[0].Rows[i]["dc_Alis_Fiyati"].ToString() + "',");
                    sb.Append("'" + ds.Tables[0].Rows[i]["dc_Satis_Fiyati"].ToString() + "'");
                    sb.Append("],");
                }

                sb2.Append("{'title':'Banka'},");
                sb2.Append("{'title':'Alış Fiyatı (TL)'},");
                sb2.Append("{'title':'Satış Fiyatı (TL)'}");

                lblBaslik.Text = IlkHarfleriBuyut("Döviz Kurları - " + paraAdi);
            }
            else if (kategori == "altin")
            {
                string cesit = segmentler[1].ToString();
                string altinAdi = "";

                if (cesit == "gram-altin")
                {
                    ds.ReadXml(new XmlTextReader(gramAltinXml));
                    altinAdi = "Gram Altın";

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sb.Append("[");
                        sb.Append("'" + ds.Tables[0].Rows[i]["dc_Banka_Tam_Adi"].ToString() + "',");
                        sb.Append("'" + ds.Tables[0].Rows[i]["dc_Alis_Fiyati"].ToString() + "',");
                        sb.Append("'" + ds.Tables[0].Rows[i]["dc_Satis_Fiyati"].ToString() + "'");
                        sb.Append("],");
                    }

                    sb2.Append("{'title':'Banka'},");
                    sb2.Append("{'title':'Alış Fiyatı (TL)'},");
                    sb2.Append("{'title':'Satış Fiyatı (TL)'}");
                }
                else
                {
                    ds.ReadXml(new XmlTextReader(serbestPiyasaAltinXml + "?$where=dc_Altin_Turu_Seo=" + cesit));
                    altinAdi = ds.Tables[0].Rows[0]["dc_Altin_Turu"].ToString();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sb.Append("[");
                        sb.Append("'Serbest Piyasa',");
                        sb.Append("'" + ds.Tables[0].Rows[i]["dc_Alis_Fiyati"].ToString() + "',");
                        sb.Append("'" + ds.Tables[0].Rows[i]["dc_Satis_Fiyati"].ToString() + "'");
                        sb.Append("],");
                    }

                    sb2.Append("{'title':'Banka'},");
                    sb2.Append("{'title':'Alış Fiyatı (TL)'},");
                    sb2.Append("{'title':'Satış Fiyatı (TL)'}");
                }
                lblBaslik.Text = IlkHarfleriBuyut("Altın Çeşitleri - " + altinAdi);
            }
        }
        else if (segmentler.Count == 1)
        {
            DefaultDurumGoster("serbest-piyasa");
            return;
        }
        else
        {
            DefaultDurumGoster("serbest-piyasa");
            return;
        }

        TabloHtml = sb.ToString();
        TabloBaslikHtml = sb2.ToString();
        Page.Title = lblBaslik.Text + " - Netdata";
    }

    public void DefaultDurumGoster(string banka)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        DataSet ds = new DataSet();
        ds.ReadXml(new XmlTextReader(dovizKurXml + "?$where=dc_Banka=" + banka));
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sb.Append("[");
            sb.Append("'" + ds.Tables[0].Rows[i]["dc_Para"].ToString() + "',");
            sb.Append("'" + ds.Tables[0].Rows[i]["dc_Alis_Fiyati"].ToString() + "',");
            sb.Append("'" + ds.Tables[0].Rows[i]["dc_Satis_Fiyati"].ToString() + "'");
            sb.Append("],");
        }

        sb2.Append("{'title':'Para'},");
        sb2.Append("{'title':'Alış Fiyatı (TL)'},");
        sb2.Append("{'title':'Satış Fiyatı (TL)'}");

        TabloHtml = sb.ToString();
        TabloBaslikHtml = sb2.ToString();

        lblBaslik.Text = IlkHarfleriBuyut("Döviz Kurları - " + banka.Replace("-", " "));
        Page.Title = IlkHarfleriBuyut("Döviz Kurları - " + banka.Replace("-", " ")) + " - Netdata";
    }

    public void ParaCesitleriniYukle()
    {
        DataSet ds = new DataSet();
        StringBuilder sb = new StringBuilder();
        ds.ReadXml(new XmlTextReader(dovizKurXml));

        DataView dv2 = new DataView(ds.Tables[0]);
        DataTable dt2 = dv2.ToTable(true, new string[] { "dc_Para" });
        ddlPara1.Items.Add("Türk Lirası");
        ddlPara2.Items.Add("Türk Lirası");
        for (int i = 0; i < dt2.Rows.Count; i++)
        {
            sb.Append("<li onclick='LinkIcerikGoruntule(1,\"" + UrlSEO(dt2.Rows[i]["dc_Para"].ToString()) + "\")'>" + dt2.Rows[i]["dc_Para"].ToString() + "</li>");
            ddlPara1.Items.Add(dt2.Rows[i]["dc_Para"].ToString());
            ddlPara2.Items.Add(dt2.Rows[i]["dc_Para"].ToString());
        }
        ddlPara1.SelectedIndex = 1;
        ddlPara2.SelectedIndex = 2;


        DataSet ds2 = new DataSet();
        ds2.ReadXml(new XmlTextReader(serbestPiyasaAltinXml));
        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
        {
            string altinTuru = ds2.Tables[0].Rows[i]["dc_Altin_Turu"].ToString();
            string altinTuruSeo = ds2.Tables[0].Rows[i]["dc_Altin_Turu_Seo"].ToString();
            sb.Append("<li onclick='LinkIcerikGoruntule(2,\"" + altinTuruSeo + "\")'>" + altinTuru + "</li>");
            ddlAltin1.Items.Add(ds2.Tables[0].Rows[i]["dc_Altin_Turu"].ToString());
            ddlAltin2.Items.Add(ds2.Tables[0].Rows[i]["dc_Altin_Turu"].ToString());
        }
        ddlAltin2.SelectedIndex = 1;

        ltrSagLinkler.Text = sb.ToString();
    }

    public void PopulerKurlariGoster()
    {
        DataSet ds = new DataSet();
        ds.ReadXml(new XmlTextReader(dovizKurXml + "?$where=dc_Banka=serbest-piyasa[and]dc_Para=Amerikan Doları"));
        lblDolar.Text = ds.Tables[0].Rows[0]["dc_Satis_Fiyati"].ToString() + " TL";

        ds = new DataSet();
        ds.ReadXml(new XmlTextReader(dovizKurXml + "?$where=dc_Banka=serbest-piyasa[and]dc_Para=Euro"));
        lblEuro.Text = ds.Tables[0].Rows[0]["dc_Satis_Fiyati"].ToString() + " TL";

        ds = new DataSet();
        ds.ReadXml(new XmlTextReader(gramAltinXml + "?$where=dc_Banka=serbest-piyasa"));
        lblGramAltin.Text = ds.Tables[0].Rows[0]["dc_Satis_Fiyati"].ToString() + " TL";

    }

    [WebMethod]
    public static string AramaUrlDon(string kategori, string banka)
    {
        if (kategori == "Doviz Kurlari")
        {
            return "/doviz/doviz/" + UrlSEO(kategori) + "/" + banka;
        }
        else
        {
            return "/doviz/doviz/" + UrlSEO(kategori);
        }
    }

    [WebMethod]
    public static string DovizCeviriYap(string paraTuru, string paraMiktari, string donusturulecekParaTuru, string fiyatTipi)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(new XmlTextReader(dovizKurXml + "?$where=dc_Banka=serbest-piyasa"));
        decimal donusecekParaFiyati = 0;
        decimal _paraMiktari = Convert.ToDecimal(paraMiktari.Replace(".", ","));
        decimal istenilenParaFiyati = 0;

        if (paraTuru == "Türk Lirası")
        {
            donusecekParaFiyati = 1;
        }
        if (donusturulecekParaTuru == "Türk Lirası")
        {
            istenilenParaFiyati = 1;
        }

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["dc_Para"].ToString() == paraTuru)
            {
                if (fiyatTipi == "alis")
                    donusecekParaFiyati = Convert.ToDecimal(ds.Tables[0].Rows[i]["dc_Alis_Fiyati"].ToString());
                else
                    donusecekParaFiyati = Convert.ToDecimal(ds.Tables[0].Rows[i]["dc_Satis_Fiyati"].ToString());
            }
            else if (ds.Tables[0].Rows[i]["dc_Para"].ToString() == donusturulecekParaTuru)
            {
                if (fiyatTipi == "alis")
                    istenilenParaFiyati = Convert.ToDecimal(ds.Tables[0].Rows[i]["dc_Alis_Fiyati"].ToString());
                else
                    istenilenParaFiyati = Convert.ToDecimal(ds.Tables[0].Rows[i]["dc_Satis_Fiyati"].ToString());
            }
        }

        decimal gonderilecekDeger = (donusecekParaFiyati * _paraMiktari) / istenilenParaFiyati;
        return gonderilecekDeger.ToString("##,##0.0000");
    }

    [WebMethod]
    public static string AltinCeviriYap(string altinTuru, string altinMiktari, string donusturulecekAltinTuru, string fiyatTipi)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(new XmlTextReader(serbestPiyasaAltinXml));

        decimal donusecekAltinFiyati = 0;
        decimal _altinMiktari = Convert.ToDecimal(altinMiktari.Replace(".", ","));
        decimal istenilenAltinFiyati = 0;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["dc_Altin_Turu"].ToString() == altinTuru)
            {
                if (fiyatTipi == "alis")
                    donusecekAltinFiyati = Convert.ToDecimal(ds.Tables[0].Rows[i]["dc_Alis_Fiyati"].ToString());
                else
                    donusecekAltinFiyati = Convert.ToDecimal(ds.Tables[0].Rows[i]["dc_Satis_Fiyati"].ToString());
            }
            else if (ds.Tables[0].Rows[i]["dc_Altin_Turu"].ToString() == donusturulecekAltinTuru)
            {
                if (fiyatTipi == "alis")
                    istenilenAltinFiyati = Convert.ToDecimal(ds.Tables[0].Rows[i]["dc_Alis_Fiyati"].ToString());
                else
                    istenilenAltinFiyati = Convert.ToDecimal(ds.Tables[0].Rows[i]["dc_Satis_Fiyati"].ToString());
            }
        }

        decimal gonderilecekDeger = (donusecekAltinFiyati * _altinMiktari) / istenilenAltinFiyati;
        return gonderilecekDeger.ToString("##,##0.0000");
    }

    public static string UrlSEO(string Text)
    {
        System.Globalization.CultureInfo cui = new System.Globalization.CultureInfo("en-US");

        string strReturn = System.Net.WebUtility.HtmlDecode(Text.Trim());
        strReturn = strReturn.Replace("ğ", "g");
        strReturn = strReturn.Replace("Ğ", "g");
        strReturn = strReturn.Replace("ü", "u");
        strReturn = strReturn.Replace("Ü", "u");
        strReturn = strReturn.Replace("ş", "s");
        strReturn = strReturn.Replace("Ş", "s");
        strReturn = strReturn.Replace("ı", "i");
        strReturn = strReturn.Replace("İ", "i");
        strReturn = strReturn.Replace("ö", "o");
        strReturn = strReturn.Replace("Ö", "o");
        strReturn = strReturn.Replace("ç", "c");
        strReturn = strReturn.Replace("Ç", "c");
        strReturn = strReturn.Replace(" - ", "+");
        strReturn = strReturn.Replace("-", "+");
        strReturn = strReturn.Replace(" ", "+");
        strReturn = strReturn.Trim();
        strReturn = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9+]").Replace(strReturn, "");
        strReturn = strReturn.Trim();
        strReturn = strReturn.Replace("+", "-");
        return strReturn.ToLower(cui);
    }

    public string IlkHarfleriBuyut(string metin)
    {
        System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
        return textInfo.ToTitleCase(metin);
    }
}