using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Web.Models {

    public interface ISearchViewModel {
        IEnumerable Data { get; set; }
        DataParameter DataParameters { get; }
    }

    public abstract class SearchViewModel<T> : ISearchViewModel {
        public IEnumerable Data { get; set; }

        public DataParameter DataParameters { get { return getDataParameters(); }}
        protected abstract DataParameter getDataParameters();
    }

    public abstract class Parameters<T> where T: new() {
        public DataParameter DataParameters { get; set; }
        public T SearchParameters { get; set; }

        public Parameters() {
            DataParameters = new DataParameter();
            SearchParameters = new T();


        }
    }
}