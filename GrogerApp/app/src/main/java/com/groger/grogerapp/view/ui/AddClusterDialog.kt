package com.groger.grogerapp.view.ui

import android.app.AlertDialog
import android.app.Dialog
import android.content.Context
import android.databinding.DataBindingUtil
import android.os.Bundle
import android.support.v7.app.AppCompatDialogFragment
import com.groger.grogerapp.R
import com.groger.grogerapp.databinding.AddClusterBinding
import com.groger.grogerapp.service.model.NewCluster

class AddClusterDialog :  AppCompatDialogFragment() {

    private lateinit var binding: AddClusterBinding
    private lateinit var listener : NewClusterListener

    val cluster = NewCluster("","")

    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {
        super.onCreateDialog(savedInstanceState)

        val dialog = AlertDialog.Builder(activity)
        val inflater = activity?.layoutInflater

        binding = DataBindingUtil.inflate(inflater!!, R.layout.add_cluster, null, false)

        //binding =  DataBindingUtil.setContentView(activity!!, R.layout.add_cluster)

        binding.cluster = cluster
        binding.executePendingBindings()
        dialog.setView(binding.root)
                .setTitle("Create a new cluster")
                .setNegativeButton("Cancel", { _, _ -> })
                .setPositiveButton("Ok", { _, _ ->
                    listener.onNewCluster(cluster)
                })
        return dialog.create()
    }

    override fun onAttach(context: Context?) {
        super.onAttach(context)

        if (context is NewClusterListener)
            listener = context
        else
            throw ClassCastException("must implement NewClusterListener")

    }



    interface NewClusterListener{
        fun onNewCluster(cluster : NewCluster)
    }
}