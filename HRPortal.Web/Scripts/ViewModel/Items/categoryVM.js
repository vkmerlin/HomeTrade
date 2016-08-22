$(function () {
    ko.applyBindings(categoryVM, document.getElementById("categorymanagement"));
    categoryVM.loadCategories();
});

var categoryVM = {
    allCategories: ko.observableArray([]),
    categoryName: ko.observable(),

    loadCategories: function () {
        var self = this;
        SubmitGet('/api/ItemApi/GetCategories', null, function (data) {
            self.allCategories(data);
        });
    },

    createCategory: function () {
        var self = this;
        if (!self.categoryName() || self.categoryName().trim().length === 0) {
            showBlockMessage('Нужно ввести название категории');
            return;
        }
        blockScreen();

        SubmitPostWithParams('/api/ItemApi/CreateNewCategory', { CategoryName: self.categoryName() }, function (data) {
            $('#addcatbtn').addClass('hidden');
            categoryVM.clearCategories();
            categoryVM.loadCategories();
            unblockScreen();
        });
    },

    editCategory: function (cat) {
        bootbox.prompt({
            title: "Введите название категории",
            value: cat.categoryName,
            callback: function (result) {
                if (result != null) {
                    if (result.length > 0) {
                        SubmitPostWithParams('/api/ItemApi/EditCategory', { id: cat.id, CategoryName: result }, function (data) {
                            categoryVM.clearCategories();
                            categoryVM.loadCategories();
                            unblockScreen();
                        });
                    }
                    else {
                        bootbox.alert("Введите название!");
                    }
                }
            }
        });
    },

    removeCategory: function (cat) {
        bootbox.confirm("Удалить категорию??", function (result) {
            if (result) {
                blockScreen();
                SubmitPostWithParams('/api/ItemApi/RemoveCategory', cat, function (data) {
                    categoryVM.clearCategories();
                    categoryVM.loadCategories();
                    unblockScreen();
                });
            }
        });
    },

    showcategoriessection: function () {
        if ($('#addcatbtn').hasClass('hidden')) {
            $('#addcatbtn').removeClass('hidden');
        }
        else { $('#addcatbtn').addClass('hidden'); }
    },

    clearCategories: function () {
        categoryVM.categoryName(null);
    }
};