package com.groger.grogerapp.service.repository

import com.groger.grogerapp.service.GrogerService
import com.groger.grogerapp.service.model.*
import io.reactivex.Observable

object GroceryRepositoryProvider {
    fun provideGroceryRepository() : GroceryRepository
    {
        return GroceryRepository(GrogerService.create())
    }
}

class GroceryRepository(private val apiService : GrogerService)
{
    fun getClusterGroceries(token: String, cluster: Cluster) : Observable<List<Grocery>>
    {
        return Observable.create({
            val response = apiService.getClusterGroceries(token, cluster.id).execute()
            if (response.isSuccessful)
            {
                val grocery= response.body()
                if (grocery != null)
                    it.onNext(grocery)
                it.onComplete()
            }
            else
            {
                it.onError(Throwable (when (response?.code())
                {
                    404 -> "Service unreachable"
                    401 -> "Not authenticated"
                    500 -> "Service error"
                    else -> "Unknown error"
                }))
                it.onComplete()
            }
        })
    }

    fun updateClusterGroceries(token: String, cluster: Cluster, groceryId: Int, grocery: UpdateGrocery) : Observable<Unit>
    {
        return Observable.create({
            val response = apiService.updateClusterGroceries(token,cluster.id, groceryId, grocery).execute()
            if (response.isSuccessful)
            {
                it.onComplete()
            }
            else
            {
                it.onError(Throwable (when (response?.code())
                {
                    404 -> "Service unreachable"
                    401 -> "Not authenticated"
                    500 -> "Service error"
                    else -> "Unknown error"
                }))
                it.onComplete()
            }
        })
    }

    fun getGroceries(token: String, query : String) : Observable<List<Grocery>>
    {
        return Observable.create({
            val response = apiService.getGroceries(token, query, query, query).execute()
            if (response.isSuccessful)
            {
                val groceries= response.body()
                if (groceries != null)
                    it.onNext(groceries)
                it.onComplete()
            }
            else
            {
                it.onError(Throwable (when (response?.code())
                {
                    404 -> "Service unreachable"
                    401 -> "Not authenticated"
                    500 -> "Service error"
                    else -> "Unknown error"
                }))
                it.onComplete()
            }
        })
    }

    fun createGrocery(token : String, cluster: Cluster, grocery : NewGrocery) : Observable<Grocery>
    {
        return Observable.create({
            val response = apiService.createGrocery(token, cluster.id,grocery).execute()
            if (response.isSuccessful)
            {
                val grocery= response.body()
                if (grocery != null)
                    it.onNext(grocery)
                it.onComplete()
            }
            else
            {
                it.onError(Throwable (when (response?.code())
                {
                    404 -> "Service unreachable"
                    401 -> "Not authenticated"
                    500 -> "Service error"
                    else -> "Unknown error"
                }))
                it.onComplete()
            }
        })
    }

    fun addGrocery(token : String, cluster: Cluster, groceryId : Int, grocery: AddGrocery) : Observable<Grocery>
    {
        return Observable.create({
            val response = apiService.addGrocery(token, cluster.id, groceryId, grocery).execute()
            if (response.isSuccessful)
            {
                val grocery= response.body()
                if (grocery != null)
                    it.onNext(grocery)
                it.onComplete()
            }
            else
            {
                it.onError(Throwable (when (response?.code())
                {
                    404 -> "Service unreachable"
                    401 -> "Not authenticated"
                    500 -> "Service error"
                    else -> "Unknown error"
                }))
                it.onComplete()
            }
        })
    }

    fun removeGrocery(token : String, cluster: Cluster, grocery : Grocery) : Observable<Unit>
    {
        return Observable.create({
            val response = apiService.removeGrocery(token, cluster.id, grocery.id).execute()
            if (response.isSuccessful)
            {
                it.onComplete()
            }
            else
            {
                it.onError(Throwable (when (response?.code())
                {
                    404 -> "Service unreachable"
                    401 -> "Not authenticated"
                    500 -> "Service error"
                    else -> "Unknown error"
                }))
                it.onComplete()
            }
        })
    }
}