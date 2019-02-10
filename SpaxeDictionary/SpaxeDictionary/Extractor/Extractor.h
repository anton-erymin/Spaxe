#ifdef EXTRACTOR_EXPORTS
#define EXTRACTOR_API __declspec(dllexport)
#else
#define EXTRACTOR_API __declspec(dllimport)
#endif


EXTRACTOR_API int Process(HANDLE handle, INT pause, UINT data[], INT count, ULONG total);
