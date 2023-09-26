using CATodo.BLLContracts;
using Microsoft.AspNetCore.Components;

namespace CATodo.BlazorApp.Components {
    public partial class TodoDetailComponent {
        [Parameter]
        public Todo? Todo { get; set; }
    }
}
