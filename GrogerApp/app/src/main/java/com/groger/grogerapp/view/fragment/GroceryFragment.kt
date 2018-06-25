package com.groger.grogerapp.view.fragment

import android.arch.lifecycle.ViewModelProviders
import android.content.Context
import android.databinding.DataBindingUtil
import android.os.Bundle
import android.support.v4.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.bumptech.glide.Glide
import com.groger.grogerapp.R
import com.groger.grogerapp.databinding.FragmentGroceryBinding
import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.Grocery
import com.groger.grogerapp.service.model.UpdateGrocery
import com.groger.grogerapp.view.listener.groceries.GroceryUpdateListener
import com.groger.grogerapp.viewmodel.GroceriesViewModel
import com.groger.grogerapp.viewmodel.factory.GroceriesViewModelFactory


private const val ARG_TOKEN = "token"
private const val ARG_CLUSTER = "cluster"
private const val ARG_GROCERY = "grocery"

class GroceryFragment : Fragment() {

    lateinit var binding : FragmentGroceryBinding
    lateinit var viewModel : GroceriesViewModel
    private var token: String? = null
    private var grocery: Grocery? = null
    private var cluster : Cluster? =null
    private var updateGrocery : UpdateGrocery? = null
    private var listener: GroceryUpdateListener? = null


    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        binding = DataBindingUtil.inflate(inflater,
                R.layout.fragment_grocery, container, false)

        arguments?.let {
            token = it.getString(ARG_TOKEN)
            cluster = it.getSerializable(ARG_CLUSTER) as Cluster
            grocery = it.getSerializable(ARG_GROCERY) as Grocery
        }

        updateGrocery = UpdateGrocery(grocery!!.name, grocery!!.quantity!!)

        val factory = GroceriesViewModelFactory(token!!, cluster!!)
        viewModel = ViewModelProviders.of(this, factory)
                .get(GroceriesViewModel::class.java)

        binding.grocery = grocery
        binding.updateGrocery = updateGrocery
        if (grocery?.picture != null)
            Glide.with(binding.root)
                    .load(grocery?.picture)
                    .into(binding.imgGrocery)
        binding.executePendingBindings()

        binding.btnApply.setOnClickListener {
            onApplyClicked()
        }

        return binding.root
    }

    private fun onApplyClicked() {
        viewModel.updateGrocery(grocery!!.id, updateGrocery!!)
        listener?.onUpdateGrocery(grocery!!)
    }

    override fun onAttach(context: Context) {
        super.onAttach(context)
        if (context is GroceryUpdateListener) {
            listener = context
        } else {
            throw RuntimeException(context.toString() + " must implement GroceryUpdateListener")
        }
    }

    override fun onDetach() {
        super.onDetach()
        listener = null
    }

    companion object {
        /**
         * Use this factory method to create a new instance of
         * this fragment using the provided parameters.
         *
         * @param token Auth token
         * @param grocery Related grocery.
         * @return A new instance of fragment GroceryFragment.
         */
        // TODO: Rename and change types and number of parameters
        @JvmStatic
        fun newInstance(token: String, cluster : Cluster, grocery: Grocery) =
                GroceryFragment().apply {
                    arguments = Bundle().apply {
                        putString(ARG_TOKEN, token)
                        putSerializable(ARG_CLUSTER, cluster)
                        putSerializable(ARG_GROCERY, grocery)
                    }
                }
    }
}
