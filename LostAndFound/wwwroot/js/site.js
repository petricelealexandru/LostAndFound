var categoriesAvailableList = [
    {
        "Id": "00DBF0D7-3781-49D7-BD92-1E60B8C9B455",
        "Text": "Obiect"
    },
    {
        "Id": "FF9E110D-68F8-435C-A082-EFD709FE1A66",
        "Text": "Animal"
    }]

// Get the modal
var modal = document.getElementById("myModal");

// Get the button that opens the modal
//ko.bindingHandlers.datepicker = {
//    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
//        //initialize datepicker with some optional options
//        var options = {
//            format: 'DD/MM/YYYY HH:mm',
//            defaultDate: valueAccessor()()
//        };

//        if (allBindingsAccessor() !== undefined) {
//            if (allBindingsAccessor().datepickerOptions !== undefined) {
//                options.format = allBindingsAccessor().datepickerOptions.format !== undefined ? allBindingsAccessor().datepickerOptions.format : options.format;
//            }
//        }

//        $(element).datetimepicker(options);
//        var picker = $(element).data('datetimepicker');

//        //when a user changes the date, update the view model
//        ko.utils.registerEventHandler(element, "dp.change", function (event) {
//            var value = valueAccessor();
//            if (ko.isObservable(value)) {
//                value(event.date);
//            }
//        });

//        var defaultVal = $(element).val();
//        var value = valueAccessor();
//        value(moment(defaultVal, options.format));
//    },
//    update: function (element, valueAccessor) {
//        var widget = $(element).data("datepicker");
//        //when the view model is updated, update the widget
//        if (widget) {
//            widget.date = ko.utils.unwrapObservable(valueAccessor());
//            if (widget.date) {
//                widget.setValue();
//            }
//        }
//    }
//};

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
    evt.currentTarget.className += " active";
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