using System;

namespace Common
{
    public class ApplicationException : Exception
    {
        private int m_ErrorCode;

        public int ErrorCode
        {
            get { return m_ErrorCode; }
        }

        public ApplicationException(int errorCode)
        {
            m_ErrorCode = errorCode;
        }
    }
}