import torch

def dlg_attack(gradients_real, model):
    # Убедитесь, что определены необходимые параметры:
    # batch_size, input_shape, num_classes, max_iterations, criterion
    x_dummy = torch.randn(batch_size, *input_shape, requires_grad=True)  # Нужен requires_grad=True
    y_dummy = torch.randn(batch_size, num_classes, requires_grad=True)   # И здесь тоже
    print("Success")
    optimizer = torch.optim.LBFGS([x_dummy, y_dummy])

    for iteration in range(max_iterations):
        def closure():
            optimizer.zero_grad()
            pred_dummy = model(x_dummy)
            loss_dummy = criterion(pred_dummy, y_dummy)
            
            # Получаем градиенты модели
            gradients_dummy = torch.autograd.grad(
                loss_dummy, 
                model.parameters(), 
                create_graph=True  # Важно для вычисления градиентов от градиентов
            )
            
            # Сравниваем градиенты (исправлено .pow(2) вместо .pov)
            grad_diff = 0
            for g1, g2 in zip(gradients_dummy, gradients_real):
                grad_diff += (g1 - g2).pow(2).sum()

            grad_diff.backward()
            return grad_diff

        optimizer.step(closure)

    return x_dummy.detach(), y_dummy.detach()  # Отключаем градиенты для возвращаемых значений
