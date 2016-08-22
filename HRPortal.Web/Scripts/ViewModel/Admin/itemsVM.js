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

    loadProducts: function () {
        var self = this;
        SubmitGet('/api/AdminApi/GetProducts', null, function (data) {
            self.allProducts(data.products);
            self.allCategories(data.categories);
        });
    },

    createProduct: function () {
        var self = this;
        if (!self.name() || self.name().trim().length === 0 || !self.description() || self.description().trim().length === 0 || self.price() == 0) {
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

    editProduct: function (cat) {
        bootbox.prompt({
            title: "Введите название продукта",
            value: cat.categoryName,
            callback: function (result) {
                if (result != null) {
                    if (result.length > 0) {
                        SubmitPostWithParams('/api/AdminApi/UpdateProduct', { id: cat.id, CategoryName: result }, function (data) {
                            itemsVM.clearProducts();
                            itemsVM.loadProducts();
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
    }
};