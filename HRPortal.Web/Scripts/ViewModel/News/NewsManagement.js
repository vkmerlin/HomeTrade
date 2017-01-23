$(function () {
    NewsVM.loadCategories();
    ko.applyBindings(NewsVM, document.getElementById("newssection"));
});

var NewsVM = {
    newsPage: 0,
    //Create news
    ShowFullMessage: ko.observable(),
    CreateTitle: ko.observable().extend({ required: true }),
    CreateMessage: ko.observable().extend({ required: true }),
    CreateImages: ko.observableArray([]),

    //News feed
    NewsId: ko.observable(),
    NewsTitle: ko.observable(),
    NewsMessage: ko.observable(),
    NewsPinned: ko.observable(false),
    NewsEditNewImages: ko.observableArray([]),
    AllNews: ko.observableArray([]),
    AvailableCategories: ko.observableArray([]),

    //Comments
    CreateNewComment: ko.observable(),
    ShowAllComments: ko.observable(),
    CreateNewReplyMessage: ko.observable(),
    NewReplyParent: ko.observable(),

    //Functions and handlers
    formatDate: function (item) {
        var date = new Date();
        var postDate = new Date(item.createDate);
        if (postDate.getDate() == date.getDate()) {
            return "сегодня, " + moment(postDate).format('HH-mm');
        }
        else if (postDate.getDate() == new Date(date.setDate(date.getDate() - 1)).getDate()) {
            return "вчера, " + moment(postDate).format('HH-mm');
        }
        else {
            return moment(postDate).format('YYYY-MM-DD');
        }
    },

    addHandlers: function () {
        $(window).scroll(function () {
            if ($(window).scrollTop() + $(window).height() == $(document).height()) {
                NewsVM.addNews();
            }
        });

        $('input#fileUpload').change(function () {
            NewsVM.CreateImages([]);
            var input = this;
            if (input.files.length > 5) {
                showBlockMessage('Максимум 5 картинок');
                return;
            }
            else if (input.files && input.files.length > 0) {
                $.each(input.files, function (i, c) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        NewsVM.CreateImages.push({ data: e.target.result });
                    }
                    reader.readAsDataURL(input.files[i]);
                });
            }
            else {
                NewsVM.CreateImages([]);
            }
        });
    },

    editHandler: function () {
        $('input#fileUpload').off().change(function () {
            NewsVM.CreateImages([]);
            var input = this;
            if (input.files.length > 5) {
                showBlockMessage('Максимум 5 картинок');
                return;
            }
            else if (input.files && input.files.length > 0) {
                $.each(input.files, function (i, c) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        NewsVM.CreateImages.push({ data: e.target.result });
                    }
                    reader.readAsDataURL(input.files[i]);
                });
            }
            else {
                NewsVM.CreateImages([]);
            }
        });

        $('input.editfileupload').off().change(function () {
            NewsVM.NewsEditNewImages([]);
            var input = this;
            if (Enumerable.From(NewsVM.AllNews()).Where(function (x) { return x.id == NewsVM.NewsId() }).FirstOrDefault().attachedFiles.length + input.files.length > 5) {
                showBlockMessage('Максимум 5 картинок');
                return;
            }
            else if (input.files && input.files.length > 0) {
                $.each(input.files, function (i, c) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        NewsVM.NewsEditNewImages.push({ data: e.target.result });
                    }
                    reader.readAsDataURL(input.files[i]);
                });
            }
            else {
                NewsVM.NewsEditNewImages([]);
            }
        });
    },

    loadCategories: function () {
        var self = this;
        SubmitGet('/api/NewsApi/GetCategories', null, function (data) {
            self.AvailableCategories(data);
            self.loadNews();
            self.addHandlers();
        });
    },

    loadNews: function () {
        this.newsPage = 0;
        var self = this;
        SubmitGet('/api/NewsApi/GetNews', null, function (data) {
            $.each(data, function () {
                this.formattedDate = NewsVM.formatDate(this);
            });
            self.AllNews(data);
            self.newsPage = Enumerable.From(NewsVM.AllNews()).Where(function (x) { return x.isPin == false }).Count();
            setTimeout(NewsVM.editHandler(), 1000);
        });
    },

    loadNewsWithCurrentPosition: function () {
        blockScreen();
        NewsVM.clearNews();
        var self = NewsVM;
        SubmitGet('/api/NewsApi/GetNewsWithPosition', { pageNum: self.newsPage }, function (data) {
            $.each(data, function () {
                this.formattedDate = NewsVM.formatDate(this);
            });
            self.AllNews(data);
            setTimeout(NewsVM.editHandler(), 1000);
            unblockScreen();
        });
    },

    createNews: function () {
        if (!NewsVM.CreateTitle.isValid() || !NewsVM.CreateMessage.isValid()) {
            var valerrors = ko.validation.group(NewsVM);
            valerrors.showAllMessages(true);
            return false;
        }

        var self = this;
        var data = new FormData();
        var files = $("#fileUpload").get(0).files;
        if (files.length > 5) {
            showBlockMessage('Максимум 5 картинок');
            return;
        }
        else if (files.length > 0) {
            $.each(files, function (k, v) {
                data.append("file" + k, files[k]);
            });
        }
        blockScreen();
        data.append("NewsCategoryId", $('#newscatsddl').val());
        data.append("Title", this.CreateTitle());
        data.append("Message", this.CreateMessage());
        data.append("IsPin", this.NewsPinned() == null ? false : this.NewsPinned());

        SubmitPostWithParamsNoTypes('/api/NewsApi/CreateNews', data, function (responce) {
            self.loadNews();
            self.clearNews();
            var valerrors = ko.validation.group(this);
            valerrors.showAllMessages(false);
            unblockScreen();
        });
    },

    editNews: function (news) {
        NewsVM.clearNews();
        var selectedNews = Enumerable.From(NewsVM.AllNews()).Where(function (x) { return x.Id == news.Id }).FirstOrDefault();
        NewsVM.ShowFullMessage(news.id);
        NewsVM.NewsId(news.id);
        NewsVM.NewsTitle(selectedNews.title);
        NewsVM.NewsMessage(selectedNews.message);
    },

    updateNews: function () {
        if (this.title.length < 1 || this.message.length < 1) {
            showBlockMessage('Validation error');
            return false;
        }

        var self = this;
        var data = new FormData();
        var filestorage = $("input#" + self.id + ".editfileupload");
        if (filestorage.get(0)) {
            var files = filestorage.get(0).files;
            if (files != null) {
                if (Enumerable.From(NewsVM.AllNews()).Where(function (x) { return x.id == NewsVM.NewsId() }).FirstOrDefault().attachedFiles.length + files.length > 5) {
                    showBlockMessage('Максимум 5 картинок');
                    return;
                }
                else if (files.length > 0) {
                    $.each(files, function (k, v) {
                        data.append("file" + k, files[k]);
                    });
                }
            }
        }
        blockScreen();
        data.append("Id", this.id);
        data.append("NewsCategoryId", this.newsCategoryId);
        data.append("Title", this.title);
        data.append("Message", this.message);
        data.append("IsPin", this.isPin);

        SubmitPostWithParamsNoTypes('/api/NewsApi/EditNews', data, function (data) {
            NewsVM.loadNewsWithCurrentPosition();
            NewsVM.clearNews();
            unblockScreen();
        });
    },

    removeNews: function (news) {
        bootbox.confirm("Удалить новость?", function (result) {
            if (result) {
                SubmitPostWithParams('/api/NewsApi/RemoveNews', news, function (data) {
                    NewsVM.loadNewsWithCurrentPosition();
                    NewsVM.clearNews();
                });
            }
        });
    },

    removeComment: function (comment) {
        bootbox.confirm("Удалить комментарий?", function (result) {
            if (result) {
                SubmitPostWithParams('/api/NewsApi/RemoveComment', comment, function (data) {
                    NewsVM.loadNewsWithCurrentPosition();
                    NewsVM.clearNews();
                });
            }
        });
    },

    removeReply: function (reply) {
        bootbox.confirm("Удалить ответ на комментарий?", function (result) {
            if (result) {
                SubmitPostWithParams('/api/NewsApi/RemoveReply', reply, function (data) {
                    NewsVM.loadNewsWithCurrentPosition();
                    NewsVM.clearNews();
                });
            }
        });
    },

    removeAttachment: function (attId) {
        SubmitPostWithParams('/api/NewsApi/RemoveAttachment', attId, null);
        $.each(NewsVM.AllNews(), function (i, s) {
            var index = i;
            var attachments = this.attachedFiles;
            $.each(attachments, function (o, k) {
                if (this.id == attId.id) {
                    $('#' + NewsVM.AllNews()[index].attachedFiles[o].id).fadeOut();
                    attachments.splice(o, 1);
                    return;
                }
            });
        });
    },

    showfullmessage: function (news) {
        NewsVM.ShowFullMessage(news.id);
    },
    addNews: function () {
        var self = this;
        blockScreen();
        SubmitGet('/api/NewsApi/GetNews', { pageNum: NewsVM.newsPage }, function (data) {
            $.each(data, function () {
                this.formattedDate = NewsVM.formatDate(this);
                self.AllNews.push(this);
            });
            self.newsPage = Enumerable.From(NewsVM.AllNews()).Where(function (x) { return x.isPin == false }).Count();
            setTimeout(NewsVM.editHandler(), 1000);
            unblockScreen();
        });
    },

    sendNewComment: function (news) {
        if (!NewsVM.CreateNewComment() || NewsVM.CreateNewComment().length == 0) {
            showBlockMessage("Сообщение пустое.");
            return;
        }
        var newComment = { NewsId: this.id, Message: NewsVM.CreateNewComment() };
        SubmitPostWithParams('/api/NewsApi/AddNewsComment', newComment, function (data) {
            NewsVM.loadNewsWithCurrentPosition();
            NewsVM.clearNews();
        });
    },

    addNewReply: function (comment) {
        NewsVM.CreateNewReplyMessage(null);
        if (NewsVM.NewReplyParent() == this.id) {
            NewsVM.NewReplyParent(null);
        }
        else {
            NewsVM.NewReplyParent(this.id);
        }
    },


    sendNewReply: function (comment) {
        if (!NewsVM.CreateNewReplyMessage() || NewsVM.CreateNewReplyMessage().length == 0) {
            showBlockMessage("Ответа нет.");
            return;
        }
        var newReply = {
            NewsCommentsId: this.id,
            Message: NewsVM.CreateNewReplyMessage()
        };
        SubmitPostWithParams('/api/NewsApi/AddReply', newReply, function (data) {
            NewsVM.loadNewsWithCurrentPosition();
            NewsVM.clearNews();
        });
    },

    expandCommentsSection: function (news) {
        NewsVM.ShowAllComments(news.id);
    },

    clearNews: function () {
        if (document.getElementById("addfilecontainer")) {
            document.getElementById("addfilecontainer").innerHTML = document.getElementById("addfilecontainer").innerHTML;
        }
        NewsVM.ShowFullMessage(null),
        NewsVM.NewsId(null),
        NewsVM.NewsTitle(null),
        NewsVM.NewsMessage(null),
        NewsVM.NewsPinned(null),
        NewsVM.NewsEditNewImages([]),
        NewsVM.CreateTitle(null),
        NewsVM.CreateMessage(null),
        NewsVM.CreateImages([]),
        NewsVM.CreateNewComment(null),
        NewsVM.ShowAllComments(null),
        NewsVM.CreateNewReplyMessage(null),
        NewsVM.NewReplyParent(null);
        var valerrors = ko.validation.group(this);
        valerrors.showAllMessages(false);
    }
};


