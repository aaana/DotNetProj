
function pblSupport() {
    var support = { transitions: Modernizr.csstransitions },
    // transition end event name
        transEndEventNames = { 'WebkitTransition': 'webkitTransitionEnd', 'MozTransition': 'transitionend', 'OTransition': 'oTransitionEnd', 'msTransition': 'MSTransitionEnd', 'transition': 'transitionend' },
        transEndEventName = transEndEventNames[ Modernizr.prefixed( 'transition' ) ],
        onEndTransition = function( el, callback ) {
            var onEndCallbackFn = function( ev ) {
                if( support.transitions ) {
                    if( ev.target != this ) return;
                    this.removeEventListener( transEndEventName, onEndCallbackFn );
                }
                if( callback && typeof callback === 'function' ) { callback.call(this); }
            };
            if( support.transitions ) {
                el.addEventListener( transEndEventName, onEndCallbackFn );
            }
            else {
                onEndCallbackFn();
            }
        };

    new GridFx(document.querySelector('.grid'), {
        imgPosition : {
            x : -0.5,
            y : 1
        },
        onOpenItem : function(instance, item) {
            instance.items.forEach(function(el) {
                if(item != el) {
                    var delay = Math.floor(Math.random() * 50);
                    el.style.WebkitTransition = 'opacity .5s ' + delay + 'ms cubic-bezier(.7,0,.3,1), -webkit-transform .5s ' + delay + 'ms cubic-bezier(.7,0,.3,1)';
                    el.style.transition = 'opacity .5s ' + delay + 'ms cubic-bezier(.7,0,.3,1), transform .5s ' + delay + 'ms cubic-bezier(.7,0,.3,1)';
                    el.style.WebkitTransform = 'scale3d(0.1,0.1,1)';
                    el.style.transform = 'scale3d(0.1,0.1,1)';
                    el.style.opacity = 0;
                }
            });
        },
        onCloseItem : function(instance, item) {
            instance.items.forEach(function(el) {
                if(item != el) {
                    el.style.WebkitTransition = 'opacity .4s, -webkit-transform .4s';
                    el.style.transition = 'opacity .4s, transform .4s';
                    el.style.WebkitTransform = 'scale3d(1,1,1)';
                    el.style.transform = 'scale3d(1,1,1)';
                    el.style.opacity = 1;

                    onEndTransition(el, function() {
                        el.style.transition = 'none';
                        el.style.WebkitTransform = 'none';
                    });
                }
            });
        }
    }); // grid fx

    $('.grid__item').click(function() {
        document.documentElement.style.overflow='hidden';
        document.body.style.overflow='hidden';

        $('.footerAddcom').animate({bottom:'-=100px'});
    });
    $('.action--close').click(function() {
        document.documentElement.style.overflow='';
        document.body.style.overflow='';

        $('.footerAddcom').animate({bottom:'+=100px'});
    });
}

(function() {


    $('.hotWeike').click(function() {
        $(this).addClass('active').siblings().removeClass('active');
        $('.content').remove();
        $('.mainContent .row').append('<div class="col-md-10 hotWeike_c content">' +
            '<div class="jumbotron">' +
                '<h1>微课</h1>' +
                '<p>在这里，有老师们精心挑选的资料；</p>' +
                '<p>在这里，有大家共同进步的快乐。</p>' +
            '</div>' +
            '<div class="js">' +
                '<div class="grid">' +
                '</div>' +
                '<div class="preview">' +
                    '<button class="action action--close"><h1>×</h1></button>' +
                    '<div class="description description--preview"></div>' +
                '</div><!-- /preview -->' +
            '</div>' +
        '</div>');


        var json = {};
        $.ajax({
            type:"post",
            url:"/home/index",
            data:json,
            dataType:"json",
            success:function(data){
                for (var item in data) {
                    $('.grid').append('<div class="grid__item" data-size=' + data[item].size + '>' +
                        '<div class="weikeCell"> ' +
                            '<h3>' + data[item].title + '</h3> ' +
                            '<h6>' + data[item].subject + '</h6> ' +
                            '<div class="thumbnail"> ' +
                            '<a href=' + data[item].src + ' class="img-wrap"><img src=' + data[item].src + '></a> ' +
                            '</div> ' +
                            '<p><a class="btn" href="#" role="button">star</a></p> ' +
                            '</div> ' +
                            '<div class="description description--grid"> ' +
                            '<h3>' + data[item].title + '</h3> ' +
                            '<p>' + data[item].description + ' <em>&mdash; ' + data[item].author + '</em></p> ' +
                            '<div class="details"> ' +
                            '<ul> ' +
                            '<li><i class="icon icon-camera"></i><span>' + data[item].star + '</span></li> ' +
                            '<li><i class="icon icon-focal_length"></i><span>22.5mm</span></li> ' +
                            '</ul> ' +
                            '</div> ' +
                        '</div> ' +
                    '</div>');
                }
                pblSupport();
            },
            error:function(XMLHttpRequest, textStatus, errorThrown) {
                alert("error:" + XMLHttpRequest.status + " " + XMLHttpRequest.readyState + " " + textStatus);
            }
        });

       
    });
    $('.playground').click(function() {
        $(this).addClass('active').siblings().removeClass('active');
        $('.content').remove();
        $('.mainContent .row').append('<div class="col-md-10 playground_c content">' +
        '<div class="js">' +
        '<div class="grid">' +
        '</div>' +
        '<div class="preview">' +
        '<button class="action action--close"><h1>×</h1></button>' +
        '<div class="description description--preview"></div>' +
        '</div><!-- /preview -->' +
        '</div>' +
        '</div>');

        var json = {};
        $.ajax({
            type:"post",
            url:"home/playground",
            data:json,
            dataType:"json",
            success: function (data) {
                for (var item in data) {
                $('.grid').append('<div class="grid__item" data-size=' + data[item].size + '>' +
                '<div class="weikeCell"> ' +
                '<h3>' + data[item].title + '</h3> ' +
                '<h6>' + data[item].subject + '</h6> ' +
                '<div class="thumbnail"> ' +
                '<a href=' + data[item].src + ' class="img-wrap"><img src=' + data[item].src + '></a> ' +
                '</div> ' +
                '<p><a class="btn" href="#" role="button">star</a></p> ' +
                '</div> ' +
                '<div class="description description--grid"> ' +
                '<h3>' + data[item].title + '</h3> ' +
                '<p>' + data[item].description + ' <em>&mdash; ' + data[item].author + '</em></p> ' +
                '<div class="details"> ' +
                '<ul> ' +
                '<li><i class="icon icon-camera"></i><span>' + data[item].star + '</span></li> ' +
                '<li><i class="icon icon-focal_length"></i><span>22.5mm</span></li> ' +
                '</ul> ' +
                '</div> ' +
                '</div> ' +
                '</div>');
            }
            pblSupport();
                
            },
            error:function(XMLHttpRequest, textStatus, errorThrown) {
                alert("error:" + XMLHttpRequest.status + " " + XMLHttpRequest.readyState + " " + textStatus);
            }
        });

     
    });
    $('.myFollow').click(function() {
        $(this).addClass('active').siblings().removeClass('active');
        $('.content').remove();
        $('.mainContent .row').append('<div class="col-md-10 myFollow_c content">' +
            '<div class="jumbotron">' +
            '<h1>关注</h1>' +
            '<p>关注的人</p>' +
            '</div>' +
        '</div>');

    });
    $('.myStar').click(function() {
        $(this).addClass('active').siblings().removeClass('active');
        $('.content').remove();
        $('.mainContent .row').append('<div class="col-md-10 myStar_c content">' +
        '<div class="jumbotron">' +
        '<h1>收藏</h1>' +
        '<p>收藏的微课</p>' +
        '</div>' +
        '</div>');
    });
    $('.myComment').click(function() {
        $(this).addClass('active').siblings().removeClass('active');
        $('.content').remove();
        $('.mainContent .row').append('<div class="col-md-10 myFollow_c content">' +
        '<div class="jumbotron">' +
        '<h1>评论</h1>' +
        '<p>评论列表</p>' +
        '<p>消息提醒</p>' +
        '</div>' +
        '</div>');
    });
    $('.hotWeike').click();

    $('.addButton').click(function() {
        document.documentElement.style.overflow='hidden';
        document.body.style.overflow='hidden';

        $('.footerAddcom').animate({bottom:'-=100px'});
        $('body').append('<div class="hidebg"></div>');
        $('.hidebg').css('height', document.body.scrollHeight+"px");
        $('.hidebg').fadeIn('slow');
        $('.addWindow').fadeIn('slow');
        $('.hidebg').click(function() {
            $('.closeButton').click();
        });
    });
    $('.closeButton').click(function() {
        $('.addWindow').fadeOut('fast');
        $('.hidebg').remove();
        $('.footerAddcom').animate({bottom:'+=100px'});
        document.documentElement.style.overflow='';
        document.body.style.overflow='';
    });


    $('#uploadFile')[0].onchange = function(event){
        var selectedFile = event.target.files[0];
        var reader = new FileReader();

        reader.onload = function(e){
            var img = new Image();
            img.onload = function(){
                var scale = 100/img.height;
                $('.imgPreview').css('height', 100);
                $('.imgPreview').css('width', img.width*scale);
                $('.imgPreview').attr('size', img.width + 'x' + img.height);
                $('.imgPreview')[0].src = e.target.result;
            };
            img.src = e.target.result;
        };
        reader.readAsDataURL(selectedFile);
        return false;
    };

    //$('#uploadFile')[0].onchange = function(node) {
    //    var imgURL = "";
    //    try{
    //        var file = null;
    //        if(node.files && node.files[0] ){
    //            file = node.files[0];
    //        }else if(node.files && node.files.item(0)) {
    //            file = node.files.item(0);
    //        }else if(node.target.files && node.target.files[0]) {
    //            file = node.target.files[0];
    //        }
    //        //Firefox 因安全性问题已无法直接通过input[file].value 获取完整的文件路径
    //        try{
    //            //Firefox7.0
    //            imgURL =  file.getAsDataURL();
    //            //alert("//Firefox7.0"+imgRUL);
    //        }catch(e){
    //            //Firefox8.0以上
    //            imgRUL = window.URL.createObjectURL(file);
    //            //alert("//Firefox8.0以上"+imgRUL);
    //        }
    //    }catch(e){      //这里不知道怎么处理了，如果是遨游的话会报这个异常
    //        //支持html5的浏览器,比如高版本的firefox、chrome、ie10
    //        if (node.files && node.files[0]) {
    //            var reader = new FileReader();
    //            reader.onload = function (e) {
    //                imgURL = e.target.result;
    //            };
    //            reader.readAsDataURL(node.files[0]);
    //        }
    //    }
    //    //imgurl = imgURL;
    //    //creatImg(imgRUL);
    //    return imgURL;
    //};
    function getImgURLIE(node) {
        var imgURL = "";
        var file = null;
        if(node.files && node.files[0] ){
            file = node.files[0];
        }else if(node.files && node.files.item(0)) {
            file = node.files.item(0);
        }

        //这种获取方式支持IE10
        node.select();
        imgURL = document.selection.createRange().text;
        alert(imgURL);


        var textHtml = "<img src='"+imgURL+"'/>";     //创建img标签用于显示图片
        alert(textHtml);
        $(".mark").after(textHtml);
        return imgURL;
    }

    function creatImg(imgRUL){   //根据指定URL创建一个Img对象
        var textHtml = "<img src='"+imgRUL+"'/>";
        $(".mark").after(textHtml);
    }

    $('.uplaodFile_f').click(function() {
        $('#uploadFile').click();
        return false;
    });
    $('.uploadButton').click(function() {
        var json = {
            title: $('#title').val(),
            subject: $('#subject').val(),
            des: $('#des').val(),
            size: $('.imgPreview').attr('size'),
            src: document.getElementById('uploadFile').value
        };
        $.ajax({
            type:"post",
            url:"insert",
            data:json,
            dataType:"json",
            success:function(info){

            },
            error:function(XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.status + " " + XMLHttpRequest.readyState + " " + textStatus);
                return false;
            }
        });
        $('.closeButton').click();
    });

    $('#search-bd li').click(function(){
        $(this).addClass('selected').siblings().removeClass('selected');
    });
    $('.btn-search').click(function() {
        var id = $('.search-bd .selected').attr("id").substr(7);
        var json = {
            type: id,
            key: $('.search-input').val()
        };
        $.ajax({
            type:"post",
            url:"",
            data:json,
            dataType:"json",
            success:function(data){
                if(data.success == 'true') {
                    alert('data');
                }
                else {

                }
            },
            error:function(){
                $('.search-input').val("");
                $('.playground').click();
            }
        });

    });

})();
