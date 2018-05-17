package com.groger.grogerapp.view.callback

import com.groger.grogerapp.service.model.UserToken

interface SigninResultCallback {
    fun onSuccess(token: UserToken?)
    fun onError(reason: String)
}