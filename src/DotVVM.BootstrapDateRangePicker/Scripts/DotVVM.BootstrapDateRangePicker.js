dotvvm.events.init.subscribe(function () {
    ko.bindingHandlers["bootstrap-daterangepicker"] = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

            // get the value from the viewmodel
            var value = ko.unwrap(valueAccessor());

            var options = {
                singleDatePicker: value.single || false,
                locale: {
                    "monthNames": moment.months()
                }
            };

            if (value.format)
                options.locale.format = value.format;
            if (value.applyLabel)
                options.locale.applyLabel = value.applyLabel;
            if (value.cancelLabel)
                options.locale.cancelLabel = value.cancelLabel;
            if (value.customRangeLabel)
                options.locale.customRangeLabel = value.customRangeLabel;

            console.log('ranges', value.ranges);
            if (value.ranges != null) {

                options.alwaysShowCalendars = false;

                options.ranges = {};
                for (var i = 0; i < value.ranges.length; i++) {

                    console.log('Date', value.ranges[i].Start);
                    console.log('Date', value.ranges[i].End);

                    let s = moment(dotvvm.serialization.parseDate(value.ranges[i].Start));
                    let e = moment(dotvvm.serialization.parseDate(value.ranges[i].End));

                    options.ranges[value.ranges[i].Label] = [s, e];
                }
            }

            console.log('options', options);

            var $el = $(element);
            $el.daterangepicker(options);

            $el.on('apply.daterangepicker', function (ev, picker) {
                console.log('callback', {
                    start: picker.startDate.toDate(),
                    end: picker.endDate.toDate(),
                    label: picker.chosenLabel
                });

                // this will retrieve the property from the viewmodel
                var prop = valueAccessor().value();

                console.log('prop', prop);

                element.daterangepickerupdating = true;

                console.log('update startDate', picker.startDate.toDate());
                prop.Start(picker.startDate.toDate());

                console.log('update endDate', picker.endDate.toDate());
                prop.End(picker.endDate.toDate());

                prop.HasValue(true);
                prop.IsOneDay(picker.startDate.isSame(picker.endDate, 'day'));

                // get index from value.ranges by picker.chosenLabel
                var index = value.ranges.findIndex(function (range) {
                    return range.Label === picker.chosenLabel;
                });
                if (index !== -1) {
                    prop.HasSelectedRange(true);
                    prop.SelectedRangeIndex(index);
                    prop.SelectedRange(value.ranges[index]);
                }
                else {
                    prop.HasSelectedRange(false);
                    prop.SelectedRangeIndex(null);
                    prop.SelectedRange(null);
                }

                element.daterangepickerupdating = false;
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            // get the value from the viewmodel
            var value = ko.unwrap(valueAccessor()).value();
            if (!element.daterangepickerupdating) {

                let isSingle = value.single || false;
                let startDate = value.Start();
                let endDate = value.End();

                console.log('update', { isSingle, startDate, endDate });

                if (startDate) {
                    startDate = moment(dotvvm.serialization.parseDate(startDate));

                    $(element).data('daterangepicker').setStartDate(startDate);

                    if (isSingle) {
                        $(element).data('daterangepicker').setEndDate(startDate);
                    }
                    else if (endDate) {
                        endDate = moment(dotvvm.serialization.parseDate(endDate));
                        $(element).data('daterangepicker').setEndDate(endDate);
                    }
                }
                else {
                    $(element).data('daterangepicker').setStartDate(moment());
                    $(element).data('daterangepicker').setEndDate(moment());
                }
            }
        }
    };
});