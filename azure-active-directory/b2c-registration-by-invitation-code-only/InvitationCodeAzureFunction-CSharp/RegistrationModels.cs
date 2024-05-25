namespace b2c_reg_code_function
{
    public class SuccessResponse
    {
        public string promoCode { get; set; } = string.Empty;
    }

    public class ConflictResponse
    {
        public string version { get; set; } = "1.0.1";
        public string status { get; set; } = "409";
        public string userMessage { get; set; } = string.Empty;
    }
}