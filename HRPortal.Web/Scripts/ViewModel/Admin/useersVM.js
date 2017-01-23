$(function () {
    ko.applyBindings(usersVM, document.getElementById("usermanagement"));
    usersVM.loadUsers();
});

var usersVM = {
    allUsers: ko.observableArray([]),

    loadUsers: function () {
        var self = this;
        SubmitGet('/api/AdminApi/GetUsers', null, function (data) {
            self.allUsers(data);
        });
    },

    banUser: function(user) {
        alsert("To do!");
    },

    clearCategories: function () {
        usersVM.allUsers(null);
    }
};