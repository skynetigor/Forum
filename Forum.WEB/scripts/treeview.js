//$(function () {
//    $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');
//    $('.tree li.parent_li > span').on('click', function (e) {
//        var children = $(this).parent('li.parent_li').find(' > ul > li');
//        if (children.is(":visible")) {
//            children.hide('fast');
//            $(this).attr('title', 'Expand this branch').find(' > i').addClass('icon-plus-sign').removeClass('icon-minus-sign');
//        } else {
//            children.show('fast');
//            $(this).attr('title', 'Collapse this branch').find(' > i').addClass('icon-minus-sign').removeClass('icon-plus-sign');
//        }
//        e.stopPropagation();
//    });
//});


$(document).ready(function () {
    $('.hasSub').click(function () {
        $(this).parent().toggleClass('subactivated');
        $(this).parent().children('ul:first').toggle();

        if ($(this).find('i').hasClass('glyphicon-folder-open')) {
            $(this).find('i').removeClass('glyphicon-folder-open').addClass('glyphicon-folder-close');
        } else {
            $(this).find('i').removeClass('glyphicon-folder-close').addClass('glyphicon-folder-open');
        }
    });

    $(".menufilter").keyup(function () {
        //$(this).addClass('hidden');

        var searchTerm = $(".menufilter").val();
        var listItem = $('.fordtreeview').children('li');

        var searchSplit = searchTerm.replace(/ /g, "'):containsi('")

        //extends :contains to be case insensitive
        $.extend($.expr[':'], {
            'containsi': function (elem, i, match, array) {
                return (elem.textContent || elem.innerText || '').toLowerCase()
                .indexOf((match[3] || "").toLowerCase()) >= 0;
            }
        });

        $(".fordtreeview li").not(":containsi('" + searchSplit + "')").each(function (e) {
            $(this).hide()
        });

        $(".fordtreeview li:containsi('" + searchSplit + "')").each(function (e) {
            $(this).show();
        });
    });
});