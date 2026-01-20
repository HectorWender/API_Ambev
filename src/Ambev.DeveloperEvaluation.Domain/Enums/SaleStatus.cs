namespace Ambev.DeveloperEvaluation.Domain.Enums
{
    public enum SaleStatus
    {
        Unknown = 0,
        Pending = 1,    // Sale created, but possibly not finalized.
        Completed = 2,  // Sale completed/valid (Equivalent to !IsCancelled)
        Cancelled = 3 
    }
}