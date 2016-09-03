$(document).ready(function () {
    $("#btnDonusumTabloDetay").click(function () {
        var panelBody = $("#divDonusumBody");
        if (panelBody.is(':visible')) {
            panelBody.fadeOut();
            $("#btnDonusumTabloDetay").removeClass("btn-danger");
            $("#btnDonusumTabloDetay").addClass("btn-default");
            $(this).find(".glyphicon").removeClass("glyphicon-minus");
            $(this).find(".glyphicon").addClass("glyphicon-plus");
        }
        else {
            panelBody.fadeIn();
            $("#btnDonusumTabloDetay").removeClass("btn-default");
            $("#btnDonusumTabloDetay").addClass("btn-danger");
            $(this).find(".glyphicon").removeClass("glyphicon-plus");
            $(this).find(".glyphicon").addClass("glyphicon-minus");
        }
    });
});

function LinkIcerikGoruntule(kategoriTip, cesit) {
    $(".loaderStore").show();
    $('html, body').animate({
        scrollTop: $(".loaderStore").offset().top
    }, 0);

    debugger;
    var kategoriSeo = "";
    switch (kategoriTip) {
        case 1: kategoriSeo = "doviz-kur";
            break;
        case 2: kategoriSeo = "altin";
            break;
        default:
            break;
    }

    window.location.href = "/doviz/doviz/" + kategoriSeo + "/" + cesit;
}

function SadeceSayi(evt) {
    evt = (evt) ? evt : window.event
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if ((charCode > 47 && charCode < 58) || charCode == 8 || charCode == 9 || charCode == 127 || charCode == 44) {
        var para1 = $("#txtPara1").val()
        if (para1.split('.').length < 3) {
            return true;
        }
    }
    return false;
}

function DovizCeviriYap(evt) {
    var paraTuru = $("#ddlPara1").val();
    var paraMiktari = $("#txtPara1").val().trim();
    var donusturulecekParaTuru = $("#ddlPara2").val();
    var fiyatTipi = $("[name='dovizRbtn']").val();
    if (paraMiktari=="") {
        bootbox.alert("Lütfen değer giriniz!");
        return;
    }
    if (paraTuru == donusturulecekParaTuru) {
        bootbox.alert("Dönüştürülecek para tipleri aynı olamaz!");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/doviz/doviz.aspx/DovizCeviriYap",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{paraTuru:" + JSON.stringify(paraTuru) + ", paraMiktari:" + JSON.stringify(paraMiktari) + ", donusturulecekParaTuru:" + JSON.stringify(donusturulecekParaTuru) + ", fiyatTipi:" + JSON.stringify(fiyatTipi) + "}",
        async: true,
        success: function (result) {
            $("#lblDovizDonusmusSonuc").html(result.d);
        },
        error: function (xhr, status, error) {
            bootbox.alert(error);
        }
    });
}

function AltinCeviriYap(evt) {
    var altinTuru = $("#ddlAltin1").val();
    var altinMiktari = $("#txtAltin1").val().trim();
    var donusturulecekAltinTuru = $("#ddlAltin2").val();
    var fiyatTipi = $("[name='altinRbtn']").val();
    if (altinMiktari == "") {
        bootbox.alert("Lütfen değer giriniz!");
        return;
    }
    if (altinTuru == donusturulecekAltinTuru) {
        bootbox.alert("Dönüştürülecek altın tipleri aynı olamaz!");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/doviz/doviz.aspx/AltinCeviriYap",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{altinTuru:" + JSON.stringify(altinTuru) + ", altinMiktari:" + JSON.stringify(altinMiktari) + ", donusturulecekAltinTuru:" + JSON.stringify(donusturulecekAltinTuru) + ", fiyatTipi:" + JSON.stringify(fiyatTipi) + "}",
        async: true,
        success: function (result) {
            $("#lblAltinDonusmusSonuc").html(result.d);
        },
        error: function (xhr, status, error) {
            bootbox.alert(error);
        }
    });
}


//function KategoriDegistir() {
//    var kategori = $("#ddlKategori").val();
//    if (kategori == "0") {
//        $("#divBankaCesitleri").show();
//    }
//    else {
//        $("#divBankaCesitleri").hide();
//    }
//}
