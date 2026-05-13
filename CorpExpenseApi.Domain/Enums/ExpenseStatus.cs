using System;
using System.Collections.Generic;
using System.Text;

namespace CorpExpenseApi.Domain.Enums
{
    public enum ExpenseStatus
    {
        Draft = 0,
        Submitted = 1,
        UnderReview = 2,
        Approved = 3,
        Rejected = 4,
        Reimbursed = 5
    }
}
