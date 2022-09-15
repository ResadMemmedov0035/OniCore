namespace OniCore.Persistence.Dynamic
{
    public class Sort
    {
        public string Field { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
    }

    /*
     * {
     *  field: "color",
     *  operator: "eq",
     *  value: "green",
     *  and: [
     *   {
     *    field: "cpu",
     *    operator: "eq",
     *    value: "i3",
     *    or: [
     *     {
     *      field: "cpu",
     *      operator: "eq",
     *      value: "i5",
     *     },
     *     {
     *      field: "cpu",
     *      operator: "eq",
     *      value: "i7",
     *     }
     *    ]
     *   }
     *  ]
     * }    
     * 
     */
}
