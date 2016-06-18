var oriShowCommentListBtn = null;

var showCommentDiv = function (t, oriBtn) {
    oriShowCommentListBtn = oriBtn

    var weikeId = $(t).parent().parent().attr('id');

    $.ajax({
        type: "post",
        url: "../CommentAction/getCommentList",
        data: weikeId,
        dataType: "json",
        success: function (data) {
            console.log("get comment success");
            // for example
            // required info
            var commentList = [
                {
                    'id': 101,
                    'user': {
                        'name': '用户名1',
                        'imgSrc': '../resource/img/8.jpg'
                    },
                    'time': '2016-6-17 13:50',
                    'content': '评论内容评论内容评论内容评论内容评论内容评论内容评论内容',
                    'commentList': [
                        {
                            'id': 102,
                            'user': {
                                'name': '用户名2',
                                'imgSrc': '../resource/img/8.jpg'
                            },
                            'time': '2016-6-17 13:50',
                            'content': '评论内容评论内容评论内容评论内容评论内容评论内容评论内容',
                            'commentList': [
                                {
                                    'id': 103,
                                    'user': {
                                        'name': '用户名3',
                                        'imgSrc': '../resource/img/8.jpg'
                                    },
                                    'time': '2016-6-17 13:50',
                                    'content': '评论内容评论内容评论内容评论内容评论内容评论内容评论内容',
                                    'commentList': []
                                }
                            ]
                        }
                    ]
                },
                {
                    'id': 102,
                    'user': {
                        'name': '用户名2',
                        'imgSrc': '../resource/img/8.jpg'
                    },
                    'time': '2016-6-17 13:50',
                    'content': '评论内容评论内容评论内容评论内容评论内容评论内容评论内容',
                    'commentList': []
                },
                {
                    'id': 103,
                    'user': {
                        'name': '用户名3',
                        'imgSrc': '../resource/img/8.jpg'
                    },
                    'time': '2016-6-17 13:50',
                    'content': '评论内容评论内容评论内容评论内容评论内容评论内容评论内容',
                    'commentList': []
                }

            ];
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

var initCommentDiv = function (commentList, parentNode) {
    for (var index in commentList) {
        parentNode.append(initCommentTemplate(commentList[index]));

        if (commentList[index].commentList.length > 0) {
            initCommentDiv(commentList[index].commentList, parentNode.children('#' + commentList[index].id + ':last-child').children('.media-body'));
        }
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
            '</li>'
}

var makeComment2weike = function (t) {
    var content = $(t).parent().prev().val();
    var commentTargetId = "0";
    var info = {
        "commentTargetId": commentTargetId,
        "content": content
    };
    var success = function (data) {
        console.log(data);
        var time = new Date();
        var data = {
            'comment': {
                'id': data,
                'user': {
                    'name': '用户名3',
                    'imgSrc': '../resource/img/8.jpg'
                },
                'time': time.getFullYear() + '-' + (time.getMonth() + 1) + '-' + time.getDate() + ' ' + time.getHours() + ':' + time.getMinutes(),
                'content': content,
                'commentList': []
            }
        }

        var comment = data.comment;
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
    var info = {
        "commentTargetId": commentTargetId,
        "content": content
    };
    var success = function (data) {
        console.log(data);
        var time = new Date();
        var data = {
            'comment': {
                'id': data,
                'user': {
                    'name': '用户名3',
                    'imgSrc': '../resource/img/8.jpg'
                },
                'time': time.getFullYear() + '-' + (time.getMonth() + 1) + '-' + time.getDate() + ' ' + time.getHours() + ':' + time.getMinutes(),
                'content': content,
                'commentList': []
            }
        }

        var comment = data.comment;
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
            console.log(data);
            var count = parseInt($(t).children('span').text()) + 1;
            $(t).children('span').text(parseInt($(t).children('span').text()) + 1);
            $(t).html("已赞 <span>" + count + "</span>");
            $(t).css('color', 'white');
            $(t).attr('onclick', 'dislikeWeike(this)');
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
        url: "../FollowAction/follow",
        data: {
            "userId": userId
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
        url: "../FollowAction/follow",
        data: {
            "userId": userId
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