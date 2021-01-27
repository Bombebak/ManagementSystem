using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.Helpers
{
    public class ListItemDto<t>
    {
        public t Value { get; set; }
        public string Label { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }

        public ListItemDto()
        {
        }

        public ListItemDto(t value, string label)
        {
            Value = value;
            Label = label;
        }

        public ListItemDto(t value, string label, bool isSelected) : this(value, label)
        {
            IsSelected = isSelected;
        }

        public ListItemDto(t value, string label, bool isSelected, bool isDisabled) : this(value, label, isSelected)
        {
            IsDisabled = isDisabled;
        }
    }
}
