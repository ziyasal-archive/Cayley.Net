using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cayley.Net.Dsl.Gremlin
{
    internal class GremlinPagedEnumerator<TResult> : IEnumerator<TResult>
    {
        private readonly Func<IGremlinQuery, IEnumerable<TResult>> _pageLoadCallback;
        private readonly int _pageSize;
        private readonly IGremlinQuery _query;
        private TResult[] _currentPageData;

        private int _currentPageIndex = -1;
        private int _currentRowIndex = -1;

        public GremlinPagedEnumerator(
            Func<IGremlinQuery, IEnumerable<TResult>> pageLoadCallback,
            IGremlinQuery query,
            int pageSize = 100)
        {
            _pageLoadCallback = pageLoadCallback;
            _query = query;
            _pageSize = pageSize;
        }

        public bool MoveNext()
        {
            bool hasAPageLoaded = _currentPageIndex != -1;
            bool curentPageIsPartialPage = _currentPageData != null && _currentPageData.Count() < _pageSize;
            bool currentRecordIsLastOneOnPage = _currentPageData != null &&
                                                _currentRowIndex == _currentPageData.Count() - 1;

            if (hasAPageLoaded && curentPageIsPartialPage && currentRecordIsLastOneOnPage)
                return false;

            if (!hasAPageLoaded || currentRecordIsLastOneOnPage)
                LoadNextPage();

            if (_currentPageData == null)
                throw new InvalidOperationException("CurrentPageData is null even though we have a page index.");

            _currentRowIndex++;
            return _currentRowIndex < _currentPageData.Count();
        }

        public void Reset()
        {
            // MSDN says this method only exists for COM interop and that we
            // don't need to bother with it otherwise http://l.tath.am/ohpNvS
            throw new NotSupportedException();
        }

        public TResult Current
        {
            get { return _currentPageData[_currentRowIndex]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
        }

        private void LoadNextPage()
        {
            _currentPageIndex++;
            _currentRowIndex = -1;
            int drop = _currentPageIndex*_pageSize;
            IGremlinQuery pageQuery = _query.AddBlock(".drop({0}).take({1})._()", drop, _pageSize);
            _currentPageData = _pageLoadCallback(pageQuery).ToArray();
        }
    }
}