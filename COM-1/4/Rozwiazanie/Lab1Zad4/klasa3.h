#include"Iklasa3.h"

class Klasa3: public IKlasa3 {
public:
	Klasa3();
	~Klasa3();
	virtual ULONG STDMETHODCALLTYPE AddRef();
	virtual ULONG STDMETHODCALLTYPE Release();
	virtual HRESULT STDMETHODCALLTYPE QueryInterface(REFIID iid, void **ptr);
	virtual int STDMETHODCALLTYPE Test(const char *napis);
	//virtual HRESULT STDMETHODCALLTYPE Test(const char* napis);


private:

	ULONG m_ref;

	};
