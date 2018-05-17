package com.groger.grogerapp.service.model

data class UserToken (
        val access_token : String,
        val token_type : String,
        val expires_in : String
)