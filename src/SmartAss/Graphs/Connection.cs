namespace SmartAss.Graphs;

public  sealed record Connection<TNode>(TNode From, TNode To, Distance Distance);
