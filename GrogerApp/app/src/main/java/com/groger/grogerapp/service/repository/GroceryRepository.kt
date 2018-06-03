package com.groger.grogerapp.service.repository

import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.Grocery
import com.groger.grogerapp.service.model.NewCluster
import io.reactivex.Observable
import retrofit2.Call

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
}