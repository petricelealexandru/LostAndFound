var categoriesAvailableList = [
    {
        "Id": "00DBF0D7-3781-49D7-BD92-1E60B8C9B455",
        "Text": "Obiect"
    },
    {
        "Id": "FF9E110D-68F8-435C-A082-EFD709FE1A66",
        "Text": "Animal"
    }]

ko.bindingHandlers.dateTimePicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().dateTimePickerOptions || {};
        $(element).datetimepicker(options);

       //when a user changes the date, update the view model
         ko.utils.registerEventHandler(element, "dp.change", function (event) {
             var value = valueAccessor();
             if (ko.isObservable(value)) {
                 if ((event.date != null || !event.date) && (event.date instanceof Date)) {
                     value(event.date.toDate());
                 } else {
                     value(event.date);
                 }
             }
         });

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            var picker = $(element).data("DateTimePicker");
            if (picker) {
                picker.destroy();
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var picker = $(element).data("DateTimePicker");
        //when the view model is updated, update the widget
        if (picker) {
            var koDate = ko.utils.unwrapObservable(valueAccessor());

            //in case return from server datetime i am get in this form for example /Date(93989393)/ then fomat this
            koDate = (typeof (koDate) !== 'object') && koDate ? new Date(parseFloat(koDate.replace(/[^0-9]/g, ''))) : koDate;

            if (koDate)
                picker.date(koDate);
        }
    }
};

function selectTab(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    if (evt) {
        evt.currentTarget.className += " active";
    }
}

function CountyViewModel() {
    var self = this;

    self.Id = ko.observable();
    self.Text = ko.observable();

    self.Initialize = function (data) {
        self.Id = data.Id;
        self.Text = data.Text;
    }
}

var emailRegex = new RegExp('[a-z0-9]+@[a-z]+\.[a-z]{2,3}');
var numberRegex = new RegExp(/^\s*[+-]?(\d+|\d*\.\d+|\d+\.\d*)([Ee][+-]?\d+)?\s*$/);
var phoneNumberRegex = new RegExp(/^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/);