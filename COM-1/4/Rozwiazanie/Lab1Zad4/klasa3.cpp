#include<windows.h>
#include <iostream>
#include"klasa3.h"


extern volatile int usageCount;


Klasa3::Klasa3() {
	usageCount++;
	};


Klasa3::~Klasa3() {
	usageCount--;
	};


ULONG STDMETHODCALLTYPE Klasa3::AddRef() {
	InterlockedIncrement(&m_ref);
	return m_ref;
	};


ULONG STDMETHODCALLTYPE Klasa3::Release() {
	ULONG rv = InterlockedDecrement(&m_ref);
	if (rv == 0) delete this;
	return rv;
	};


HRESULT STDMETHODCALLTYPE Klasa3::QueryInterface(REFIID iid, void **ptr) {
	if(ptr == NULL) return E_POINTER;
	if(IsBadWritePtr(ptr, sizeof(void *))) return E_POINTER;
	*ptr = NULL;
	if(iid == IID_IUnknown) *ptr = this;
	if(iid == IID_IKlasa3) *ptr = this;
	if(*ptr != NULL) { AddRef(); return S_OK; };
	return E_NOINTERFACE;
	};

int STDMETHODCALLTYPE Klasa3::Test(const char *napis){
	std::cout << printf(napis) << std::endl;
	return 0;
	};

