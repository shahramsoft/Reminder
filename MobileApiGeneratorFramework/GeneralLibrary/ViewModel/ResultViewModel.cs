namespace GeneralLibrary.ViewModel
{
    public  class ResultViewModel
    {
        /// <summary>
        /// معتبر بودن 
        /// </summary>
        public bool Validate { get; set; }

        /// <summary>
        /// پیام
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// پیغام برای نوع معتبر بودن
        /// </summary>
        public string ValidateMessage { get; set; }
        /// <summary>
        /// پیغام خطا
        /// </summary>
        public string ExceptionMessage { get; set; }
        /// <summary>
        /// آیا توکن غیر معتبر است
        /// </summary>
        public bool InvalidToken { get; set; }

    }
}
