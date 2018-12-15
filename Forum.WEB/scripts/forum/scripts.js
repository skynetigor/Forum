$(document).ready(function () {
    tinymce.init({
        theme: 'modern',
        selector: '#editor',
        language: "ru"
    });
});

$('#topic-send-btn').click(function () {
    var url = '/topic/update';
    var sendedData = { };
    sendedData.Description = $('[name = "Description"]').val();
    sendedData.Id = $('[name = "Id"]').val();
    sendedData.SubCategoryId = $('[name = "SubCategoryId"]').val();
    sendedData.Message = tinyMCE.activeEditor.getContent();
    $.ajax(url, {
        type: "POST",
        url: url,
        data: sendedData,
        error: function (data, s, m) {

        },
        success: function (data, s, m) {
            location.href = data;
        }
    });
});

$('#comment-send-btn').click(function () {
    var topicUrl = '/topic/index';
    var url = '/comment/update';
    var sendedData = {};
    sendedData.Id = $('[name = "Id"]').val();
    sendedData.TopicId = $('[name = "TopicId"]').val();
    sendedData.Message = tinyMCE.activeEditor.getContent();
    sendedData.returnPageId = $('[name = "returnPageId"]').val();

    $.ajax(url, {
        type: "POST",
        url: url,
        data: sendedData,
        error: function (data, s, m) {

        },
        success: function (data, s, m) {
            location.href = topicUrl + '?id=' + sendedData.TopicId + '&last=true';
        }
    });
});

var isCommentCheckBox = $('[name = isComment]');
var isTopicCheckBox = $('[name = isTopic]');
var isAccessCheckBox = $('[name = isAccess]');

function findCheckBoxes(checkbox) {
    return $(checkbox).parent().parent().find('input[type=checkbox]');
}

function SendBlock(checkBoxes, id) {
    var url = 'block';
    var data = { UserId: id };
    checkBoxes.each(function (indexInArray, valueOfElement) {
        var obj = $(valueOfElement);
        var name = obj.attr('name');
        var value = obj.prop('checked');
        data[name] = value;
    });
    data.UserId = id;
    $.post(url, data);
}

function BlockUser(ch,id) {
    var chbx = findCheckBoxes(ch);
    SendBlock(chbx, id);
}

function FullBlockUser(ch, id) {
    var val = $(ch).prop('checked');
    var chbx = findCheckBoxes(ch);
    chbx.prop('checked', val);
    SendBlock(chbx, id);
}