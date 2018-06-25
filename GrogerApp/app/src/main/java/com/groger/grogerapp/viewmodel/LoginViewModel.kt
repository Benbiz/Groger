package com.groger.grogerapp.viewmodel

import android.arch.lifecycle.MutableLiveData
import android.arch.lifecycle.ViewModel
import android.databinding.ObservableField
import com.groger.grogerapp.service.model.UserCredential
import com.groger.grogerapp.service.model.UserToken
import com.groger.grogerapp.service.repository.UserRepositoryProvider
import com.groger.grogerapp.view.listener.SigninResultCallback
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.CompositeDisposable
import io.reactivex.observers.DisposableObserver
import io.reactivex.schedulers.Schedulers

class LoginViewModel(val callback: SigninResultCallback)  : ViewModel() {

    val credential = ObservableField<UserCredential>(UserCredential("", ""))
    val isLoading = ObservableField<Boolean>(false)
    val userToken = MutableLiveData<UserToken>()
    val isCredentialValid = ObservableField<Boolean>(true)
    private val disposables = CompositeDisposable()

    override fun onCleared() {
        disposables.dispose()
        super.onCleared()
    }


    fun signIn() {
        val cred = credential.get()

        if (cred != null) {
            isLoading.set(true)
            disposables.add(UserRepositoryProvider.provideUserRepository().login(cred).subscribeOn(Schedulers.newThread())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribeWith(object : DisposableObserver<UserToken>() {
                        override fun onComplete() {
                            isLoading.set(false)
                            callback.onSuccess(userToken.value)
                        }

                        override fun onNext(t: UserToken) {
                            userToken.value = t
                        }

                        override fun onError(e: Throwable) {
                            isLoading.set(false)
                            val reason = e.message

                            if (reason != null)
                                callback.onError(reason)
                            else
                                callback.onError("Fail to login (No reason")
                        }

                    }))
        }
    }
}