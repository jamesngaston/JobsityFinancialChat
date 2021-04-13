using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobsityFinancialChat.Models
{
    // Models returned by MeController actions.
    public class GetViewModel
    {
        public string Nickname { get; set; }
    }
}