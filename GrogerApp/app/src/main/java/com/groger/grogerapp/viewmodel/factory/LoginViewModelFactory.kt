package com.groger.grogerapp.viewmodel.factory

import android.arch.lifecycle.ViewModel
import android.arch.lifecycle.ViewModelProvider
import com.groger.grogerapp.view.callback.SigninResultCallback
import com.groger.grogerapp.viewmodel.LoginViewModel

class LoginViewModelFactory(private val signinResultCallback: SigninResultCallback) : ViewModelProvider.Factory {

    override fun <T : ViewModel?> create(modelClass: Class<T>): T {

        if (modelClass.isAssignableFrom(LoginViewModel::class.java)) {
            return LoginViewModel(signinResultCallback) as T
        }

        throw IllegalArgumentException("Unknown ViewModel Class")
    }

}