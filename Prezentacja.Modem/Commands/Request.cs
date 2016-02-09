using System;

namespace Prezentacja.Modem.Commands
{
    public class Request
    {
        public Request(string query)
            : this(query, null)
        {
        }

        public Request(string query, Action<string> onSuccess)
            : this(query, onSuccess, null)
        {
        }

        public Request(string query, Action<string> onSuccess, Action onError)
            : this(query, onSuccess, onError, null)
        {
        }

        public Request(string query, Action<string> onSuccess, Action onError, Action onFinish)
        {
            Query = query;
            OnSuccess = onSuccess;
            OnError = onError;
            OnFinish = onFinish;
        }

        public string Answer { get; protected set; }

        public string Query { get; protected set; }

        public Action<string> OnSuccess { get; protected set; }

        public Action OnError { get; protected set; }

        public Action OnFinish { get; protected set; }

        public bool? IsSuccess { get; protected set; }

        public void InvokeAction(bool? isSuccess, string answer)
        {
            IsSuccess = isSuccess;

            if (IsSuccess.HasValue)
            {
                if (IsSuccess.Value && OnSuccess != null) OnSuccess(answer);
                else if (OnError != null) OnError();
            }

            if (OnFinish != null) OnFinish();
        }
    }
}
