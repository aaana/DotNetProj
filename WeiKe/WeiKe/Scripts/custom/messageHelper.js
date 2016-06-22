var gotoPersonalPage = function (userId) {
    var url = "../PersonalPage/PersonalPageWeike?userId=" + userId;
    window.location.href = url;
}

var showModal = function (t) {
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
            setRead(t);
        },
        error: function () {
        }
    });
}

var setRead = function (t) {
    $('#myModal').modal('show');
    if ($(t).hasClass('btn-primary')) {
        $(t).text('已读');
        $(t).removeClass('btn-primary');
    }
    if ($(t).children().children('button').hasClass('btn-primary')) {
        $(t).children().children('button').text('已读');
        $(t).children().children('button').removeClass('btn-primary');
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
    $('#myModal .weikeImg')[0].src = "../" +weikeDetail.weike.src;
    $('#myModal .weikeCommentCount').text(weikeDetail.weike.commentNum);
    $('#myModal .weikeLikeCount').text(weikeDetail.weike.star);
    if (hasFavorited) {
        $('#myModal .weikeLikeCount').prev().text("点赞");
        $('#myModal .weikeLikeCount').parent().css('color', '#ccc');
        $('#myModal .weikeLikeCount').parent().attr('onclick', 'dislikeWeike(this)');
    } else {
        $('#myModal .weikeLikeCount').prev().text("已赞");
        $('#myModal .weikeLikeCount').parent().css('color', 'white');
        $('#myModal .weikeLikeCount').parent().attr('onclick', 'likeWeike(this)');
    }

}

var initCommentDiv = function (commentList, parentNode) {
    for (var index in commentList) {
        parentNode.append(initCommentTemplate(commentList[index]));

        if (commentList[index].commentList.length > 0) {
            initCommentDiv(commentList[index].commentList, parentNode.children('#' + commentList[index].id + ':last-child').children('.media-body'));
        }
        console.log(index + ' ' + commentList[index].user.name + ' ' + commentList[index].commentList.length);
    }
}

var initCommentTemplate = function (comment) {
    return '<li class="media" id="' + comment.id + '">' +
                '<div class="media-left">' +
                '<a href="#">' +
                    '<img class="media-object" src="' + comment.user.imgSrc + '">' +
                '</a>' +
                '</div>' +
                '<div class="media-body">' +
                    '<h5 class="media-heading">' + comment.user.name + ' <small>' + comment.time + '</small></h5>' +
                    '<h6>' + comment.content + '</h6>' +
                    '<div class="weikeCellCommentReply">' +
                        '<a onclick="showCommentInput(this)">回复</a>' +
                    '</div>' +
                '</div>' +
            '</li>';
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
                $(t).html("<span>已赞</span> <span>" + count + "</span>");

                $(t).css('color', 'white');
                $(t).attr('onclick', 'dislikeWeike(this)');
            } else {
                alert("请先登录！");
                location: "../Auth/Index/0"
            }

        },
        error: function () {
            console.log('error');
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
                        .children('a').html('<span>点赞</span>  <span>' + count + '</span>');
                }
            })
            $(t).css('color', '#cccccc');
            $(t).attr('onclick', 'likeWeike(this)');
        },
        error: function () {
            console.log("error");
        }
    });
}

var hideCommentListDiv = function (t) {
    $(t).parent().parent().remove();
}

var makeComment2weike = function (t) {

    var content = $(t).parent().prev().val();

    // post comment
    // if success, return the new comment
    var time = new Date();
    var data = {
        'comment': {
            'id': 103,
            'user': {
                'name': '用户名3',
                'imgSrc': 'resource/img/8.jpg'
            },
            'time': time.getFullYear() + '-' + (time.getMonth() + 1) + '-' + time.getDate() + ' ' + time.getHours() + ':' + time.getMinutes(),
            'content': content,
            'commentList': []
        }
    }

    var comment = data.comment;
    $(t).parent().prev().val('');
    $(t).parents('.weikeCellComment').next().append(initCommentTemplate(comment));

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
}

var makeComment2comment = function (t) {
    var commentId = $($(t).parents('.media')[0]).attr('id');
    var content = $(t).parent().prev().val();


    // post comment
    // if success, return the new comment
    var time = new Date();
    var data = {
        'comment': {
            'id': 103,
            'user': {
                'name': '用户名3',
                'imgSrc': 'resource/img/8.jpg'
            },
            'time': time.getYear() + '-' + time.getMonth() + '-' + time.getDay() + ' ' + time.getHours() + ':' + time.getMinutes(),
            'content': content,
            'commentList': []
        }
    }

    var comment = data.comment;
    $(t).parent().prev().val('');
    $($(t).parents('.media-body')[0]).append(initCommentTemplate(comment));
    hideComment2comment(t);

}

var hideComment2comment = function (t) {
    $(t).parents('.weikeCellComment').remove();

}


