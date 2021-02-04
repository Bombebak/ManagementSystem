var selectPureService = (function () {

    var self = this; 

    self.initializeSelectPure = function (selectPureTarget, options, value, onChangeCallback) {
        return new SelectPure(selectPureTarget, {
            options: options,
            value: value,
            multiple: true, // default: false
            autocomplete: true, // default: false
            icon: "fa fa-times",
            onChange: onChangeCallback,
            placeholder: 'Start typing',
            classNames: {
                select: "select-pure__select",
                dropdownShown: "select-pure__select--opened",
                multiselect: "select-pure__select--multiple",
                label: "select-pure__label",
                placeholder: "select-pure__placeholder",
                dropdown: "select-pure__options",
                option: "select-pure__option",
                autocompleteInput: "select-pure__autocomplete",
                selectedLabel: "select-pure__selected-label",
                selectedOption: "select-pure__option--selected",
                placeholderHidden: "select-pure__placeholder--hidden",
                optionHidden: "select-pure__option--hidden",
            }
        });
    };

    return {
        initializeSelectPure
    };
}());
