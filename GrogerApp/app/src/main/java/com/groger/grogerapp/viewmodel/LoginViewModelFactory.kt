package com.groger.grogerapp.viewmodel

import android.arch.lifecycle.ViewModel
import android.arch.lifecycle.ViewModelProvider
import com.groger.grogerapp.view.callback.SigninResultCallback

class LoginViewModelFactory(private val signinResultCallback: SigninResultCallback) : ViewModelProvider.Factory {

    override fun <T : ViewModel?> create(modelClass: Class<T>): T {

        if (modelClass.isAssignableFrom(LoginViewModel::class.java)) {
            return LoginViewModel(signinResultCallback) as T
        }

        throw IllegalArgumentException("Unknown ViewModel Class")
    }

}