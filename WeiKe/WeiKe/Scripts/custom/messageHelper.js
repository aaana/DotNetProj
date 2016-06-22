var gotoPersonalPage = function (userId) {
    var notice_id = $(this).attr('id');
    // setRead(this, notice_id);

    var url = "../PersonalPage/PersonalPageWeike?userId=" + userId;
    window.location.href = url;
}

var showModal = function (t)
{
    var noticeId = $(t).children('.media-left').attr('id');
    var weikeId = $(t).attr('id');
    console.log(weikeId);

    $.ajax({
        type: "post",
        url: "../CommentAction/WeikeDetailWithComment",
        data: {
            "weikeId": weikeId
        },
        dataType: "json",
        success: function (data) {
            console.log(data);

            setWeikeDetail(data.weikeData, data.hasFavorited);
            setCommentDiv(data.comments);
            setRead(t, noticeId);
        },
        error: function () {
        }
    });
};

var setRead = function (t, notice_id) {
    $('#myModal').modal('show');

    $.ajax({
        type: "post",
        url: "../MessageAction/ReadNotice",
        data: {
            "notice_id": notice_id
        },
        dataType: "json",
        success: function (data) {
            console.log(data);

            if ($(t).hasClass('btn-primary')) {
                $(t).text('已读');
                $(t).removeClass('btn-primary');
            }
            if ($(t).children().children('button').hasClass('btn-primary')) {
                $(t).children().children('button').text('已读');
                $(t).children().children('button').removeClass('btn-primary');
            }
        },
        error: function () {
        }
    });
}

var setWeikeDetail = function (weikeDetail, hasFavorited) {
    var date = new Date(parseInt(weikeDetail.weike.postdate.substr(6)));
    Y = date.getFullYear() + '-';
    M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';
    D = date.getDate() + ' ';
    var rDate = (Y + M + D);
    $('#myModal .weikeId').attr('id', weikeDetail.weike.weike_id);
    $('#myModal .weikeTitle').text(weikeDetail.weike.title);
    $('#myModal .weikeDateAndSubject').text(rDate + ' ' + weikeDetail.weike.subject);
    $('#myModal .weikeAuthorName').text(weikeDetail.author);
    $('#myModal .weikeDescription').text(weikeDetail.weike.description);
    $('#myModal .weikeImg')[0].src = "../" + weikeDetail.weike.src;
    $('#myModal .weikeCommentCount').text(weikeDetail.weike.commentNum);
    $('#myModal .weikeLikeCount').text(weikeDetail.weike.star);
    if (hasFavorited) {
        $('#myModal .weikeLikeCount').prev().text("已赞");
        $('#myModal .weikeLikeCount').parent().css('color', 'white');
        $('#myModal .weikeLikeCount').parent().attr('onclick', 'dislikeWeike(this)');
    } else {
        $('#myModal .weikeLikeCount').prev().text("点赞");
        $('#myModal .weikeLikeCount').parent().css('color', '#ccc');
        $('#myModal .weikeLikeCount').parent().attr('onclick', 'likeWeike(this)');
    }

}

var setCommentDiv = function(commentList) {
    $('.weikeDetail .modalWeikeItemComment').remove();
    $('.weikeDetail').append(
        '<div class="modalWeikeItemComment">' +
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

    var commentListDivNode = $('.weikeDetail').children('.modalWeikeItemComment').children('.weikeCellCommentList');
    initCommentDiv(commentList, commentListDivNode);
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
    var date = new Date(parseInt(ncomment.commentData.comment.date.substr(6)));
    Y = date.getFullYear() + '-';
    M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';
    D = date.getDate() + ' ';
    var rDate = (Y + M + D);
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
    var weikeId = $(t).parents('.weikeDetail.weikeId').attr('id');
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
                    'commentData': {
                        'comment': {
                            comment_id: data.commentId,
                            date: data.time,
                            content: content
                        },
                        'commenter': data.username
                    },
                    'commentList': []
                }
            };
            var comment = commentInfo.comment;
            $(t).parent().prev().val('');
            $(t).parents('.weikeCellComment').next().append(initCommentTemplate(comment));
        }
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
    var weikeId = $(t).parents('.weikeDetail.weikeId').attr('id');
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
                    'commentData': {
                        'comment': {
                            comment_id: data.commentId,
                            date: data.time,
                            content: content
                        },
                        'commenter': data.username
                    },
                    'commentList': []
                }
            };
            var comment = commentInfo.comment;
            $(t).parent().prev().val('');
            $($(t).parents('.media-body')[0]).append(initCommentTemplate(comment));
            hideComment2comment(t);
        }
    }
    sendComment2Controller(info, success, function (error) { console.log(error) });
}

var hideComment2comment = function (t) {
    $(t).parents('.weikeCellComment').prev().show();
    $(t).parents('.weikeCellComment').remove();
}

var likeWeike = function (t) {
    var weikeId = $(t).parents('.weikeDetail.weikeId').attr('id');

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
                var count = parseInt($('.weikeLikeCount').text()) + 1;
                $(t).html("<span>已赞</span> <span>" + count + "</span>");

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
    var weikeId = $(t).parents('.weikeDetail.weikeId').attr('id');

    $.ajax({
        type: "post",
        url: "../LikeAction/Dislike",
        data: {
            "weikeId": weikeId
        },
        dataType: "json",
        success: function (data) {
            console.log(data);
            var count = parseInt($('.weikeLikeCount').text()) - 1;
            $(t).html("<span>点赞<span> <span>" + count + "</span>");
            $(t).css('color', '#cccccc');
            $(t).attr('onclick', 'likeWeike(this)');
        },
        error: function () {

        }
    });
}


