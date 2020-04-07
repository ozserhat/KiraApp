function UyariVer(Baslik, Icerik, IslemSonucu, gozukmeSuresi) {
    if (IslemSonucu) {

        $.smallBox({
            title: Baslik,
            //content: "<i class='fa fa-clock-o'></i> <i>You pressed Yes...</i>",
            content: Icerik,
            color: "#659265",
            iconSmall: "fa fa-check fa-2x fadeInRight animated",
            timeout: gozukmeSuresi
        });
    }
    else {
        $.smallBox({
            title: Baslik,
            content: Icerik,
            color: "#C46A69",
            iconSmall: "fa fa-times fa-2x fadeInRight animated",
            timeout: gozukmeSuresi
        });
    }
};