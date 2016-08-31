$(function () {
    ko.applyBindings(itemsVM, document.getElementById("itemmanagement"));
    itemsVM.loadProducts();
});

var itemsVM = {
    allProducts: ko.observableArray([]),
    allCategories: ko.observableArray([]),
    name: ko.observable(),
    description: ko.observable(),
    price: ko.observable(),
    selectedCategory: ko.observable(),
    productId: ko.observable(),
    newCategoryId: ko.observable(),
    uploadedimages: ko.observableArray([]),

    loadProducts: function () {
        var self = this;
        SubmitGet('/api/AdminApi/GetProducts', null, function (data) {
            self.allProducts(data.products);
            self.allCategories(data.categories);
            self.addHandlers();
        });
    },

    addHandlers: function () {
        $('input#fileUpload').change(function () {
            itemsVM.uploadedimages([]);
            var input = this;
            if (input.files.length > 5) {
                showBlockMessage('Максимум 5 картинок');
                return;
            }
            else if (input.files && input.files.length > 0) {
                $.each(input.files, function (i, c) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        itemsVM.uploadedimages.push({ data: e.target.result });
                    }
                    reader.readAsDataURL(input.files[i]);
                });
            }
            else {
                NewsVM.uploadedimages([]);
            }
        });
    },

    createProduct: function () {
        var self = this;
        if (!self.name() || self.name().trim().length === 0 || !self.description() || self.description().trim().length === 0 || self.price() == 0 || !self.selectedCategory()) {
            showBlockMessage('Нужно ввести всю информацию для продукта');
            return;
        }
        blockScreen();

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
        data.append("Name", self.name());
        data.append("Description", self.description());
        data.append("CategoryId", self.selectedCategory());
        data.append("Price", self.price());

        SubmitPostWithParamsNoTypes('/api/AdminApi/CreateNewProduct', data, function (responce) {
            $('#addprodbtn').addClass('hidden');
            itemsVM.clearProducts();
            itemsVM.loadProducts();
            unblockScreen();
        });
    },

    editProduct: function (item) {
        itemsVM.productId(item.id);
    },

    updateProduct: function(item) {
        var self = this;
        if (!item.name || item.name.trim().length === 0 || !item.description || item.description.trim().length === 0 || item.price == 0 || !item.categoryId) {
            showBlockMessage('Нужно ввести всю информацию для продукта');
            return;
        }
        blockScreen();

        SubmitPostWithParams('/api/AdminApi/UpdateProduct', { Id: item.id, Name: self.name, Description: item.description, CategoryId: item.categoryId, Price: item.price }, function (data) {
            $('#addprodbtn').addClass('hidden');
            itemsVM.clearProducts();
            itemsVM.loadProducts();
            unblockScreen();
        });
    },

    cancelUpdate: function(){
        itemsVM.clearProducts();
        itemsVM.loadProducts();
    },

    removeProduct: function (item) {
        bootbox.confirm("Удалить продукт??", function (result) {
            if (result) {
                blockScreen();
                SubmitPostWithParams('/api/AdminApi/RemoveProduct', item, function (data) {
                    itemsVM.clearProducts();
                    itemsVM.loadProducts();
                    unblockScreen();
                });
            }
        });
    },

    showproductsection: function () {
        if ($('#addprodbtn').hasClass('hidden')) {
            $('#addprodbtn').removeClass('hidden');
        }
        else { $('#addprodbtn').addClass('hidden'); }
    },

    clearProducts: function () {
        itemsVM.name(null);
        itemsVM.description(null);
        itemsVM.selectedCategory(null);
        itemsVM.price(null);
        itemsVM.selectedCategory(null);
        itemsVM.productId(null);
    }
};