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

    loadProducts: function () {
        var self = this;
        SubmitGet('/api/AdminApi/GetProducts', null, function (data) {
            self.allProducts(data.products);
            self.allCategories(data.categories);
        });
    },

    createProduct: function () {
        var self = this;
        if (!self.name() || self.name().trim().length === 0 || !self.description() || self.description().trim().length === 0 || self.price() == 0 || !self.selectedCategory()) {
            showBlockMessage('Нужно ввести всю информацию для продукта');
            return;
        }
        blockScreen();

        SubmitPostWithParams('/api/AdminApi/CreateNewProduct', { Name: self.name(), Description: self.description(), CategoryId: self.selectedCategory(), Price: self.price() }, function (data) {
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