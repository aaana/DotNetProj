var oriShowCommentListBtn = null;

var showCommentDiv = function (t, oriBtn) {
    oriShowCommentListBtn = oriBtn

    var weikeId = $(t).parent().attr('id');
    $.ajax({
        type: "post",
        url: "../CommentAction/getCommentList",
        data: {
            "weikeId":weikeId
        },
        dataType: "json",
        success: function (data) {
            console.log("get comment success");
            var commentList = data.comments;

            setCommentDiv(commentList, $(t));
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("error:" + XMLHttpRequest.status + " " + XMLHttpRequest.readyState + " " + textStatus);
        }
    });
}

var setCommentDiv = function (commentList, target) {
    target.parent().parent().append(
        '<div class="personalPageContentItemComment">' +
            '<div></div>' +
            '<div class="weikeCellComment input-group">' +
                '<input type="text" class="form-control" placeholder="我来评论">' +
                '<span class="input-group-btn">' +
                    '<button class="btn btn-default" type="button" onclick="makeComment2weike(this)">评论</button>' +
                '</span>' +
            '</div>' +

            '<ul class="media-list weikeCellCommentList">' +
            '</ul>' +
        '</div>');

    var commentListDivNode = target.parent().next().children('.weikeCellCommentList');
    initCommentDiv(commentList, commentListDivNode);

    commentListDivNode.append('<a onclick="hideCommentListDiv(this)"><span class="glyphicon glyphicon-chevron-up"></span></a>');
}

var hideCommentListDiv = function (t) {
    
    if (oriShowCommentListBtn != null) {
        $(oriShowCommentListBtn).show();
        oriShowCommentListBtn = null;
    } else {
        
        var top = document.body.scrollTop - $(t).parent().parent().height();
        $(document.body).animate({ scrollTop: top }, 300);
    }
    $(t).parent().parent().remove();
}

var initCommentTemplate = function (ncomment) {
    var date = new Date( parseInt(ncomment.commentData.comment.date.substr( 6 ) ) );
    Y = date.getFullYear() + '-';
    M = ( date.getMonth() + 1 < 10 ? '0' + ( date.getMonth() + 1) : date.getMonth() + 1) + '-';
    D = date.getDate() + ' ';
    var rDate = ( Y + M + D );
    return '<li class="media" id="' + ncomment.commentData.comment.comment_id + '">' +
                '<div class="media-left">' +
                '<a href="#">' +
                    '<img class="media-object" src="' + '../resource/img/portrait.jpg' + '">' +
                '</a>' +
                '</div>' +
                '<div class="media-body">' +
                    '<h5 class="media-heading">' + ncomment.commentData.commenter + ' <small>' + rDate + '</small></h5>' +
                    '<h6>' + ncomment.commentData.comment.content + '</h6>' +
                    '<div class="weikeCellCommentReply">' +
                        '<a onclick="showCommentInput(this)">回复</a>' +
                    '</div>' +
                '</div>' +
            '</li>'
}

var initCommentDiv = function (commentList, parentNode) {
    for (var index in commentList) {
        parentNode.append(initCommentTemplate(commentList[index]));

        if (commentList[index].nestedComments.length > 0) {
            initCommentDiv(commentList[index].nestedComments, parentNode.children('#' + commentList[index].commentData.comment.comment_id + ':last-child').children('.media-body'));
        }
    }
}



var makeComment2weike = function (t) {
    var content = $(t).parent().prev().val();
    var commentTargetId = 0;
    var weikeId = $(t).parents('.personalPageContentItemComment').prev().attr('id');
    var info = {
        "commentTargetId": commentTargetId,
        "content": content,
        "weikeId": weikeId
    };
    var success = function (data) {
        if (data.success == -1) {
            alert("不能对自己回复！");
        } else if (data.success == 0) {
            alert("请先登录！");
            location = "../Auth?type=0";
        } else {
            console.log(data);
            var time = new Date();
            var commentInfo = {
                'comment': {
                    'commentData':{
                        'comment':{
                            comment_id:data.commentId,
                            date:data.now,
                            content:content
                        },
                        'commenter':data.username
                    },
                   // 'user': {
                   //     'name': data.username,
                   //     'imgSrc': '../resource/img/8.jpg'
                   // },
                   // 'time': time.getFullYear() + '-' + (time.getMonth() + 1) + '-' + time.getDate() + ' ' + time.getHours() + ':' + time.getMinutes(),
                   // 'content': content,
                    'commentList': []
                }
            }
        }
     

        var comment = info.comment;
        $(t).parent().prev().val('');
        $(t).parents('.weikeCellComment').next().children('a').before(initCommentTemplate(comment));
    }

    sendComment2Controller(info, success, function (error) { console.log(error) });
}

var sendComment2Controller = function (data, success, error) {
    $.ajax({
        type: "post",
        url: "../CommentAction/makeComment",
        data: data,
        dataType: "json",
        success: success,
        error: error
    });
}

var showCommentInput = function (t) {
    $(t).parent().after(
        '<div class="weikeCellComment input-group">' +
            '<input type="text" class="form-control" placeholder="我来评论">' +
            '<span class="input-group-btn">' +
                '<button class="btn btn-default" type="button" onclick="makeComment2comment(this)">评论</button>' +
                '<button class="btn btn-default" type="button" onclick="hideComment2comment(this)">取消</button>' +
            '</span>' +
        '</div>');
    $(t).parent().hide();
}

var makeComment2comment = function (t) {
    var commentTargetId = $($(t).parents('.media')[0]).attr('id');
    var content = $(t).parent().prev().val();
    var weikeId = $(t).parents('.personalPageContentItemComment').prev().attr('id');
    var info = {
        "commentTargetId": commentTargetId,
        "content": content,
        "weikeId":weikeId
    };
    var success = function (data) {
        if (data.success == -1) {
            alert("不能对自己回复！");
        } else if (data.success == 0) {
            alert("请先登录！");
            location = "../Auth?type=0";
        } else {
            console.log(data);
            var time = new Date();
            var commentInfo = {
                'comment': {
                    'commentData': {
                        'comment': {
                            comment_id: data.commentId,
                            date: data.now,
                            content: content
                        },
                        'commenter': data.username
                    },
                    // 'user': {
                    //     'name': data.username,
                    //     'imgSrc': '../resource/img/8.jpg'
                    // },
                    // 'time': time.getFullYear() + '-' + (time.getMonth() + 1) + '-' + time.getDate() + ' ' + time.getHours() + ':' + time.getMinutes(),
                    // 'content': content,
                    'commentList': []
                }
            }
        }


        var comment = commentInfo.comment;
        $(t).parent().prev().val('');
        $($(t).parents('.media-body')[0]).append(initCommentTemplate(comment));
        hideComment2comment(t);
    }
    sendComment2Controller(info, success, function (error) { console.log(error) });
}

var hideComment2comment = function (t) {
    $(t).parents('.weikeCellComment').prev().show();
    $(t).parents('.weikeCellComment').remove();
}

var likeWeike = function (t) {
    var weikeId = $(t).parents('.weikeId').attr('id');

    $.ajax({
        type: "post",
        url: "../LikeAction/Like",
        data: {
            "weikeId": weikeId
        },
        dataType: "json",
        success: function (data) {
            if (data.success) {
                console.log(data);
                var count = parseInt($(t).children('span').text()) + 1;
                $(t).children('span').text(parseInt($(t).children('span').text()) + 1);
                $(t).html("已赞 <span>" + count + "</span>");
                $('.weikeCellVote  > span:last-child').each(function () {
                    if ($(this).parents('.weikeCell').attr('id') == weikeId) {
                        $(this).html('<span class="glyphicon glyphicon-heart"></span>' + count);
                        $(this).parents('.grid__item').children('.description').children('.weikeCellDetail').children('.weikeId')
                            .children('a').html('已赞  <span>' + count + '</span>');

                    }
                })
                $(t).css('color', 'white');
                $(t).attr('onclick', 'dislikeWeike(this)');
            } else {
                alert("请先登录！");
                location: "../Auth/Index/0"
            }
           
        },
        error: function () {

        }
    });
}

var dislikeWeike = function (t) {
    var weikeId = $(t).parents('.weikeId').attr('id');

    $.ajax({
        type: "post",
        url: "../LikeAction/Dislike",
        data: {
            "weikeId": weikeId
        },
        dataType: "json",
        success: function (data) {
            console.log(data);

            var count = parseInt($(t).children('span').text()) - 1;
            $(t).html("点赞 <span>" + count + "</span>");
            $('.weikeCellVote  > span:last-child').each(function () {
                if ($(this).parents('.weikeCell').attr('id') == weikeId) {
                    $(this).html('<span class="glyphicon glyphicon-heart"></span>' + count);
                    $(this).parents('.grid__item').children('.description').children('.weikeCellDetail').children('.weikeId')
                        .children('a').html('点赞  <span>' + count + '</span>');
                }
            })
            $(t).css('color', '#cccccc');
            $(t).attr('onclick', 'likeWeike(this)');
        },
        error: function () {

        }
    });
}

var follow = function (t) {
    var userId = $(t).parent().attr('id');
    $.ajax({
        type: "post",
        url: "../FollowAction/Follow",
        data: {
            "following_id": userId
        },
        dataType: "json",
        success: function (data) {
            console.log(data);

            if ($(t).hasClass('navFollowBtn')) {
                $('.navFollowBtn').text('已关注');
                $('.navFollowBtn').attr('onclick', 'unfollow(this)');
            } else {
                $(t).text('已关注');
                $(t).attr('onclick', 'unfollow(this)');
            }
        },
        error: function () {

        }
    });
}

var unfollow = function (t) {
    var userId = $(t).parent().attr('id');

    $.ajax({
        type: "post",
        url: "../FollowAction/UnFollow",
        data: {
            "following_id": userId
        },
        dataType: "json",
        success: function (data) {
            console.log(data);
            if ($(t).hasClass('navFollowBtn')) {
                $('.navFollowBtn').text('+ 关注');
                $('.navFollowBtn').attr('onclick', 'follow(this)');
            } else {
                $(t).text('+ 关注');
                $(t).attr('onclick', 'follow(this)');
            }
        },
        error: function () {

        }
    });
}

var gotoPersonalPage = function (userId) {
    var url = "../PersonalPage/PersonalPageWeike?userId=" + userId;
    window.location.href = url;
}

var pressUploadFileInput = function () {
    $('#changeAvatarInput').click();
}

$(function () {
    $("#changeAvatarInput").change(function () {
        if (typeof (FileReader) != "undefined") {
            var dvPreview = $("#img-preview");
            var regex = /(.jpg|.jpeg|.gif|.png|.bmp)$/;
            dvPreview.html('<img class="upload-img"  src="@ViewBag.personalInfo["portraitSrc"]" />');
            $($(this)[0].files).each(function () {
                var file = $(this);
                if (regex.test(file[0].name.toLowerCase())) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var img = $("<img />");
                        img.attr("src", e.target.result);
                        img.attr("class", "img-responsive img-circle")
                        dvPreview.html("");
                        dvPreview.append(img);
                    };
                    var src = reader.readAsDataURL(file[0]);
                } else {
                    alert(file[0].name + " is not a valid image file.");
                    dvPreview.html("");
                    return false;
                }
            });
        } else {
            alert("This browser does not support HTML5 FileReader.");
        }
    });
}); 