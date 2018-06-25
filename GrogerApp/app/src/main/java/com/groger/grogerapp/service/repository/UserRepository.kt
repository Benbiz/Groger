package com.groger.grogerapp.service.repository

import com.groger.grogerapp.service.GrogerService
import com.groger.grogerapp.service.model.UserCredential
import com.groger.grogerapp.service.model.UserToken
import io.reactivex.Observable
import javax.inject.Inject

object UserRepositoryProvider {
    fun provideUserRepository() : UserRepository
    {
        return UserRepository(GrogerService.create())
    }
}

class UserRepository @Inject constructor(private val apiService : GrogerService)
{
    fun login(credential: UserCredential) : Observable<UserToken>
    {
        return Observable.create({
            val response = apiService.login("password", credential.username, credential.password).execute()
            if (response.isSuccessful)
            {
                val token= response.body()
                if (token != null)
                    it.onNext(token)
                it.onComplete()
            }
            else
            {
                it.onError(Throwable (when (response?.code())
                {
                    404 -> "Service unreachable"
                    400 -> "Username or password incorrect"
                    500 -> "Service error"
                    else -> "Unknown error"
                }))
                it.onComplete()
            }
        })

    }
}