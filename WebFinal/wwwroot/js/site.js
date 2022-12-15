// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


    $(document).ready(function () {
        $('#example').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.13.1/i18n/pt-BR.json"
            }
            });

    });
    $(document).ready(function () {
        $('#relatorio').DataTable({

            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.13.1/i18n/pt-BR.json"
            },
           
        });

    });


function limparViewBagCPF() {
    var cpf = document.getElementById("CPFCADASTRADO");
    cpf.innerHTML = "";
}

function LimparViewBag()
{
 
    var campo = document.getElementById("InputEmBranco")
    var campoI = document.getElementById("InputEmBranc");

    campoI.innerHTML = "";
    campo.innerHTML =  "";
}